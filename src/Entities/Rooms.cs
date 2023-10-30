namespace WorldOfZuul
{
  public class Rooms
  {
    private protected Room? outside;
    private protected Room? theatre;
    private protected Room? pub;
    private protected Room? lab;
    private protected Room? office;
    public Room? CurrentRoom { get; set; }
    public Room? PreviousRoom { get; set; }

    public void CreateRooms()
    {
      //creates rooms and returns the current one
      outside = new("Outside", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.");
      theatre = new("Theatre", "You find yourself inside a large lecture theatre. Rows of seats ascend up to the back, and there's a podium at the front. It's quite dark and quiet.");
      pub = new("Pub", "You've entered the campus pub. It's a cozy place, with a few students chatting over drinks. There's a bar near you and some pool tables at the far end.");
      lab = new("Lab", "You're in a computing lab. Desks with computers line the walls, and there's an office to the east. The hum of machines fills the room.");
      office = new("Office", "You've entered what seems to be an administration office. There's a large desk with a computer on it, and some bookshelves lining one wall.");

      outside.SetExits(null, theatre, lab, pub); // North, East, South, West

      theatre.SetExit(DirectoriesEnum.WEST, outside);

      pub.SetExit(DirectoriesEnum.EAST, outside);

      lab.SetExits(outside, office, null, null);

      office.SetExit(DirectoriesEnum.WEST, lab);

      CurrentRoom = outside;
    }
  }
}
