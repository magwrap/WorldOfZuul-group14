using System.Text;
using WorldOfZuul;
namespace WorldOfZuul
{
  public class Map
  {
    private int position_x = 1;
    private int position_y = 1;
    private bool mapVisible = false;
    private readonly int heightOfMap;
    private readonly int widthOfMap;

    public Map(int height = 10, int width = 32)
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

      bool isOccupied = IsCoordinateOccupied(newPositionX, newPositionY, out MapObject? occupyingObject);

      if (BoundsOfTheMap(newPositionX, newPositionY))
      { 
        position_x = newPositionX;
        position_y = newPositionY;
        if (MapVisibility) // Check if the map is visible before showing it
        {
          ShowMap();
        }
        if (isOccupied)
        {
          occupyingObject?.DisplayOccupiedMessage(); // Display the occupied message if any
          Console.WriteLine("DisplayOccupiedMessage called");
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

    public void ShowMap()
    {
      if (AsiaRoom.OngoingMission())
      { //from 2 to 29 for the first coordinate 
        //from 2 to 9 for the second coordinate
        // Create MapObjects
        MapObject council = new(5, 4, "^", "You have entered the building"); //When creating an map object, j has to be always uneven because horizontaly the player always moves two places
        // Add MapObjects to the map
        AddMapObject(5, 4, council); //First coordinate always uneven!

        MapObject poachers = new(11, 6, "X", "You intercepted poachers");
        AddMapObject(11, 6, poachers);
      }
      //from 1 to 29 //movement of the player W/E
      //from 1 to 9 // movement of the plyer N/S
      int rows = heightOfMap + 1; //size of the map rows N/S, added +1 to avoid the bug of going out of the map :)
      int columns = widthOfMap; //size of the map columns W/E
      Console.WriteLine($"x: {rows}, y: {columns}");
      for (int i = 0; i <= rows; i++) //int i are for x coordinates  
      {
        for (int j = 0; j <= columns; j++) //int j are for y coordinates 
        {
          if (i == 0 || i == rows)
          {
            Console.Write("-");
          }
          else if (j == 0)
          {
            Console.Write("[");
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
            Console.Write(mapLabel);
          }

          else if (IsCoordinateOccupied(j, i, out MapObject? occupyingObject))
          {
            occupyingObject?.DisplayMapObject();//check for the objects and display them 
          }

          else if (position_x == j && position_y == i)
          {
            Console.Write(Game.Initials);
            //inicials taken from the playes name at the beggining of the game, 
            //players initials are shown on the map
          }
          else
          {
            Console.Write(" ");
          }
        }
        Console.WriteLine();
      }

    }

    private readonly Dictionary<(int, int), MapObject> mapObjects = new Dictionary<(int, int), MapObject>();

    public void AddMapObject(int x, int y, MapObject mapObject)
    {
      mapObjects[(x, y)] = mapObject;
    }

    public void RemoveMapObject(int x, int y)
    {
      if (mapObjects.ContainsKey((x, y)))
      {
        mapObjects.Remove((x, y));
      }
    }

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