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
    private bool IsImpassable { get; set; }

    private static readonly Dictionary<MapObjectsEnum, string> MapMarkers = new()
    {
      [MapObjectsEnum.NPC] = "#",
      [MapObjectsEnum.ENEMY] = "X",
      [MapObjectsEnum.PLACE] = "\U00002302", //'^'
      [MapObjectsEnum.ITEM] = "!",
      [MapObjectsEnum.VERTICALWALL] = "\U0000258F", //U0000275A //\U0000258F //'|'
      [MapObjectsEnum.HORIZONTALWALL] = "\U00002E0F", //U0000268A //U00002594 //\U00002581 //\U00002015 //\U00002D67 //\U00002E0F//'-' 

    };

    private static readonly Dictionary<MapObjectsEnum, FontTheme> MapObjectFonts = new()
    {
      [MapObjectsEnum.NPC] = FontTheme.NPC,
      [MapObjectsEnum.ENEMY] = FontTheme.Danger,
      [MapObjectsEnum.PLACE] = FontTheme.HighligtedText,
      [MapObjectsEnum.ITEM] = FontTheme.NewItem,
      [MapObjectsEnum.VERTICALWALL] = FontTheme.Wall,
      [MapObjectsEnum.HORIZONTALWALL] = FontTheme.Wall,

    };

    public MapObject(int mapCordX, int mapCordY, MapObjectsEnum? mapObjectType, bool isRemovable, bool isImpassable, string? occupiedMessage = null, Quest? quest = null)
    {

      // X has to be odd number bcs. user moves 2 fields at the time
      if (mapCordX % 2 == 0) throw new ArgumentException("mapCordX has to be an odd number!");

      this.MapCordX = mapCordX;
      this.MapCordY = mapCordY;
      this.MapObjectType = mapObjectType;
      this.OccupiedMessage = occupiedMessage;
      this.Quest = quest;
      this.IsRemovable = isRemovable;
      this.IsImpassable = isImpassable;

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
    public bool CannotPassTheObject()
    {
      return IsImpassable;
    }
  }
}
