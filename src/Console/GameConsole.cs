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
      bool resetColor = ConsoleConstants.ResetColor,
      int paddingLeft = ConsoleConstants.paddingLeft
    )
    {
      if (string.IsNullOrEmpty(text))
      {
        Console.WriteLine("");
        return;
      }

      Console.BackgroundColor = bgColor;
      Console.ForegroundColor = fgColor;
      Console.Write("".PadLeft(paddingLeft));

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
        WriteLine(">>", breakline: false);
      }
      while (string.IsNullOrEmpty(input = Console.ReadLine()));

      input = input.ToString().Normalize().ToUpper();
      return input;
    }
  }
}