namespace WorldOfZuul
{
  public class Room
  {
    public string ShortDescription { get; private set; }
    public string LongDescription { get; private set; }
    public Dictionary<DirectoriesEnum, Room> Exits { get; private set; } = new();

    public Room(string? shortDesc, string? longDesc)
    {
      ShortDescription = shortDesc ?? "";
      LongDescription = longDesc ?? "";
    }

    public void SetExits(Room? north, Room? east, Room? south, Room? west)
    {
      SetExit(DirectoriesEnum.NORTH, north);
      SetExit(DirectoriesEnum.EAST, east);
      SetExit(DirectoriesEnum.SOUTH, south);
      SetExit(DirectoriesEnum.WEST, west);
    }

    public void SetExit(DirectoriesEnum direction, Room? neighbor)
    {
      if (neighbor != null)
        Exits.Add(direction, neighbor);
    }
  }
}
