namespace WorldOfZuul
{
  class AsiaRoom : Room
  {
    bool continuePlaying = true;

    public static string? Inicials { get; set; }

    public AsiaRoom(string shortDesc, string longDesc) : base(shortDesc, longDesc)
    {

    }


    public void CurrentlyInAsiaRoom()
    {

      GameConsole.WriteLine("Hello ranger! Welcome to Asia. ");

      PrintIntroductionToTheRoom();

      Game.PrintHelp();

      GameConsole.WriteLine("Heads up ranger! You also acquired a map. The map can be seen by using command 'map on' as well as turned off by command 'map off'.", font: FontTheme.NewItem);


      while (continuePlaying)
      {
        string? input = GameConsole.Input();

        if (string.IsNullOrEmpty(input))
        {
          GameConsole.WriteLine("Please enter a command.");
          continue;
        }

        Command? command = Parser.GetCommand(input);

        if (command == null)
        {
          GameConsole.WriteLine("Invalid command. Type 'help' for a list of valid commands.", font: FontTheme.Danger);
          continue;
        }

        switch (command.Name)
        {
          case "look":
            GameConsole.WriteLine(LongDescription);
            break;

          case "back":
            // if (previousRoom == null)
            //     GameConsole.WriteLine("You can't go back from here!");
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

            GameConsole.WriteLine("Map is now visible", font: FontTheme.Success);

            GameConsole.WriteLine("Game tip: For better orientation look at the compass on the righthand side of the map.");

            Map.ShowMap(Map.PositionX, Map.PositionY);

            break;

          case "map off":
            Map.ChangeMapVisibility(false); //hide map

            GameConsole.WriteLine("Map is no longer visible", font: FontTheme.Danger);

            break;

          case "hub":

            GameConsole.WriteLine("Are you sure you want return to the hub? All progress will be lost!", font: FontTheme.Danger);

            string? inputConfirmation = GameConsole.Input("Yes/No");
            if (inputConfirmation == "yes")
            {
              continuePlaying = false;
              return;
            }
            LoadingAnimation.Loading("Redirecting back to lobby");

            break;

          case "quit":

            GameConsole.WriteLine("Are you sure you want quit playing? All progress will be lost!", font: FontTheme.Danger);

            string? inputConfirmation1 = GameConsole.Input("Yes/No");
            if (inputConfirmation1 == "yes")
            {
              LoadingAnimation.Loading("Quiting");
              continuePlaying = false;
              return;
            }

            break;

          case "help":
            Game.PrintHelp();
            break;
          case "clear":
            GameConsole.Clear();
            break;

          default:
            GameConsole.WriteLine("Invalid command. Type 'help' for a list of valid commands.", font: FontTheme.Danger);
            break;
        }
      }
      //GameConsole.WriteLine("Thank you for playing World of Zuul!");
    }
    public static void PrintIntroductionToTheRoom()
    {
      GameConsole.WriteLine("Park Ranger: Poaching across Asia is reaching critical levels, driven by an unrelenting demand for illegal wildlife products.");
      GameConsole.WriteLine("I am be here to guide you through the brief introduction into the quest, the rest falls upon your individual choices.");
      GameConsole.WriteLine("Hope you are up for the task, the poachers around here are relentless!");
    }


  }
}