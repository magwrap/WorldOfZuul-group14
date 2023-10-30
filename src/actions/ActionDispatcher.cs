namespace WorldOfZuul
{
  public class ActionDispatcher
  {
    private static readonly MessagePrinter messagePrinter = new();

    public delegate bool ActionMethod(Room currentRoom, Room? previousRoom, string[]? arguments = null);

    private readonly Rooms? rooms;

    private readonly Dictionary<ActionsEnum, ActionMethod> actions;


    public ActionDispatcher(ref Rooms passedRooms)
    {
      rooms = passedRooms;

      actions = new()
     {
        {
          ActionsEnum.LOOK, delegate(
              Room currentRoom,  Room? previousRoom,
             string[]? arguments
          )
          {
            GameConsole.WriteLine(currentRoom?.LongDescription, bgColor: ConsoleColor.DarkGreen);
            return true;
          }
        },
        {
          ActionsEnum.BACK, delegate(
              Room currentRoom,  Room? previousRoom,
             string[]? arguments
          )
          {
            if (previousRoom == null)
            {
              GameConsole.WriteLine("You can't go back from here!");
            }
            else
            {
              SetPreviousRoom(currentRoom);
              SetCurrentRoom(previousRoom);
            }

          return true;
          }
        },
        {
          ActionsEnum.MOVE,  delegate (
             Room currentRoom,  Room? previousRoom,
            string[]? arguments
          )
          {
            if (arguments != null)
            {
              DirectoriesEnum directionsKey = (DirectoriesEnum)Enum.Parse(typeof(DirectoriesEnum), arguments[0]);

              if (currentRoom?.Exits.ContainsKey(directionsKey) == true)
              {
                SetPreviousRoom(currentRoom);
                SetCurrentRoom(currentRoom.Exits[directionsKey]);
              }
              else
              {
                GameConsole.WriteLine($"You can't go {arguments[0]}!");
              }
            }
            return true;
          }
        },
        {
          ActionsEnum.HELP, delegate(
             Room currentRoom,  Room? previousRoom,
            string[]? arguments
          )
          {
             messagePrinter.PrintHelp();
            return true;
          }
        },
        {
          ActionsEnum.QUIT, delegate(
             Room currentRoom,  Room? previousRoom,
            string[]? arguments
          )
          {
             return false;
          }
        }
      };
    }


    public Dictionary<ActionsEnum, ActionMethod> GetActions()
    {
      return actions;
    }

    public bool DecideAction(ref Command? command)
    {
      if (command != null && rooms != null && rooms.CurrentRoom != null)
      {
        ActionsEnum actionKey = (ActionsEnum)Enum.Parse(typeof(ActionsEnum), command.Name);
        return actions[actionKey](rooms.CurrentRoom, rooms.PreviousRoom, command.Arguments);
      }

      throw new ArgumentException("Parameter cannot be null", nameof(command));
    }
    private void SetCurrentRoom(Room room)
    {
      if (rooms == null) return;
      rooms.CurrentRoom = room;
    }
    private void SetPreviousRoom(Room room)
    {
      if (rooms == null) return;
      rooms.PreviousRoom = room;
    }
  }

}