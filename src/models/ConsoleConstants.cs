namespace WorldOfZuul
{
  public struct ConsoleConstants
  {
    public const int Delay = 3; //default 10
    public const string Text = "";
    public const bool Breakline = true;
    public const ConsoleColor BackgroundColor = ConsoleColor.Black;
    public const ConsoleColor ForegroundColor = ConsoleColor.Gray;
    public const bool ResetColor = true;
    public const int paddingLeft = 0;
    public const FontTheme fontTheme = FontTheme.Default;
    public readonly static Dictionary<FontTheme, ConsoleColor> Colors = new()
    {
      [FontTheme.Default] = ConsoleColor.Gray,
      [FontTheme.Danger] = ConsoleColor.Red,
      [FontTheme.Success] = ConsoleColor.DarkGreen,
      [FontTheme.NewItem] = ConsoleColor.DarkYellow,
      [FontTheme.HighligtedText] = ConsoleColor.Blue,
      [FontTheme.Info] = ConsoleColor.DarkMagenta,

    };


    public ConsoleConstants()
    {
    }

  }
  public enum FontTheme
  {
    Default,
    Danger,
    Success,
    NewItem,
    HighligtedText,
    NPC,
    Info

  }
}

