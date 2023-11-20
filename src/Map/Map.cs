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

    public Map(int height = 11, int width = 42)
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

      if (BoundsOfTheMap(newPositionX, newPositionY))
      {
        position_x = newPositionX;
        position_y = newPositionY;
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

      return true;
    }

    public void ShowMap()
    {

      //from 1 to 39 //movement of the player W/E
      //from 1 to 9 // movement of the plyer N/S
      int rows = heightOfMap; //size of the map rows N/S
      int columns = widthOfMap; //size of the map columns W/E
      Console.WriteLine($"x: {rows}, y: {columns}");
      for (int i = 0; i <= rows; i++)
      {
        for (int j = 0; j <= columns; j++)
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
  }
}