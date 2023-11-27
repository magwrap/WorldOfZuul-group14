using System.Diagnostics.Contracts;
using WorldOfZuul;

namespace WorldOfZuul
{
  public class MapObject
  {
    public int MapCordX { get; set; }
    public int MapCordY { get; set; }
    private MapObjectsEnum? MapObjectType { get; set; }
    private string? OccupiedMessage { get; set; }
    public Quest? Quest { get; set; }
    private bool IsRemovable { get; set; }

    private static readonly Dictionary<MapObjectsEnum, string> MapMarkers = new()
    {
      [MapObjectsEnum.NPC] = "#",
      [MapObjectsEnum.ENEMY] = "X",
      [MapObjectsEnum.PLACE] = "^",
      [MapObjectsEnum.ITEM] = "!",
    };

    private static readonly Dictionary<MapObjectsEnum, FontTheme> MapObjectFonts = new()
    {
      [MapObjectsEnum.NPC] = FontTheme.NPC,
      [MapObjectsEnum.ENEMY] = FontTheme.Danger,
      [MapObjectsEnum.PLACE] = FontTheme.HighligtedText,
      [MapObjectsEnum.ITEM] = FontTheme.NewItem,
    };

    public MapObject(int mapCordX, int mapCordY, MapObjectsEnum? mapObjectType, bool isRemovable, string? occupiedMessage = null, Quest? quest = null)
    {

      // X has to be odd number bcs. user moves 2 fields at the time
      if (mapCordX % 2 == 0) throw new ArgumentException("mapCordX has to be an odd number!");

      this.MapCordX = mapCordX;
      this.MapCordY = mapCordY;
      this.MapObjectType = mapObjectType;
      this.OccupiedMessage = occupiedMessage;
      this.Quest = quest;
      this.IsRemovable = isRemovable;

    }

    public void DisplayMapObject(bool isPlayerOcuppyingField = false)
    {
      if (MapObjectType != null)
        GameConsole.Write(
          MapMarkers[(MapObjectsEnum)MapObjectType],
          font: isPlayerOcuppyingField ? FontTheme.Player : MapObjectFonts[(MapObjectsEnum)MapObjectType]
        );
    }

    public void DisplayOccupiedMessage()
    {
      if (!string.IsNullOrEmpty(OccupiedMessage))
      {
        Console.WriteLine(OccupiedMessage);
      }
      if (Quest != null)
      {
        if (Quest.ArePrerequisitesMet())
        {
          Quest.MarkCompleted();
          Console.WriteLine("Quest Information: " + Quest.Title);
        }
        else
        {
          Console.WriteLine("Cannot complete the quest. Prerequisites not met.");
        }
      }
    }

    public bool RemoveAfterCompletition()
    {
      return IsRemovable;
    }
  }
}
