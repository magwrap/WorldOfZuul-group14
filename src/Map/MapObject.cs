namespace WorldOfZuul
{
    public class MapObject
    {
        private int MapCordX { get; set; }
        private int MapCordY { get; set; }
        private string? MapCharacter { get; set; }
        private string? OccupiedMessage { get; set; }

        public MapObject(int mapCordX, int mapCordY, string mapCharacter, string? occupiedMessage = null)
        {
            this.MapCordX = mapCordX;
            this.MapCordY = mapCordY;
            this.MapCharacter = mapCharacter;
            this.OccupiedMessage = occupiedMessage;
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
        }
    }
}
