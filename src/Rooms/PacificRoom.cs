using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldOfZuul
{
  public class PacificRoom : Room
  {

    public PacificRoom(
      string? shortDesc,
      string? longDesc
    // string? msgOnArrival
    ) : base(shortDesc, longDesc)
    {
    }
  }
}