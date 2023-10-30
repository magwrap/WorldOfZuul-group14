namespace WorldOfZuul
{
  public class GameConsole
  {
    public static void WriteLine(
      string? text = ConsoleConstants.Text,
      int delay = ConsoleConstants.Delay,
      bool breakline = ConsoleConstants.Breakline,
      ConsoleColor bgColor = ConsoleConstants.BackgroundColor,
      ConsoleColor fgColor = ConsoleConstants.ForegroundColor,
      bool resetColor = ConsoleConstants.ResetColor
    )
    {
      if (string.IsNullOrEmpty(text))
      {
        Console.WriteLine("");
        return;
      }

      Console.BackgroundColor = bgColor;
      Console.ForegroundColor = fgColor;
      //test TODO: remove this comment

      foreach (char c in text)
      {
        Console.Write(c);
        Thread.Sleep(delay);
      }

      if (resetColor) Console.ResetColor();
      if (breakline) Console.WriteLine("");
    }

    public static string Input(
      string? text = ConsoleConstants.Text,
      int delay = ConsoleConstants.Delay,
      bool breakline = ConsoleConstants.Breakline,
      ConsoleColor bgColor = ConsoleConstants.BackgroundColor,
      ConsoleColor fgColor = ConsoleConstants.ForegroundColor,
      bool resetColor = ConsoleConstants.ResetColor
    )
    {
      string? input;

      do
      {
        WriteLine(text, delay, breakline, bgColor, fgColor, resetColor);
        Console.Write(">>");
      }
      while (string.IsNullOrEmpty(input = Console.ReadLine()));

      input = input.ToString().Normalize().ToLower();
      return input;
    }
  }
}