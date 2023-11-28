using WorldOfZuul;

namespace WorldOfZuul
{
  public class Map
  {
    private int position_x = 1;
    private int position_y = 1;
    private bool mapVisible = true;
    private readonly int heightOfMap;
    private readonly int widthOfMap;
    private Dictionary<(int X, int Y), MapObject> mapObjects = new Dictionary<(int, int), MapObject>();

    public Map(
      int height = 10,
      int width = 32
    )
    {
      heightOfMap = height;
      widthOfMap = width;
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
      //TODO: add moving a few fields at once for example: south 5, moves you 5 fields down

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

      GameConsole.WriteLine($"Player pos: {newPositionX} {newPositionY}");

      if (BoundsOfTheMap(newPositionX, newPositionY))
      {
        bool isOccupied = IsCoordinateOccupied(newPositionX, newPositionY, out MapObject? occupyingObject);

        if (occupyingObject != null && occupyingObject.CannotPassTheObject())
        {
          // Move the player back to the previous position
          if (MapVisibility)
          {
            ShowMap();
          }
          System.Console.WriteLine(position_x);
          System.Console.WriteLine(position_y);

          GameConsole.WriteLine("You can't pass through here!", font: FontTheme.Danger);
        }
        else
        { 
          position_x = newPositionX;
          position_y = newPositionY;
          if (MapVisibility)
          {
            ShowMap();
          }
        }


        if (isOccupied)
        {
          occupyingObject?.DisplayOccupiedMessage();

          if (occupyingObject?.Quest != null)
          {
            occupyingObject.Quest.CompleteCurrentQuest();

            // Remove the map object associated with the completed quest
            if (occupyingObject.RemoveAfterCompletition())
            {
              RemoveMapObject(newPositionX, newPositionY);
              GameConsole.WriteLine("I got here");
            }
          }
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

      return true;
    }

    /// <summary>
    /// Function displaying room map
    /// </summary>
    public void ShowMap()
    {
      int rows = heightOfMap + 1; //size of the map rows N/S, added +1 to avoid the bug of going out of the map :)
      int columns = widthOfMap; //size of the map columns W/E
      GameConsole.WriteLine($"x: {rows}, y: {columns}");

      for (int i = 0; i <= rows; i++) //int i are for x coordinates  
      {
        for (int j = 0; j <= columns; j++) //int j are for y coordinates 
        {
          DecideWhatCharacterToWrite(i, j);
        }
        GameConsole.WriteLine();
      }
    }

    /// <summary>
    /// Function that decides which character it has to write onto the map
    /// </summary>
    /// <param name="i">y coordinate of map</param>
    /// <param name="j">x coordinate of map</param>
    private void DecideWhatCharacterToWrite(int i, int j)
    {
      bool isCoordinateOccupiedByPlayer = position_x == j && position_y == i;

      if (i == 0 || i == heightOfMap + 1)
      {
        GameConsole.Write("-");
      }
      else if (j == 0)
      {
        GameConsole.Write("[");
      }
      else if (j == widthOfMap)
      {
        string mapLabel = i switch
        {
          1 => "]    {N}",
          2 => "] <{W}:{E}>",
          3 => "]    {S}",
          _ => "]"
        };
        GameConsole.Write(mapLabel);
      }

      else if (IsCoordinateOccupied(j, i, out MapObject? occupyingObject))
      {
        occupyingObject?.DisplayMapObject(isCoordinateOccupiedByPlayer);
      }

      else if (isCoordinateOccupiedByPlayer)
      {
        GameConsole.Write(Game.Initials, font: FontTheme.Player);
        //inicials taken from the playes name at the beggining of the game, 
        //players initials are shown on the map
      }
      else
      {
        GameConsole.Write(" ");
      }
    }


    /// <summary>
    /// This function takes an array of mapObjects and adds them onto the map
    /// </summary>
    /// <param name="objects">
    /// array of MapObjects you want to add onto the map
    /// </param>
    /// 
    public void PopulateMap(MapObject[] objects)
    {
      foreach (var mapObject in objects)
      {
        AddMapObject(mapObject);
      }

      foreach (var item in mapObjects)
      {
        GameConsole.WriteLine($"Object at ({item.Key.X}, {item.Key.Y}): {item.Value}");
      }
    }

    /// <summary>
    /// Function for adding one map object onto the map
    /// </summary>
    /// <param name="mapObject">Pass MapObject with set x and y coordinates</param>
    public void AddMapObject(MapObject mapObject)
    {
      mapObjects[(mapObject.MapCordX, mapObject.MapCordY)] = mapObject;
    }

    /// <summary>
    /// Function for removing object at coordinates x and y
    /// </summary>
    /// <param name="x"> x coordinate of a map object</param>
    /// <param name="y">y coordinate of a map object</param>
    public void RemoveMapObject(int x, int y)
    {
      if (mapObjects.ContainsKey((x, y)))
      {
        mapObjects.Remove((x, y));
      }
    }

    /// <summary>
    /// Function checking if on current coordinate exists an object
    /// </summary>
    /// <param name="x">x coordinate to check</param>
    /// <param name="y">y coordinate to chck</param>
    /// <param name="occupyingObject">function writes passed object if coordinate is occupied</param>
    /// <returns>return bool wheter map object exists on passed coordinate or not</returns>
    public bool IsCoordinateOccupied(int x, int y, out MapObject? occupyingObject)
    {
      if (mapObjects.TryGetValue((x, y), out occupyingObject))
      {
        return true;
      }

      occupyingObject = null;
      return false;
    }
  }
}