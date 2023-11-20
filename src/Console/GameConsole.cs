using Microsoft.VisualBasic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Windows.Input;
namespace WorldOfZuul
{
  public class GameConsole
  {
    /// <summary>
    /// Console substitue function for displaying text, with animation and extra parans
    /// </summary>
    /// <param name="text">text you want to display</param>
    /// <param name="delay">delay between displaying letters</param>
    /// <param name="breakline">should add the breakline at the end, default: true</param>
    /// <param name="bgColor">what should be the background color</param>
    /// <param name="fgColor">what is the color of the letters</param>
    /// <param name="resetColor">should reset the color to the default one after dispalying the text, default: true</param>
    /// <param name="paddingLeft">how big is the padding on the left of the string, default: 0</param>
    /// <param name="font">font style(foreground color) of value of enum FontTheme</param>
    public static void WriteLine(
      string? text = ConsoleConstants.Text,
      int delay = ConsoleConstants.Delay,
      bool breakline = ConsoleConstants.WriteLineBreakline,
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



    /// <summary>
    /// Console substitue function for displaying text in one line, with extra parans
    /// </summary>
    /// <param name="text">string you want to display</param>
    /// <param name="breakline">should add the breakline at the end, default: true</param>
    /// <param name="bgColor">what should be the background color</param>
    /// <param name="fgColor">what is the color of the letters</param>
    /// <param name="resetColor">should reset the color to the default one after dispalying the text, default: true</param>
    /// <param name="paddingLeft">how big is the padding on the left of the string, default: 0</param>
    /// <param name="font">font style(foreground color) of value of enum FontTheme</param>
    public static void Write(
      string text,
      bool breakline = ConsoleConstants.WriteBreakline,
      ConsoleColor bgColor = ConsoleConstants.BackgroundColor,
      ConsoleColor fgColor = ConsoleConstants.ForegroundColor,
      bool resetColor = ConsoleConstants.ResetColor,
      int paddingLeft = ConsoleConstants.paddingLeft,
      FontTheme? font = null
    )
    {
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
      Console.Write(text);
      if (resetColor) Console.ResetColor();
      if (breakline) Console.WriteLine("");
    }

    /// <summary>
    /// Console substitue function for displaying one character, with extra parans
    /// </summary>
    /// <param name="character">character you want to display</param>
    /// <param name="breakline">should add the breakline at the end, default: true</param>
    /// <param name="bgColor">what should be the background color</param>
    /// <param name="fgColor">what is the color of the letters</param>
    /// <param name="resetColor">should reset the color to the default one after dispalying the text, default: true</param>
    /// <param name="paddingLeft">how big is the padding on the left of the string, default: 0</param>
    /// <param name="font">font style(foreground color) of value of enum FontTheme</param>
    public static void Write(
      char character,
      bool breakline = ConsoleConstants.WriteBreakline,
      ConsoleColor bgColor = ConsoleConstants.BackgroundColor,
      ConsoleColor fgColor = ConsoleConstants.ForegroundColor,
      bool resetColor = ConsoleConstants.ResetColor,
      int paddingLeft = ConsoleConstants.paddingLeft,
      FontTheme? font = null
    )
    {
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
      Console.Write(character);
      if (resetColor) Console.ResetColor();
      if (breakline) Console.WriteLine("");
    }

    /// <summary>
    /// Function for taking an user string input, can also display passed text before asking for input
    /// </summary>
    /// <param name="text">text you want to display</param>
    /// <param name="delay">delay between displaying letters</param>
    /// <param name="breakline">should add the breakline at the end, default: true</param>
    /// <param name="bgColor">what should be the background color</param>
    /// <param name="fgColor">what is the color of the letters</param>
    /// <param name="resetColor">should reset the color to the default one after dispalying the text, default: true</param>
    /// <param name="paddingLeft">how big is the padding on the left of the string, default: 0</param>
    /// <param name="font">font style(foreground color) of value of enum FontTheme</param>
    /// <returns>Returns the string input by user</returns>
    public static string Input(
      string? text = ConsoleConstants.Text,
      int delay = ConsoleConstants.Delay,
      bool breakline = ConsoleConstants.WriteLineBreakline,
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

    /// <summary>
    /// Clears one the console
    /// </summary>
    public static void Clear()
    {
      Console.ResetColor();
      Console.Clear();
    }
    /// <summary>
    /// Substitue for the Console function for reading user key
    /// </summary>
    /// <returns>Returns info about input key</returns>
    public static ConsoleKeyInfo ReadKey()
    {
      return Console.ReadKey();
    }

    //More cursor movement info:
    //https://learn.microsoft.com/en-us/windows/console/console-virtual-terminal-sequences

    public static void MoveCursorUp(int val = 1)
    {
      try
      {

        Console.CursorTop -= val;
      }
      catch
      {
        GameConsole.WriteLine("Can't move cursor up!", font: FontTheme.Danger);
      }
    }

    public static void MoveCursorDown(int val = 1)
    {
      try
      {

        Console.CursorTop += val;
      }
      catch
      {
        GameConsole.WriteLine("Can't move cursor down!", font: FontTheme.Danger);
      }
    }

    public static void MoveCursorLeft(int val)
    {
      try
      {

        Console.CursorLeft -= val;
      }
      catch
      {
        GameConsole.WriteLine("Can't move cursor left!", font: FontTheme.Danger);
      }
    }

    public static void MoveCursorRight(int val)
    {
      try
      {

        Console.CursorLeft += val;
      }
      catch
      {
        GameConsole.WriteLine("Can't move cursor right!", font: FontTheme.Danger);
      }
    }

  }
}