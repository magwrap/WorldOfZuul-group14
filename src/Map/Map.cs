using System.Text;
using WorldOfZuul;
namespace WorldOfZuul
{
  public class Map
  {
    private static int position_x = 1;
    private static int position_y = 1;
    private static bool mapVisible = false;

    public static bool MapVisibility
    {
      get { return mapVisible; }
    }

    public static int PositionX
    {
      get { return position_x; }
    }

    public static int PositionY
    {
      get { return position_y; }
    }

    public static bool ChangeMapVisibility(bool changeVisibility)
    {
      return mapVisible = changeVisibility;
    }

    public static void MoveOnMap(string direction)
    {
      int newPositionX = position_x;
      int newPositionY = position_y;

      if (direction == "north")
      {
        newPositionY--;
      }
      else if (direction == "south")
      {
        newPositionY++;
      }
      else if (direction == "west")
      {
        newPositionX -= 2;
      }
      else if (direction == "east")
      {
        newPositionX += 2;
      }

      if (BoundsOfTheMap(newPositionX, newPositionY))
      {
        position_x = newPositionX;
        position_y = newPositionY;
        if (MapVisibility) // Check if the map is visible before showing it
        {
          ShowMap(PositionX, PositionY);
        }

      }
      else
      {
        GameConsole.WriteLine("Out of the bounds of the map! Try another direction.", font: FontTheme.Danger);
      }
    }

    public static bool BoundsOfTheMap(int x, int y)
    {

      if (x < 1 || x > 42 || y < 1 || y > 11)
      {
        return false;
      }

      return true;
    }

    public static void ShowMap(int x, int y)
    {

      x = position_x; //from 1 to 39 //movement of the player W/E
      y = position_y; //from 1 to 9 // movement of the plyer N/S
      int rows = 10; //size of the map rows N/S
      int columns = 40; //size of the map columns W/E
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
          else if (x == j && y == i)
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