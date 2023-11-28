using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldOfZuul
{
  public class ChoiceTree
  {
    public Dictionary<int, ChoiceBranch> Branches = new();

    public void AddBranch(ChoiceBranch branch)
    {
      Branches[branch.Key] = branch;
    }

    public void AddMultipleBranches(ChoiceBranch[] branches)
    {
      foreach (var branch in branches)
      {
        Branches[branch.Key] = branch;
      }
    }
  }
}