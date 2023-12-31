using WorldOfZuul;
using WorldOfZuul.src.Map;

namespace WorldOfZuul
{
  public class Map
  {
    public int position_x = 1;
    public int position_y = 1;
    private bool mapVisible = true;
    private readonly int heightOfMap;
    private readonly int widthOfMap;
    public readonly MapEntities mapEntities = new();

    public Map(
      int height = 10,
      int width = 30
    )
    {
      heightOfMap = height;
      widthOfMap = width;
    }

    public bool MapVisibility
    {
      get { return mapVisible; }
    }

    public void SetXandY(int x, int y)
    {
      position_x = x;
      position_y = y;
    }

    public bool ChangeMapVisibility(bool changeVisibility)
    {
      return mapVisible = changeVisibility;
    }

    public void MoveOnMap(string direction, string range = "1")
    {
      int newPositionX = position_x;
      int newPositionY = position_y;


      _ = int.TryParse(range, out int rangeToMove);

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
          rangeToMove = 0;

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
              bool? questResult = mapEntities.StartCurrentQuest();


              //if result is positive(true) then we can finish the quest
              if (questResult == null || questResult == true)
              {
                mapEntities.CompleteCurrentQuest();
              }
              else
              {
                GameConsole.WriteLine("Failed to finish the quest try again!");
              }
            }
            else
            {
              GameConsole.WriteLine("Prerequisites are not met!", font: FontTheme.Danger);
            }
          }
          else
          {
            if (occupyingObject?.MapObjectType is MapObjectsEnum.NPC)
            {

              if (mapEntities.IsAnyQuestAvailable())
              {

                GameConsole.WriteLine("You need to finish you current quest first!", font: FontTheme.Danger);
              }
              else
              {
                GameConsole.WriteLine("You've no current quests left!", font: FontTheme.HighligtedText);
              }
            }
            else
            {
              GameConsole.WriteLine("You can't go futher in this direction.", font: FontTheme.Danger);
            }
          }
        }
      }
      else
      {
        GameConsole.WriteLine("Out of the bounds of the map! Try another direction.", font: FontTheme.Danger);
        rangeToMove = 0;
      }

      rangeToMove--;
      if (rangeToMove > 0)
      {
        Thread.Sleep(500);
        MoveOnMap(direction, rangeToMove.ToString());
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
      mapEntities.DisplayCurrentQuest();

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