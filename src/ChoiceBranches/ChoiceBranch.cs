using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using WorldOfZuul;
namespace WorldOfZuul
{
  public class ChoiceBranch : ChoiceTree
  {
    public int Key { get; set; }
    public string Content { get; set; }
    private readonly List<string> Dialogs = new();

    private string Prompt = "";

    public override string ToString()
    {
      return Content;
    }

    public string GetDialogMessage()
    {
      return Content;
    }


    /// <summary>
    ///  Class for branching choices in the game
    /// </summary>
    /// ///<param name="branchNr">Nr of a branch. Must be bigger than 0</param>
    /// <param name="branchConent">the response this branch will give you</param>
    /// <param name="choices">
    /// array of tuples constructed of dialog option and result of it
    /// 
    ///   Example:   Dialog Option,   resultingBranch
    ///             ("talk",        new ChoiceBranch("Hello Let's talk"))
    ///             
    ///   Now more complex Example:
    ///             Dialog Option,    resulting branch                                 More branches steming from this one
    ///             ("Fight",         new ChoiceBranch("He hits you in the face",      ("hit him back", new ChoiceBranch("he falls down"))))
    /// </param>
    public ChoiceBranch(
        int branchNr, // value bigger than 0
        string branchConent,
        DialogOption[]? choices = null
      // ChoiceBranch[]? choiceBranchChildren = null,
      // string[]? dialogs = null
      )
    {
      Content = branchConent ?? "";
      Key = --branchNr;

      if (choices != null)
      {
        foreach ((var choice, var resultBranch) in choices)
        {
          if (choice != null)
            AddDialog(choice);

          if (resultBranch != null)
            AddBranch(resultBranch);
        }
      }
    }

    public void AddDialog(string choice)
    {
      Dialogs.Add(choice);
    }

    public void AddMultipleDialogs(string[] dialogs, string prompt = "Your dialog option: ")
    {
      Prompt = prompt;
      foreach (string dialog in dialogs)
      {
        Dialogs.Add(dialog);
      }
    }

    public void StartDialog()
    {
      GameConsole.WriteLine(this.GetDialogMessage(), font: FontTheme.NPC);

      if (Dialogs.Count == 0) return;
      int userOption = GameConsole.GetUserOption(Dialogs.ToArray<string>(), Prompt);
      //TODO: somehow pass the font type from the parent object

      Branches[userOption].StartDialog();
    }

    public bool IsEndOfTree()
    {
      return Branches.Count == 0;
    }
  }

  //dont mind this block of code - it's auto generated just for easier typing
  //basically it's just an alias for tuple looking like: (string? choice, ChoiceBranch? result)
  public record struct DialogOption(string? choice, ChoiceBranch? result)
  {
    public static implicit operator DialogOption((string? choice, ChoiceBranch? result) value)
    {
      return new DialogOption(value.choice, value.result);
    }
  }
}
