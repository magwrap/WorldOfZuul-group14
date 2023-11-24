
namespace WorldOfZuul
{
  public class Actions
  {
    /// <summary>
    /// Action dispatcher returns true or false depending if the player wants to quit the game
    /// </summary>
    public Actions()
    {

    }



    /// <summary>
    /// Function moving player to different room
    /// </summary>
    /// <param name="direction">Direction in which player wants to go</param>
    /// <param name="currentRoom">reference to currentRoom </param>
    /// <param name="previousRoom">reference to previousRoom</param>
    public static void Move(string direction, ref Room? currentRoom, ref Room? previousRoom)
    {
      if (currentRoom?.Exits.ContainsKey(direction) == true)
      {
        GameConsole.WriteLine($"Moving to {direction}");
        previousRoom = currentRoom;
        currentRoom = currentRoom?.Exits[direction];
      }
      else
      {
        Messages.PrintWrongDirectionMessage();
      }
    }

    /// <summary>
    /// Function deciding which action to do depending on the command passed
    /// </summary>
    /// <param name="command">Command read from the users input</param>
    /// <param name="currentRoom">reference to current room created in game class</param>
    /// <param name="previousRoom">reference to previous room created in game class</param>
    /// <param name="isMissionStarted">boolean checking if the mission is started true for africa, asia etc.</param>
    /// <param name="missionName">name of the mission for actions specified for certain missions</param>
    /// <returns></returns>
    public static bool DecideAction(ref Command? command, ref Room? currentRoom, ref Room? previousRoom, bool? isMissionStarted = false, string? missionName = "")
    {
      if (currentRoom == null) return false;

      switch (command?.Name)
      {
        case "look":
          GameConsole.WriteLine(currentRoom?.LongDescription, fgColor: ConsoleColor.DarkYellow);
          return true;

        case "back":
          if (previousRoom == null)
            GameConsole.WriteLine("You can't go back from here!");
          else
            currentRoom = previousRoom;
          return true;

        case "map on":
          currentRoom?.roomMap.ChangeMapVisibility(true); //set map visible

          GameConsole.WriteLine("Map is now visible", font: FontTheme.Success);

          GameConsole.WriteLine("Game tip: For better orientation look at the compass on the righthand side of the map, to move faster you can also type 'n', 'e', 's', 'w'.", font: FontTheme.GameTip);

          currentRoom?.roomMap.ShowMap();
          return true;

        case "map off":
          currentRoom.roomMap.ChangeMapVisibility(false); //hide map

          GameConsole.WriteLine("Map is no longer visible", font: FontTheme.Danger);

          return true;

        //there has to be cleaner way to write this
        case "north" when isMissionStarted == true:
        case "south" when isMissionStarted == true:
        case "east" when isMissionStarted == true:
        case "west" when isMissionStarted == true:
        case "n" when isMissionStarted == true:
        case "s" when isMissionStarted == true:
        case "w" when isMissionStarted == true:
        case "e" when isMissionStarted == true:
          currentRoom?.roomMap.MoveOnMap(command.Name);
          return true;

        case "chose mission" when isMissionStarted == false:
          GameConsole.Clear();
          switch (Hub.SelectMission())
          {
            case 0:
              GameConsole.WriteLine("Move to Europe"); //for future europe room, doesnt do anything yet
              break;
            case 1:
              AsiaRoom.AsiaMission = true;
              Move("asia", ref currentRoom, ref previousRoom);
              break;
            case 2:
              Move("africa", ref currentRoom, ref previousRoom);
              break;
            case 3:
              GameConsole.WriteLine("Move to Pacific");
              Move("pacific", ref currentRoom, ref previousRoom);
              break;
            default:
              GameConsole.WriteLine("Invalid choice");
              break;

          }
          return true;

        //case "asia" when isMissionStarted == false:
        //case "africa" when isMissionStarted == false:
        case "camp" when isMissionStarted == true && missionName == "africa":
          Move(command.Name, ref currentRoom, ref previousRoom);
          return true;

        case "hub" when isMissionStarted == true:
          return GetQuitConfirmation();

        case "help":
          if (isMissionStarted == true)
          {
            Messages.PrintMissionHelp();
          }
          else
          {
            Messages.PrintHelp();
          }
          return true;

        case "clear":
          GameConsole.Clear();
          return true;

        case "quit":
          return GetQuitConfirmation();

        default:
          Messages.PrintUnknownCommandMessage();
          return true;
      }
    }

    private static bool GetQuitConfirmation()
    {
      GameConsole.WriteLine("Are you sure you want quit playing? All progress will be lost!", font: FontTheme.Danger);

      string? inputConfirmation1 = GameConsole.Input("Yes/No");
      if (inputConfirmation1 == "yes" || inputConfirmation1 == "y")
      {
        LoadingAnimation.Loading("Quiting");
        return false;
      }
      else if (inputConfirmation1 != "no" && inputConfirmation1 != "n")
      {
        return GetQuitConfirmation();
      }
      return true;
    }
  }
}
