namespace WorldOfZuul
{
  //TODO: FOR THE FUTURE REFACTOR
  public class Messages
  {
    /// <summary>
    /// 
    /// Class made for printing messages
    /// 
    /// 
    /// </summary>
    private protected Command? command;


    public static void PrintWelcome()
    {
      GameConsole.WriteLine(
        "Welcome to the World of Zuul!\nWorld of Zuul is a new, incredibly boring adventure game.",
        fgColor: ConsoleColor.Green
      );
      PrintHelp();
    }
    public static void PrintHelp()
    {
      GameConsole.WriteLine(
        "\nNavigate by typing ['name of the room'].\nType 'look' for more details.\nType 'back' to go to the previous room.\nType 'help' to print this message again.\nType 'quit' to exit the game.",
         font: FontTheme.Info
        );
    }

    public static void PrintUnknownCommandMessage()
    {
      GameConsole.WriteLine(
       "Invalid command. Type 'help' for a list of valid commands",
       font: FontTheme.Danger
       );
    }

    public static void PrintWrongDirectionMessage()
    {

    }

    public static void PrintAskForNameMessage()
    {
      GameConsole.WriteLine(
        "Please enter your name"
      );
    }

    public static void PrintAskForCommandMessage()
    {
      GameConsole.WriteLine(
        "Please enter a command",
        fgColor: ConsoleColor.DarkBlue
        );
    }
    public static void PrintGoodbyeMessage()
    {
      GameConsole.WriteLine("Thank you for playing World of Zuul!", fgColor: ConsoleColor.Green);
    }

    public static void PrintShutdownMessage()
    {
      GameConsole.WriteLine("", delay: 75);
    }
  }
}