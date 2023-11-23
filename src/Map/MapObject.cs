using System.Security.Cryptography.X509Certificates;

namespace WorldOfZuul
{
    class MapObject
    {
        private int MapCordX { get; set; }
        private int MapCordY { get; set; }
        private string? MapCharacter { get; set; }

        public MapObject(int mapCordX, int mapCordY, string mapCharacter)
        {
            this.MapCordX = mapCordX; //x coordinate of the map object
            this.MapCordY = mapCordY; //y coordinate of the map object
            this.MapCharacter = mapCharacter; //character representing an object on the map e.g. '^' - building, 'X' - Enemies or obstacles... --> create a legend of the map
        }

    }
}