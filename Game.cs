using WorldOfZuul.Africa;

namespace WorldOfZuul
{
  public class Game
  {
    private Room? currentRoom;
    private Room? previousRoom;
    private AsiaRoom? Asia;
    private Room? Hub;
    private AfricaRoom? Africa;
    private Command? command;
    private bool continuePlaying = true;
    public static string? Initials { get; set; }

    public Game()
    {
      CreateRooms();
    }

    private void CreateRooms()
    {
      Asia = new("Somewhere in Asia", "Asia");
      Hub = new("Hub", "You stand in the Ranger Headquarter’s Council Room.\nAs you look around you feel amazed yet again at how this building constructed underwater has such an amazing view into the ocean and its diverse wildlife from the council. \nAdditionally, you see three screens, each with its mission objective and details. On the left screen you see the details for the Pacific (pacific) deployment. On the right screen you see the Asian (asia) mission. On the middle screen you see the information for the Africa (africa) deployment.");
      Africa = new(ChangeRoom, "Africa Mission", "", "As you approach the middle screen, the mission description says ”SAFE ENDANGERED KORDOFAN GIRAFFE FROM A WILDFIRE” You scream the keyword ”Africa!”. The floor below you opens, and you fall into the pipe. As you go down, it takes a sharp twist left, and you suddenly find yourself on the comfortable leather chair of a small submarine");


      Asia.SetExit("hub", Hub); //among the hub and other rooms we could use just typos like hub, asia, pacific...
      Africa.SetExit("hub", Hub);
      Hub.SetExit("asia", Asia); //set exit for asia -> to hub
      Hub.SetExit("africa", Africa);

      currentRoom = Hub;
    }

    public void Play()
    {
      PrintWelcome();


      GameConsole.WriteLine("Please enter your name: ");
      string inputName = GameConsole.Input();
      GetInicialOfThePlayer(inputName);
      GameConsole.WriteLine($"\nHello {inputName}, just a little reminder, for better game experience, do not forget to make your terminal fullscreen. Enjoy!\n", font: FontTheme.Info);

      while (continuePlaying)
      {
        GameConsole.WriteLine("\n" + currentRoom?.ShortDescription, fgColor: ConsoleColor.DarkGreen);


        //Decide action
        if (currentRoom == null) throw new Exception("Error, no current room. You are nowhere");

        switch (currentRoom)
        {
          case var currentRoom when currentRoom.Equals(Asia):

            // LoadingAnimation.Loading("Loading");
            Asia.CurrentlyInAsiaRoom();
            GameConsole.WriteLine("Welcome back to the hub", font: FontTheme.Success);
            Move("hub");
            break;

          case var currentRoom when currentRoom.Equals(Africa):

            Africa.StartAfricaMission();

            GameConsole.WriteLine("Welcome back to the hub", font: FontTheme.Success);
            currentRoom = Hub;
            break;
          default:
            break;
        }

        command = AskForCommand();
        Console.WriteLine(command?.Name);

        switch (command?.Name)
        {
          case "look":
            GameConsole.WriteLine(currentRoom?.LongDescription);
            break;

          case "back":
            if (previousRoom == null)
              GameConsole.WriteLine("You can't go back from here!");
            else
              currentRoom = previousRoom;
            break;

          case "north":
          case "south":
          case "east":
          case "west":
          case "asia":
          case "africa":
            Move(command.Name);
            break;

          case "quit":
            continuePlaying = false;
            break;

          case "help":
            PrintHelp();
            break;

          default:
            GameConsole.WriteLine("I don't know this command", font: FontTheme.Danger);
            break;
        }
      }

      GameConsole.WriteLine("Thank you for playing World of Zuul!");
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
        GameConsole.WriteLine($"You can't go {direction}!");
      }
    }

    public static Command AskForCommand()
    {
      GameConsole.WriteLine("Please enter a command");
      Command? userCommand;

      while ((userCommand = Parser.GetCommand(GameConsole.Input("", breakline: false))) == null)
      {
        GameConsole.WriteLine(
          "Invalid command. Type 'help' for a list of valid commands",
          font: FontTheme.Danger
        );
      }

      return userCommand;
    }

    public void ChangeRoom(Room room)
    {
      previousRoom = currentRoom;
      currentRoom = room;

    }


    private static void PrintWelcome()
    {
      GameConsole.WriteLine("Welcome to the World of Zuul!");
      GameConsole.WriteLine("World of Zuul is a new, incredibly boring adventure game.");
      PrintHelp();
      GameConsole.WriteLine();
    }

    internal static void PrintHelp()
    {
      GameConsole.WriteLine("\nNavigate by typing ['name of the room'].\nType 'look' for more details.\nType 'back' to go to the previous room.\nType 'help' to print this message again.\nType 'quit' to exit the game.", font: FontTheme.Info);
    }
    public static void GetInicialOfThePlayer(string initials)
    {
      Initials = initials.ToUpper().Substring(0, 1);
    }
  }
}
