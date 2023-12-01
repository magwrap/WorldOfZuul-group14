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
    public readonly MapEntities mapEntities = new();

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



      if (BoundsOfTheMap(newPositionX, newPositionY))
      {
        bool isOccupied = IsCoordinateOccupied(newPositionX, newPositionY, out MapObject? occupyingObject);

        if (occupyingObject != null && occupyingObject.CannotPassTheObject())
        {
          GameConsole.WriteLine("You can't pass through here!", font: FontTheme.Danger);
        }
        else
        {
          // Player moves to the new position
          position_x = newPositionX;
          position_y = newPositionY;
        }

        if (MapVisibility)
        {
          ShowMap();
        }

        if (isOccupied)
        {
          occupyingObject?.DisplayOccupiedMessage();
          var quest = occupyingObject?.Quest;

          //check if occupyingObjects quest is a current quest, then checks if prerequsites are met
          if (quest != null && quest.Title == mapEntities.GetCurrentQuest()?.Title)
          {
            if (quest.ArePrerequisitesMet())
            {
              //here you can add body of a quest for example talk to a npc, some mini game?
              mapEntities.StartCurrentQuest();
              mapEntities.CompleteCurrentQuest();
            }
            else
            {
              GameConsole.WriteLine("Prerequisites are not met!", font: FontTheme.Danger);
            }
          }
          else
          {
            GameConsole.WriteLine("You need to finish you current quest first!", font: FontTheme.Danger);
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
      GameConsole.Clear();
      GameConsole.WriteLine($"Player pos: {PositionX - 2} {PositionY}"); //subtracting 2 from x so it's easier for the player to read
      mapEntities.DisplayCurrentQuest();

      for (int i = 0; i <= rows; i++) //int i are for x coordinates  
      {
        for (int j = 0; j <= columns; j++) //int j are for y coordinates 
        {
          DecideWhatCharacterToWrite(i, j);
        }
        GameConsole.WriteLine();
      }

      Messages.PrintMapObjectsHelp();
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
    /// Function checking if on current coordinate exists an object
    /// </summary>
    /// <param name="x">x coordinate to check</param>
    /// <param name="y">y coordinate to chck</param>
    /// <param name="occupyingObject">function writes passed object if coordinate is occupied</param>
    /// <returns>return bool wheter map object exists on passed coordinate or not</returns>
    public bool IsCoordinateOccupied(int x, int y, out MapObject? occupyingObject)
    {
      if (mapEntities.mapObjects.TryGetValue((x, y), out occupyingObject))
      {
        return true;
      }

      occupyingObject = null;
      return false;
    }
  }
}