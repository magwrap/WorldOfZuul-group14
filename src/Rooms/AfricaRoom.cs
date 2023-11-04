using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldOfZuul.Africa
{
  public class AfricaRoom : Room
  {
    private protected JsonFileReader jsonFileReader = new();

    public AfricaRoom(string? shortDesc = "", string? longDesc = "") : base(shortDesc, longDesc)
    {
      //Create africa rooms
    }
  }
}