using Microsoft.VisualBasic;
using System.Security.Cryptography;
using System.Windows.Input;
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
      int paddingLeft = ConsoleConstants.paddingLeft,
      FontTheme? font = null
    )
    {
      System.ConsoleKey[] consoleBreakKeys = { ConsoleKey.Enter, ConsoleKey.Spacebar };

      if (string.IsNullOrEmpty(text))
      {
        Console.WriteLine("");
        return;
      }
      //TODO: block input when displaying text
      if (font != null && Enum.IsDefined(typeof(FontTheme), font))
      {
        Console.ForegroundColor = ConsoleConstants.Colors[(FontTheme)font];

      }
      else
      {
        Console.ForegroundColor = fgColor;
      }
      Console.BackgroundColor = bgColor;

      Console.Write("".PadLeft(paddingLeft));

      int i;
      for (i = 0; i < text.Length; ++i)
      {
        Console.Write(text[i]);
        Thread.Sleep(delay);
        if (Console.KeyAvailable && consoleBreakKeys.Contains(Console.ReadKey(true).Key))
        {
          Console.WriteLine(text[i..]);
          break;
        }
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
      bool resetColor = ConsoleConstants.ResetColor,
      FontTheme? font = null
    )
    {
      string? input;

      do
      {
        if (!String.IsNullOrEmpty(text))
        {
          WriteLine(text, delay, breakline, bgColor, fgColor, resetColor, 0, font);
        }
        WriteLine(">> ", breakline: false);
      }
      while (string.IsNullOrEmpty(input = Console.ReadLine()));

      input = input.ToString().Normalize().ToLower().Trim();
      return input;
    }

    public static void Clear()
    {
      Console.Clear();
    }

    public static void ResetColor()
    {
      Console.ResetColor();
    }

    public static ConsoleKeyInfo ReadKey()
    {
        return Console.ReadKey();
    } 

  }
}