namespace WorldOfZuul
{
  //TODO: FOR THE FUTURE REFACTOR
  public class Messages
  {
    /// <summary>
    /// 
    /// Class made for printing messages
    /// 
    /// Example Usage: Message.PrintWelcome()
    /// and that's WriteLine command is within the function
    /// </summary>
    private protected Command? command;

    //TODO: fix all misspelled words

    public static void PrintWelcome()
    {

      //https://texteditor.com

      GameConsole.WriteLine(@" 
 (     ( .          (        .     )        :  (    (     
 )\    )\  (  (  (  \)         (   ()      (   )\   )\ (  
((_)  ((() )\ )\ )\ )\)_       )\ (())     )\ ((_) ((_))\ 
\ \    / /((_)( )| |)\| |     ((_)/ _|    ((_)(_))((_))| |
 \ \/\/ // _ \ '_| | _` |    / _ \  _|    |_ / || | || | |
  \_/\_/ \___/_| |_|__/_|    \___/_|      /__|\_._|\_._|_|

", delay: 1);
      GameConsole.WriteLine(
        "Welcome to the World of Zuul!\nWorld of Zuul is a new, incredibly boring adventure game.",
        fgColor: ConsoleColor.Green
      );

    }
    public static void PrintHelp()
    {
      GameConsole.WriteLine(
        "\nNavigate by typing 'chose mission'.\nType 'look' for more details.\nType 'back' to go to the previous room.\nType 'help' to print this message again.\nType 'clear' to clear out the console\nType 'quit' to exit the game.",
         font: FontTheme.Info
        );
    }
    public static void PrintMissionHelp()
    {
      GameConsole.WriteLine(
        "\nNavigate by typing ['north', 'east', 'west', 'south'] to move around the map.\nType 'look' for more details.\nType 'back' to go to the previous room.\nType 'map on' to turn on the map\nType 'map off' to turn off the map\nType 'help' to print this message again.\nType 'clear' to clear out the console\nType 'quit' to exit the game.\n\n",
         font: FontTheme.Info
        );
    }
    public static void PrintMapObjectsHelp()
    {
      GameConsole.WriteLine("\n\n\tMAP OBJECTS:", font: FontTheme.Info);
      GameConsole.Write($"{MapObject.MapMarkers[MapObjectsEnum.NPC]}", font: MapObject.MapObjectFonts[MapObjectsEnum.NPC]);
      GameConsole.Write("- Npc\n", font: FontTheme.GameTip);
      GameConsole.Write($"{MapObject.MapMarkers[MapObjectsEnum.ENEMY]}", font: MapObject.MapObjectFonts[MapObjectsEnum.ENEMY]);
      GameConsole.Write(" - Enemy\n", font: FontTheme.GameTip);
      GameConsole.Write($"{MapObject.MapMarkers[MapObjectsEnum.PLACE]}", font: MapObject.MapObjectFonts[MapObjectsEnum.PLACE]);
      GameConsole.Write(" - Place\n", font: FontTheme.GameTip);
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
      GameConsole.WriteLine("This room doesn't exist!", font: FontTheme.Danger);
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

    public static void PrintShowcaseOfMissions()
    {
      string asciiArt =
                  "\n    ,-----------------------------------,\n" +
                  "    |  /-----------------------------\\  |\n" +
                  "    | |                               | |\n" +
                  "    | |                               | |\n" +
                  "    | |    ,--',   _._.--._____       | |\n" +
                  "    | | .--.--';_'-.',  \";_     _.,-' | |\n" +
                  "    | |.'--'.  _.'    {`'-;_ x.-.>.'  | |\n" +
                  "    | |      '-:_      )x / `' '=.    | |\n" +
                  "    | |   x    ) >     {_/,     /~)   | |\n" +
                  "    | |        |/               `^ .' | |\n" +
                  "    |  \\_____________________________/  |\n" +
                  "    |___________________________________|\n" +
                  "    ,----\\_____     []     _______/-----,\n" +
                  "  /         /______________\\           /|\n" +
                  " /___________________________________ / | ___\n" +
                  "|                                   |   |    )\n" +
                  "|  _ _ _                 [-------]  |   |     (\n" +
                  "|  o o o                 [-------]  |  /       _)_\n" +
                  "|__________________________________ |/        /  /\n" +
                  "  /-------------------------------------/|    ( )/\n" +
                  " /-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/ /\n" +
                  "/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/ /\n" +
                  "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n";

      for (int i = 0; i < asciiArt.Length; i++)
      {
        if (asciiArt[i] == 'x')
        {
          GameConsole.Write("x", font: FontTheme.Danger);
        }
        else
        {
          GameConsole.Write(asciiArt[i]);
        }
      }
    }
  }
}