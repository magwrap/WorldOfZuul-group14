namespace WorldOfZuul
{
    public enum FontTheme
    {
        Default,
        Danger,
        Success,
        NewItem,
        HighligtedText,
        NPC

    }
    public static class ConsoleColorOutput
    {
        public static void ChangeFontColor(FontTheme fontTheme)
        {
            if (fontTheme == FontTheme.Danger)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (fontTheme == FontTheme.Success)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (fontTheme == FontTheme.NewItem)
            {
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ResetColor();
            }
        }
        public static void ChangeFontForegroundColor(FontTheme fontTheme)
        {
            if (fontTheme == FontTheme.Danger)
            {
                Console.ForegroundColor = ConsoleColor.Red;  
            }
            else if (fontTheme == FontTheme.Success)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (fontTheme == FontTheme.NewItem)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
            }
            else
            {
                Console.ResetColor();
            }
        }
        
    }
}