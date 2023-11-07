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
    public MissionRoom(
        string? shortDesc,
        string? longDesc,
        string? msgOnArrival,
        string? missionDesc
    ) : base(shortDesc, longDesc)
    {
      MissionDescription = missionDesc ?? "";
    }
    public void DisplayMissionDesc()
    {
      GameConsole.WriteLine(MissionDescription, fgColor: ConsoleColor.DarkYellow);
    }

  }
}