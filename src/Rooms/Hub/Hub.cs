namespace WorldOfZuul
{
  class Hub
  { 
    public static bool isAsiaCompleted = true;
    public static bool isAfricaCompleted = true;
    public static bool isPacificCompleted = true;

    private static bool[] missionStatus = {isAsiaCompleted, isAfricaCompleted, isPacificCompleted};
    public static int SelectMission()
    {
      string[] options = { "Asia", "Africa", "Pacific" };
      int selectedOption = 0;

      string initialText = "Select a mission:";
      bool loop = true;
      GameConsole.Clear();

      while (loop)
      {
        GameConsole.Clear();
        PrintMap(selectedOption + 1);
        Console.WriteLine(initialText);

        for (int i = 0; i < options.Length; i++)
        {
            if (i == selectedOption)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            string statusSymbol = missionStatus[i] ? "✔" : "🗙";
            Console.Write($"{(i == selectedOption ? "\U000027A4 " : " ")} {options[i]} {statusSymbol}");

            // Reset background color after printing each line
            Console.ResetColor();
            Console.WriteLine();
        }

        var key = GameConsole.ReadKey().Key;

        switch (key)
        {
          case ConsoleKey.UpArrow:
            selectedOption = (selectedOption - 1 + options.Length) % options.Length;
            break;

          case ConsoleKey.DownArrow:
            selectedOption = (selectedOption + 1) % options.Length;
            break;

          case ConsoleKey.Enter:
            GameConsole.Clear();
            PrintMap(selectedOption + 1);
            if(missionStatus[selectedOption] == false)
            {
              GameConsole.WriteLine($"You chose: {options[selectedOption]}");
              loop = false;
            }
            else 
            {
              GameConsole.WriteLine($"You already completed this mission", font: FontTheme.Danger);
              Thread.Sleep(3000);
            }
            
            break;
        }
      }
      return selectedOption;
    }

    public static void PrintMap(int option)
    {
      int count = 0;
      string asciiArt = "------+-----+-----+-----+-----+-----+-----+-----+-----+-----+-----+-----\n" +
                  "           . _..::__:  ,\"-\"-._        |        ,     _,.__\n" +
                  "   _.___ _ _<_>`|\\(._`.`-.    /         _._     `_ ,_/  '  '-._.---.-.__\n" +
                  ">.{     \" \" `-==,',._\\\\{  \\  /\\)       / _ \">_,-' `                |\\\\_\n" +
                  "  \\_.:--.       `._ \\)`^-\"'       , [_/\\(                       __,/-' \n" +
                  "\"'     \\         \"    _L        //_,--'                       /. \\(|\n" +
                  "          |           ,'          _\\)_.\\\\ ._<>                _,' /  '\n" + //Europe 1
                  "          `.         /           [_/_'` `\"\\(                <'\\}  \\)\n" +
                  "           \\\\    .-. \\)           /   `-'" + "\"..' `:.           _\\)  '\n" +
                  "    `        \\  \\(  `\\(           /         `:\\  > \\  ,-^.X /' '\n" +    //Asia 2
                  "              `._,   \"\"         |      X    \\`'   \\|   _\\)  \\{\\\n" +  //Africa 3
                  "                 `=.---.        `._._       ,'     \"`  |' ,- '.\n" +
                  "                   |    `-._         |     /          `:`<_| --._\n" +
                  "                   \\(        >        .     |            `=.__.`-'  \n" +
                  "                    `.     /         |     ||/              ,-.,\\     .\n" +
                  "         X           |   ,'           \\   / `'            ,\"     \\\n" +     //Pacific 4
                  "                     |  /              |_'                |  __  /\n" +
                  "                     | |                                  '-'  `-'   \\.\n" +
                  "                     |/                                         \"    /\n" +
                  "                     \\.                                             '\\\n" +
                  "\n" +
                  "                      ,/            ______._.--._ _..---.---------. \n" +
                  "     ,-----\"-..?----_/ \\)      __,-'\"             \"                  \\(\n" +
                  "-.._\\(                  `-----'                                       `- \n" +
                  "-----+-----+-----+-----+-----+-----+-----+-----+-----+-----+-----+-----\n";

      // Make the character 'X' red
      for (int i = 0; i < asciiArt.Length; i++)
      {
        if (asciiArt[i] == 'X')
        {
          count++;

          if (count == option)
          {
            GameConsole.Write("X", font: FontTheme.Danger);
          }
        }
        else
        {
          GameConsole.Write(asciiArt[i]);
        }
      }
    }
  }
}

