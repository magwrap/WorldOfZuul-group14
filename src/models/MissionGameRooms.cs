using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldOfZuul
{
  public class MissionGameRooms : GameRooms
  {
    /// <summary>
    /// Model for mission room inhereting GameRooms and adding extra properties
    /// </summary>
    public new MissionGameRoom[]? Rooms { get; set; }
  }

  public class MissionGameRoom : GameRoom
  {
    public string? MissionDescription { get; set; }
    public string[]? Actions { get; set; }
  }
}