using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfZuul
{
  public class ValidCommandWords
  {
    public List<string> ValidCommands { get; } = new List<string> { "north", "east", "south", "west", "look", "help", "back", "quit" };

    public bool IsValidCommand(string command)
    {
      return ValidCommands.Contains(command);
    }
  }
}
