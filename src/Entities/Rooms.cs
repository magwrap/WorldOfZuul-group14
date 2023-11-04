using WorldOfZuul.Africa;

namespace WorldOfZuul
{
  public class Rooms
  {
    private protected Room? hub;
    private protected Room? pacific;
    private protected AfricaRoom? africa;
    private protected Room? asia;
    private protected Room? boss;
    private protected GameRooms? rooms;
    public Room? CurrentRoom
    { get; set; }
    public Room? PreviousRoom { get; set; }

    public void CreateRooms()
    {
      rooms = JsonFileReader.GetMainRooms();

      //creates rooms and returns the current one
      if (rooms == null || rooms.Rooms == null) return;
      hub = new(rooms.Rooms[(int)RoomsEnum.HUB].ShortDesc, rooms.Rooms[(int)RoomsEnum.HUB].LongDesc);
      pacific = new(rooms.Rooms[(int)RoomsEnum.PACIFIC].ShortDesc, rooms.Rooms[(int)RoomsEnum.PACIFIC].ShortDesc);
      africa = new(rooms.Rooms[(int)RoomsEnum.AFRICA].ShortDesc, rooms.Rooms[(int)RoomsEnum.AFRICA].ShortDesc);
      asia = new(rooms.Rooms[(int)RoomsEnum.ASIA].ShortDesc, rooms.Rooms[(int)RoomsEnum.ASIA].ShortDesc);
      boss = new(rooms.Rooms[(int)RoomsEnum.BOSS].ShortDesc, rooms.Rooms[(int)RoomsEnum.BOSS].ShortDesc);

      hub.SetExits(boss, pacific, africa, asia); // North, East, South, West

      CurrentRoom = hub;
    }
  }
}
