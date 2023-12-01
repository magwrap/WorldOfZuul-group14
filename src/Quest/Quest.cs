using System.Collections.Generic;
namespace WorldOfZuul
{
  public class Quest
  {
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public List<Quest> Prerequisites { get; set; }

    public Quest(string? title, string? description, List<Quest>? prerequisites = null)
    {
      Title = title;
      Description = description;
      IsCompleted = false;
      Prerequisites = prerequisites ?? new List<Quest>();
    }

    public void MarkCompleted()
    {
      IsCompleted = true;
    }

    public void CompleteQuest()
    {
      if (ArePrerequisitesMet())
      {
        MarkCompleted();
      }
    }

    public void AddPrerequisite(Quest prerequisite)
    {
      Prerequisites.Add(prerequisite);
    }
    public void RemovePrerequisite(Quest quest)
    {
      Prerequisites.RemoveAll(prerequisite => prerequisite.Title == quest.Title);
    }
    public bool ArePrerequisitesMet()
    {
      if (Prerequisites.Count == 0)
      {
        return true; // No prerequisites to be met
      }
      return Prerequisites.All(prerequisite => prerequisite.IsCompleted);
    }
  }
}