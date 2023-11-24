using System.Text;
using WorldOfZuul;
using WorldOfZuul.src.Map;

namespace WorldOfZuul
{
  public class Map
  {
    private int position_x = 1;
    private int position_y = 1;
    private bool mapVisible = true;
    private readonly int heightOfMap;
    private readonly int widthOfMap;
    private readonly List<List<string>> map = new();
    public delegate void MapObjectAction();
    private static readonly (string npc, string item, string enemy) mapMarkers = ("?", "$", "!");
    private (int row, int col, MapObjectsEnum typeOfObject, MapObjectAction? action)[]? mapObjects = null;
    public Map(
      int height = 11,
      int width = 42
    )
    {
      heightOfMap = height;
      widthOfMap = width;
      BuildMap();
    }

    public void BuildMap()
    {
      int rows = heightOfMap; //size of the map rows N/S
      int columns = widthOfMap; //size of the map columns W/E

      for (int i = 0; i <= rows; i++)
      {
        map.Add(new List<string> { });

        for (int j = 0; j <= columns; j++)
        {
          if (i == 0 || i == rows)
          {
            map[i].Add("-");
          }
          else if (j == columns)
          {
            string mapLabel = i switch
            {
              1 => "]    {N}",
              2 => "] <{W}:{E}>",
              3 => "]    {S}",
              _ => "]"
            };
            map[i].Add(mapLabel);
          }
          else if (j == 0)
          {

            map[i].Add("[");
          }
          else
          {
            map[i].Add(" ");
          }
        }
      }

      map[position_y][position_x] = Game.Initials ?? "X";
    }

    public bool MapVisibility
    {
      get { return mapVisible; }
    }

    public int PositionX
    {
      get { return position_x; }
    }

    public int PositionY
    {
      get { return position_y; }
    }

    public bool ChangeMapVisibility(bool changeVisibility)
    {
      return mapVisible = changeVisibility;
    }

    public void MoveOnMap(string direction)
    {
      int newPositionX = position_x;
      int newPositionY = position_y;

      switch (direction)
      {
        case "north":
        case "n":
          newPositionY--;
          break;
        case "south":
        case "s":
          newPositionY++;
          break;
        case "west":
        case "w":
          newPositionX -= 2;
          break;
        case "east":
        case "e":
          newPositionX += 2;
          break;
      }

      if (BoundsOfTheMap(newPositionX, newPositionY))
      {
        //remove player from the map
        map[position_y][position_x] = " ";

        //move player
        position_x = newPositionX;
        position_y = newPositionY;

        //put the player on the map
        map[position_y][position_x] = Game.Initials ?? "X";

        if (MapVisibility) // Check if the map is visible before showing it
        {
          ShowMap();
        }

      }
      else
      {
        GameConsole.WriteLine("Out of the bounds of the map! Try another direction.", font: FontTheme.Danger);
      }
    }

    public bool BoundsOfTheMap(int x, int y)
    {

      if (x < 1 || x > widthOfMap || y < 1 || y > heightOfMap)
      {
        return false;
      }

      //checks if the field you want to move to is not a mapObject
      string field = map[y][x];
      if (field == mapMarkers.npc || field == mapMarkers.item || field == mapMarkers.enemy)
      {
        //if player tries to walk 'into' the object he automatically dispatches this objects action

        for (int i = 0; i < mapObjects?.Length; i++)
        {
          //if the field indeed is a mapObject then it invokes it's action and doesn't let player to move there
          if (mapObjects[i].row == y && mapObjects[i].col == x)
          {
            mapObjects[i].action?.Invoke();
          }
        }

        return false;
      }

      return true;
    }

    /// <summary>
    /// This function takes an array of mapObjects and adds them onto the map
    /// </summary>
    /// <param name="objects">
    /// array of values: row, columnt, typeOfObject(NPC, ITEM), and optional action you want to be dispatched when player interacts with the object
    /// </param>
    public void PopulateMap((int row, int col, MapObjectsEnum typeOfObject, MapObjectAction? action)[] objects)
    {
      mapObjects = objects;

      foreach (var (row, col, typeOfObject, action) in mapObjects)
      {
        map[row][col] = typeOfObject switch
        {
          MapObjectsEnum.NPC => mapMarkers.npc,
          MapObjectsEnum.ITEM => mapMarkers.item,
          MapObjectsEnum.ENEMY => mapMarkers.enemy,
          _ => "",
        };
      }
    }

    public void ShowMap()
    {
      int rows = heightOfMap; //size of the map rows N/S
      int columns = widthOfMap; //size of the map columns W/E
      for (int i = 0; i <= rows; i++)
      {
        for (int j = 0; j <= columns; j++)
        {
          Console.Write(map[i][j]);
        }

        Console.WriteLine();
      }
    }
  }
}