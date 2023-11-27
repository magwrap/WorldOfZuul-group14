using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldOfZuul
{
  public class ChoiceBranch : ChoiceTree
  {
    public string Name { get; set; }
    public string Content { get; set; }

    public override string ToString()
    {
      return Content;
    }
    public ChoiceBranch(string? branchName, string branchConent, ChoiceBranch[]? choiceBranchChildren = null)
    {
      Name = branchName ?? "";
      Content = branchConent ?? "";

      if (choiceBranchChildren != null)
      {
        AddMultipleBranches(choiceBranchChildren);
      }
    }
    public bool IsEndOfTree()
    {
      return Branches.Count == 0;
    }
  }
}