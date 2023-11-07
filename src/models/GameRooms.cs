using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldOfZuul
{
  public class GameRooms
  {
    /// <summary>
    /// Model for room fetched from json file
    /// </summary>
    public GameRoom[]? Rooms { get; set; }
  }

  public class GameRoom
  {
    public int? Id { get; set; }
    public string? Name { get; set; }
    public int? North { get; set; }
    public int? West { get; set; }
    public int? East { get; set; }
    public int? South { get; set; }
    public string? ShortDesc { get; set; }
    public string? LongDesc { get; set; }
    public string? MessageOnArrival { get; set; }

  }
}