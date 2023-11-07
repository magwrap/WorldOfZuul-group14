namespace WorldOfZuul
{
  class AsiaRoom
  {
    private bool playerPresentInAsiaRoom = true;

    public static string? Inicials { get; set; }

    public bool PlayerPresentInAsiaRoom
    {
      get { return playerPresentInAsiaRoom; }
    }


    public void CurrentlyInAsiaRoom()
    {
      Parser parser = new();

      string? inputName = null;
      System.Console.WriteLine("Hello ranger! Welcome to Asia. ");
      while (string.IsNullOrEmpty(inputName))
      {
        System.Console.Write("Please enter your name: \n> ");
        inputName = Console.ReadLine();
      }

      GetInicialOfThePlayer(inputName);

      System.Console.WriteLine($"\nHello {inputName}, just a little reminder, for better game experience, do not forget to make your terminal fullscreen. Enjoy!\n");

      PrintIntroductionToTheRoom();

      Game.PrintHelp();

      ConsoleChangeColorAndPrintMessage.Print(FontTheme.NewItem, "Heads up ranger! You also acquired a map. The map can be seen by using command 'map on' as well as turned off by command 'map off'.");

      bool continuePlaying = true;

      while (continuePlaying)
      {
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
            //Console.WriteLine(currentRoom?.LongDescription);
            break;

          case "back":
            // if (previousRoom == null)
            //     Console.WriteLine("You can't go back from here!");
            // else
            //     currentRoom = previousRoom;
            break;

          case "north":
          case "south":
          case "east":
          case "west":
            Map.MoveOnMap(command.Name);
            break;

          case "map on":
            Map.ChangeMapVisibility(true); //set map visible

            ConsoleChangeColorAndPrintMessage.Print(FontTheme.Success, "Map is now visible");

            System.Console.WriteLine("Game tip: For better orientation look at the compass on the righthand side of the map.");

            Map.ShowMap(Map.PositionX, Map.PositionY);

            break;

          case "map off":
            Map.ChangeMapVisibility(false); //hide map

            ConsoleChangeColorAndPrintMessage.Print(FontTheme.Danger, "Map is no longer visible");

            break;

          case "hub":

            ConsoleChangeColorAndPrintMessage.Print(FontTheme.Danger, "Are you sure you want return to the hub? All progress will be lost!");

            System.Console.Write("Yes/No\n> ");

            string? inputConfirmation = Console.ReadLine()?.ToLower();
            if (inputConfirmation == "yes")
            {
              continuePlaying = false;
              playerPresentInAsiaRoom = false;
            }
            LoadingAnimation.Loading("Redirecting back to lobby");

            break;

          case "quit":

            ConsoleChangeColorAndPrintMessage.Print(FontTheme.Danger, "Are you sure you want quit playing? All progress will be lost!");

            System.Console.Write("Yes/No\n> ");

            string? inputConfirmation1 = Console.ReadLine()?.ToLower();
            if (inputConfirmation1 == "yes")
            {
              continuePlaying = false;
              playerPresentInAsiaRoom = false;
            }
            LoadingAnimation.Loading("Quiting");

            break;

          case "help":
            Game.PrintHelp();
            break;
          case "clear":
            Console.Clear();
            break;

          default:
            ConsoleChangeColorAndPrintMessage.Print(FontTheme.Danger, "Invalid command. Type 'help' for a list of valid commands.");
            break;
        }
      }
      //Console.WriteLine("Thank you for playing World of Zuul!");
    }
    public void PrintIntroductionToTheRoom()
    {
      System.Console.WriteLine("Park Ranger: Poaching across Asia is reaching critical levels, driven by an unrelenting demand for illegal wildlife products.");
      System.Console.WriteLine("I am be here to guide you through the brief introduction into the quest, the rest falls upon your individual choices.");
      System.Console.WriteLine("Hope you are up for the task, the poachers around here are relentless!");
    }

    public static void GetInicialOfThePlayer(string inicials)
    {
      Inicials = inicials.ToUpper().Substring(0, 1);
    }
  }
}