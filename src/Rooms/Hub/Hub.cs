namespace WorldOfZuul
{
  class Hub
  {
    public static int SelectMission()
    {
      string[] options = { "Europe", "Asia", "Africa", "Pacific" };
      int selectedOption = 0;

      string initialText = "Select a mission:";
      bool loop = true;

      while (loop)
      {
        GameConsole.Clear();
        PrintMap(selectedOption + 1);
        GameConsole.WriteLine(initialText);

        for (int i = 0; i < options.Length; i++)
        {
          bool isSelected = i == selectedOption;

          GameConsole.WriteLine(
            $"{(i == selectedOption ? ">" : " ")} {options[i]}",
            fgColor: isSelected ? ConsoleColor.White : ConsoleColor.Gray,
            bgColor: isSelected ? ConsoleColor.Blue : ConsoleColor.Black

          );

        }

        var key = GameConsole.ReadKey().Key;

        switch (key)
        {
          case ConsoleKey.UpArrow:
            selectedOption = Math.Max(0, selectedOption - 1);
            break;
          case ConsoleKey.DownArrow:
            selectedOption = Math.Min(options.Length - 1, selectedOption + 1);
            break;
          case ConsoleKey.Enter:
            GameConsole.Clear();
            PrintMap(selectedOption + 1);

            GameConsole.WriteLine($"You chose: {options[selectedOption]}");
            loop = false;
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
                  "          |           ,'          _\\)_.\\\\X._<>                _,' /  '\n" + //Europe 1
                  "          `.         /           [_/_'` `\"\\(                <'\\}  \\)\n" +
                  "           \\\\    .-. \\)           /   `-'" + "\"..' `:.           _\\)  '\n" +
                  "    `        \\  \\(  `\\(           /         `:\\  >X\\  ,-^.  /' '\n" +    //Asia 2
                  "              `._,   \"\"         |      X    \\`'   \\|   ?_\\)  \\{\\\n" +  //Africa 3
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
            GameConsole.Write('X', font: FontTheme.Danger);
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
