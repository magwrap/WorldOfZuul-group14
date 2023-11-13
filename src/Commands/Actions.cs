
namespace WorldOfZuul
{
  public class Actions
  {

    //action moving player to different room
    public static void Move(string direction, ref Room? currentRoom, ref Room? previousRoom)
    {
      if (currentRoom?.Exits.ContainsKey(direction) == true)
      {
        previousRoom = currentRoom;
        currentRoom = currentRoom?.Exits[direction];
      }
      else
      {
        Messages.PrintWrongDirectionMessage();
      }
    }

    //returns boolean deciding if the game should continue
    public static bool DecideAction(ref Command? command, ref Room? currentRoom, ref Room? previousRoom)
    {
      switch (command?.Name)
      {
        case "look":
          GameConsole.WriteLine(currentRoom?.LongDescription, bgColor: ConsoleColor.DarkYellow);
          return true;

        case "back":
          if (previousRoom == null)
            GameConsole.WriteLine("You can't go back from here!");
          else
            currentRoom = previousRoom;
          return true;

        case "north":
        case "south":
        case "east":
        case "west":
          Move(command.Name, ref currentRoom, ref previousRoom);
          return true;

        case "help":
          Messages.PrintHelp();
          return true;

        case "quit":
          return false;

        default:
          Messages.PrintUnknownCommandMessage();
          return true;
      }
    }
  }
}
