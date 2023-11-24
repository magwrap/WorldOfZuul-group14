namespace WorldOfZuul
{
    public class MapObject
    {
        private int MapCordX { get; set; }
        private int MapCordY { get; set; }
        private string? MapCharacter { get; set; }
        private string? OccupiedMessage { get; set; }
        public Quest? Quest { get; set; }
        private bool IsRemovable { get; set; }

        public MapObject(int mapCordX, int mapCordY, string? mapCharacter, bool isRemovable, string? occupiedMessage = null, Quest? quest = null)
        {
            this.MapCordX = mapCordX;
            this.MapCordY = mapCordY;
            this.MapCharacter = mapCharacter;
            this.OccupiedMessage = occupiedMessage;
            this.Quest = quest;
            this.IsRemovable = isRemovable;

        }

        public void DisplayMapObject()
        {
            Console.Write(MapCharacter);
        }

        public void DisplayOccupiedMessage()
        {
            if (!string.IsNullOrEmpty(OccupiedMessage))
            {
                Console.WriteLine(OccupiedMessage);
            }
            // if (Quest != null)
            // {
            //     if (Quest.ArePrerequisitesMet())
            //     {
            //         Quest.MarkCompleted();
            //         Console.WriteLine("Quest Information: " + Quest.Title);
            //     }
            //     else
            //     {
            //         Console.WriteLine("Cannot complete the quest. Prerequisites not met.");
            //     }
            // }
        }
        
        public bool RemoveAfterCompletition()
        {
            return IsRemovable;
        }
    }
}
