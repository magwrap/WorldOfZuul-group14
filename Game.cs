namespace WorldOfZuul
{
  public class Game
  {
    private Room? currentRoom;
    private Room? previousRoom;
    private Room? Asia;

    public Game()
    {
      CreateRooms();
    }

    private void CreateRooms()
    {
      Asia = new("Somewhere in Asia", " ");
      Room? Hub = new("", " ");


      Asia.SetExit("hub", Hub); //among the hub and other rooms we could use just typos like hub, asia, pacific...

      Hub.SetExit("Asia", Asia); //set exit for asia -> to hub

      currentRoom = Asia;
    }

    public void Play()
    {
      Parser parser = new();

      PrintWelcome();

      bool continuePlaying = true;
      while (continuePlaying)
      {
        Console.WriteLine(currentRoom?.ShortDescription);
        if (currentRoom != null && currentRoom.Equals(Asia))
        {
          LoadingAnimation.Loading("Loading");
          AsiaRoom asiaRoom = new();
          while (asiaRoom.PlayerPresentInAsiaRoom)
          {
            asiaRoom.CurrentlyInAsiaRoom();
          }
          Move("hub");
          ConsoleChangeColorAndPrintMessage.PrintForeground(FontTheme.Success, "Welcome back to the hub");
        }
        Console.Write("> ");

        string? input = Console.ReadLine();

        if (string.IsNullOrEmpty(input))
        {
          Console.WriteLine("Please enter a command.");
          continue;
        }

        Command? command = parser.GetCommand(input);

        if (command == null)
        {
          ConsoleChangeColorAndPrintMessage.Print(FontTheme.Danger, "Invalid command. Type 'help' for a list of valid commands.");
          continue;
        }

        switch (command.Name)
        {
          case "look":
            Console.WriteLine(currentRoom?.LongDescription);
            break;

          case "back":
            if (previousRoom == null)
              Console.WriteLine("You can't go back from here!");
            else
              currentRoom = previousRoom;
            break;

          case "north":
          case "south":
          case "east":
          case "west":
            Move(command.Name);
            break;

          case "quit":
            continuePlaying = false;
            break;

          case "help":
            PrintHelp();
            break;

          default:
            ConsoleChangeColorAndPrintMessage.Print(FontTheme.Danger, "Invalid command. Type 'help' for a list of valid commands.");
            break;
        }
      }

      Console.WriteLine("Thank you for playing World of Zuul!");
    }

    private void Move(string direction)
    {
      if (currentRoom?.Exits.ContainsKey(direction) == true)
      {
        previousRoom = currentRoom;
        currentRoom = currentRoom?.Exits[direction];
      }
      else
      {
        Console.WriteLine($"You can't go {direction}!");
      }
    }


    private static void PrintWelcome()
    {
      Console.WriteLine("Welcome to the World of Zuul!");
      Console.WriteLine("World of Zuul is a new, incredibly boring adventure game.");
      PrintHelp();
      Console.WriteLine();
    }

    internal static void PrintHelp()
    {

      Console.WriteLine();
      Console.WriteLine("Navigate by typing 'north', 'south', 'east', or 'west'.");
      Console.WriteLine("Type 'look' for more details.");
      Console.WriteLine("Type 'back' to go to the previous room.");
      Console.WriteLine("Type 'help' to print this message again.");
      Console.WriteLine("Type 'quit' to exit the game.");
      System.Console.WriteLine();
    }
  }
}
