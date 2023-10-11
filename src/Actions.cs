namespace WorldOfZuul
{
  public class Actions
  {
    private protected MessagePrinter messagePrinter;
    private protected Room? outside;
    private protected Room? theatre;
    private protected Room? pub;
    private protected Room? lab;
    private protected Room? office;

    public Actions(ref MessagePrinter msgPrinter)
    {
      messagePrinter = msgPrinter;
    }

    //action moving player to different room
    static void Move(string direction, ref Room? currentRoom, ref Room? previousRoom)
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

    //returns boolean deciding if the game should continue
    public bool DecideAction(ref Command? command, ref Room? currentRoom, ref Room? previousRoom)
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
          messagePrinter.PrintHelp();
          return true;

        case "quit":
          return false;

        default:
          GameConsole.WriteLine("I don't know what command.");
          return true;
      }
    }

    public Room CreateRooms()
    {
      //creates rooms and returns the current one
      outside = new("Outside", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.");
      theatre = new("Theatre", "You find yourself inside a large lecture theatre. Rows of seats ascend up to the back, and there's a podium at the front. It's quite dark and quiet.");
      pub = new("Pub", "You've entered the campus pub. It's a cozy place, with a few students chatting over drinks. There's a bar near you and some pool tables at the far end.");
      lab = new("Lab", "You're in a computing lab. Desks with computers line the walls, and there's an office to the east. The hum of machines fills the room.");
      office = new("Office", "You've entered what seems to be an administration office. There's a large desk with a computer on it, and some bookshelves lining one wall.");

      outside.SetExits(null, theatre, lab, pub); // North, East, South, West

      theatre.SetExit("west", outside);

      pub.SetExit("east", outside);

      lab.SetExits(outside, office, null, null);

      office.SetExit("west", lab);

      return outside;
    }
  }
}
