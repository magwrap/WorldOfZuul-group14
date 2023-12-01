﻿namespace WorldOfZuul
{
  public class Room
  {
    public string ShortDescription { get; private set; }
    public string LongDescription { get; private set; }

    public Map RoomMap { get; set; }
    public Dictionary<string, Room> Exits { get; private set; } = new();

    public Room(string? shortDesc, string? longDesc, Map? map = null)
    {
      RoomMap = map ?? new();
      ShortDescription = shortDesc ?? "No short desc";
      LongDescription = longDesc ?? "No long desc";
    }

    // public void SetExits(Room? north, Room? east, Room? south, Room? west)
    // {
    //   SetExit("north", north);
    //   SetExit("east", east);
    //   SetExit("south", south);
    //   SetExit("west", west);
    // }

    public void SetExit(string direction, Room? neighbor)
    {
      if (neighbor != null)
        Exits[direction] = neighbor;
    }
  }
}
