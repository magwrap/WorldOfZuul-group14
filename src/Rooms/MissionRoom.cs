using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldOfZuul
{
  public class MissionRoom : Room
  {
    /// <summary>
    /// Extension of normal room with extra properties like MissionDescription etc...
    /// </summary>
    public string? MissionDescription { get; private set; }
    public string? MessageOnArrival { get; private set; }
    public MissionRoom(
        string? shortDesc,
        string? longDesc,
        string? missionDesc,
        string? msgOnArrival,
        Map? map = null
    ) : base(shortDesc, longDesc, map)
    {
      MissionDescription = missionDesc ?? "";
      MessageOnArrival = msgOnArrival ?? "";
      RoomMap = map ?? new Map();
    }
    public void DisplayMissionDesc()
    {
      GameConsole.WriteLine(MissionDescription, font: FontTheme.HighligtedText);
    }

    public void DisplayMessageOnArrival()
    {
      GameConsole.WriteLine(MessageOnArrival, fgColor: ConsoleColor.DarkYellow);
    }
  }
}