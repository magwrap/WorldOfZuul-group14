using System.Collections.Generic;
namespace WorldOfZuul
{
    public class Quest
    {
        public string? Title { get; set; }
        private string? Description { get; set; }
        public bool IsCompleted { get; set; }

        private Stack<Quest> questStack = new Stack<Quest>();
        public List<Quest> Prerequisites { get; set; }

        public Quest(string? title, string? description, List<Quest>? prerequisites = null)
        {
            Title = title;
            Description = description;
            IsCompleted = false;
            Prerequisites = prerequisites ?? new List<Quest>();

        }

        public Stack<Quest> GetQuestStack()
        {
            return questStack;
        }

        public void AddQuest()
        {
            GetQuestStack().Push(new Quest(Title, Description));
        }

        public void MarkCompleted()
        {
            IsCompleted = true;
        }

        public void CheckQuestCompletion()
        {
            foreach (var quest in questStack)
            {
                if (quest.IsCompleted)
                {
                    GameConsole.WriteLine("Quest Completed: " + quest.Title);
                }
            }
        }

        // Method to pop the top quest off the stack
        public void CompleteCurrentQuest()
        {
            if (questStack.Count > 0)
            {
                Quest currentQuest = GetQuestStack().Pop();

                // Check if prerequisites are met before marking the quest as completed
                if (currentQuest.ArePrerequisitesMet())
                {
                    currentQuest.MarkCompleted();
                    GameConsole.WriteLine("Completed Task: " + currentQuest.Title);
                }
                else
                {
                    GameConsole.WriteLine("Prerequisites not met for: " + currentQuest.Title);
                    // Re-add the quest to the stack if prerequisites are not met
                    GetQuestStack().Push(currentQuest);
                }
            }
            else
            {
                GameConsole.WriteLine("All quests completed!");
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