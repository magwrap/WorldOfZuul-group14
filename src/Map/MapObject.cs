using System.Diagnostics.Contracts;
using WorldOfZuul;

namespace WorldOfZuul
{
  public class MapObject
  {
    public int MapCordX { get; set; }
    public int MapCordY { get; set; }
    public MapObjectsEnum? MapObjectType { get; set; }
    private string? OccupiedMessage { get; set; }
    public Quest? Quest { get; set; }
    private bool IsRemovable { get; set; }
    private bool IsImpassable { get; set; }

    public NPC? Npc { get; set; }


    //https://symbl.cc/en/unicode/table/#avestan
    public static readonly Dictionary<MapObjectsEnum, string> MapMarkers = new()
    {
      [MapObjectsEnum.NPC] = "߽", //'#' //\U0001F464 //\U0001F6C9 //U0001F5E3 //\U0001F574 //\U0001F5E3
      [MapObjectsEnum.ENEMY] = "\U000026A0", //"\U00003020",  //\U0001F5F4 
      [MapObjectsEnum.PLACE] = "\U000023CF", //'^' //⏏
      [MapObjectsEnum.PRISON] = "\U000026EB", //⛫
      [MapObjectsEnum.TREE] = "♧",
      [MapObjectsEnum.TRAP] = "¤",
      [MapObjectsEnum.GIRAFFE] = "🦒",
      [MapObjectsEnum.VERTICALWALL] = "\U0000258F", //U0000275A //\U0000258F //'|'
      [MapObjectsEnum.HORIZONTALWALL] = "\U00002E0F", //U0000268A //U00002594 //\U00002581 //\U00002015 //\U00002D67 //\U00002E0F//'-' 
      [MapObjectsEnum.DIAGONALWALL_LEFT] = "\\",
      [MapObjectsEnum.DIAGONALWALL_RIGHT] = "/"

    };

    public static readonly Dictionary<MapObjectsEnum, FontTheme> MapObjectFonts = new()
    {
      [MapObjectsEnum.NPC] = FontTheme.NPC,
      [MapObjectsEnum.ENEMY] = FontTheme.Danger,
      [MapObjectsEnum.PLACE] = FontTheme.HighligtedText,
      [MapObjectsEnum.PRISON] = FontTheme.Default,
      [MapObjectsEnum.TREE] = FontTheme.Default,
      [MapObjectsEnum.TRAP] = FontTheme.Danger,
      [MapObjectsEnum.GIRAFFE] = FontTheme.NPC,
      [MapObjectsEnum.VERTICALWALL] = FontTheme.Wall,
      [MapObjectsEnum.HORIZONTALWALL] = FontTheme.Wall,
      [MapObjectsEnum.DIAGONALWALL_LEFT] = FontTheme.Wall,
      [MapObjectsEnum.DIAGONALWALL_RIGHT] = FontTheme.Wall

    };

    public MapObject(
      int mapCordX,
      int mapCordY,
      MapObjectsEnum? mapObjectType,
      bool isRemovable = false,
      bool isImpassable = true, // on default items will be Impassable and Unremovable
      string? occupiedMessage = null,
      Quest? quest = null,
      NPC? npc = null)
    {

      // X has to be odd number bcs. user moves 2 fields at the time
      if (mapCordX % 2 == 0 && (mapObjectType is MapObjectsEnum.NPC || mapObjectType is MapObjectsEnum.PLACE)) throw new ArgumentException("mapCordX has to be an odd number!");//cant be used with walls

      this.MapCordX = mapCordX;
      this.MapCordY = mapCordY;
      this.MapObjectType = mapObjectType;
      this.OccupiedMessage = occupiedMessage;
      this.Quest = quest;
      this.IsRemovable = isRemovable;
      this.IsImpassable = isImpassable;
      this.Npc = npc;

      if ((MapObjectType is MapObjectsEnum.NPC //|| MapObjectType is MapObjectsEnum.ENEMY
                                               //|| MapObjectType is MapObjectsEnum.PLACE 
       ) && Npc == null)
      {
        throw new ArgumentException("If you're creating person map object you have to pass NPC/Enemy object");
      }
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
        Console.WriteLine(OccupiedMessage + "\n");
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
