using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldOfZuul.Africa
{
  public class AfricaRoom : Room
  {

    private MissionRoom? submarine;
    private MissionRoom? camp;
    // private MissionRoom? jungle;
    private readonly Action<Room> ChangeRoom;
    bool continuePlaying = true;

    public AfricaRoom(
      Action<Room> changeRoom,
      string? shortDesc,
      string? longDesc,
      string? msgOnArrival
    ) : base(shortDesc, longDesc)
    {
      ChangeRoom = changeRoom;
    }

    public void StartAfricaMission()
    {
      MissionGameRooms africaRooms = JsonFileReader.GetAfricaRooms();
      GameConsole.WriteLine(LongDescription, font: FontTheme.HighligtedText);

      if (africaRooms == null || africaRooms.Rooms == null)
      {
        throw new Exception("No africa rooms");
      };

      submarine = new(
        africaRooms.Rooms[(int)AfricaRoomsEnum.SUBMARINE].ShortDesc,
        africaRooms.Rooms[(int)AfricaRoomsEnum.SUBMARINE].LongDesc,
        africaRooms.Rooms[(int)AfricaRoomsEnum.SUBMARINE].MissionDescription,
        africaRooms.Rooms[(int)AfricaRoomsEnum.SUBMARINE].MessageOnArrival
        );

      camp = new(
        africaRooms.Rooms[(int)AfricaRoomsEnum.CAMP].ShortDesc,
        africaRooms.Rooms[(int)AfricaRoomsEnum.CAMP].LongDesc,
        africaRooms.Rooms[(int)AfricaRoomsEnum.CAMP].MissionDescription,
        africaRooms.Rooms[(int)AfricaRoomsEnum.CAMP].MessageOnArrival
      );

      // ChangeRoom(submarine);
      // submarine.SetExit("camp", camp);
      submarine.DisplayMissionDesc();


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
            GameConsole.WriteLine(submarine.LongDescription);
            break;

          case "back":
            // if (previousRoom == null)
            //     GameConsole.WriteLine("You can't go back from here!");
            // else
            //     currentRoom = previousRoom;
            return;
          // break;

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
    }
  }
}