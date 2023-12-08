using System;
using System.Threading;
using WorldOfZuul;

class LoadingAnimation
{
  public static void Loading(string text)
  {
    Console.Write(text);

    for (int i = 0; i < 3; i++)
    {
      for (int j = 0; j < 3; j++)
      {
        Console.Write(".");
        Thread.Sleep(400); // Adjust the delay as needed (500 milliseconds = half a second)
      }

      Console.Write("\b\b\b   \b\b\b"); // Clear the three dots
      Thread.Sleep(400); // Wait before the next iterationc

    }
    // Console.Clear();
  }
  public static void CreateCountDown(int seconds)
  { 
    for (int i = seconds; i > 0; i--)
    {
      GameConsole.Write("Game starts in: ", font: FontTheme.Success);
      GameConsole.Write(i + "\r");
      Thread.Sleep(1000); // Pause for 1 second
    }
    GameConsole.Clear(); 
  }
}