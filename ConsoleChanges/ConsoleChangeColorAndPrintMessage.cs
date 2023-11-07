namespace WorldOfZuul
{
    class ConsoleChangeColorAndPrintMessage
    {
        public static void Print(FontTheme fontTheme, string message)
        {
            ConsoleColorOutput.ChangeFontColor(fontTheme);
            System.Console.Write(message);
            ConsoleColorOutput.ChangeFontColor(FontTheme.Default);
            System.Console.WriteLine();
        }
        public static void PrintForeground(FontTheme fontTheme, string message)
        {
            ConsoleColorOutput.ChangeFontForegroundColor(fontTheme);
            System.Console.Write(message);
            ConsoleColorOutput.ChangeFontColor(FontTheme.Default);
            System.Console.WriteLine();
        }
    }

}