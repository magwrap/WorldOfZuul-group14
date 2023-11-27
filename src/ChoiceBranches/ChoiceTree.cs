using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldOfZuul
{
  public class ChoiceTree
  {
    public Dictionary<string, ChoiceBranch> Branches = new();

    public void AddBranch(ChoiceBranch branch)
    {
      Branches[branch.Name] = branch;
    }

    public void AddMultipleBranches(ChoiceBranch[] branches)
    {
      foreach (var branch in branches)
      {
        Branches[branch.Name] = branch;
      }
    }
  }
}