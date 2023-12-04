using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfZuul
{
  public class CommandWords
  {
    public List<string> ValidCommands { get; } = new List<string> { "north", "east", "south", "west", "n", "e", "w", "s", "map on", "map off", "help", "look", "back", "quit", "hub", "talk", "clear", "asia", "africa", "camp", "choose mission" };

    public bool IsValidCommand(string command)
    {
      return ValidCommands.Contains(command);
    }
  }

}
