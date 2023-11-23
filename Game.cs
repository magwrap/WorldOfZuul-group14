using WorldOfZuul.Africa;

namespace WorldOfZuul
{
  public class Game
  {
    /// <summary>
    ///   Main class for running the game
    /// </summary>
    private Room? currentRoom;
    private Room? previousRoom;
    private AsiaRoom? Asia;
    private Room? Hub;
    private Room? Pacific;
    private AfricaRoom? Africa;
    private Command? command;

    private bool continuePlaying = true;
    public static string? Initials { get; set; }

    public Game()
    {
      CreateRooms();
    }

    /// <summary>
    /// function creating all the main rooms
    /// </summary>

    private void CreateRooms()
    {
      Hub = new("Ranger Headquarters", "You stand in the Ranger Headquarter's Council Room, surrounded by the breathtaking view of the ocean's depths through the expansive windows.\nDominating the central area is a large, illuminated map on marked with indications highlighting the Pacific, Asia, and Africa.\nThese areas stand out on the map, representing the regions most severely impacted by poaching activities across the globe.");

      Asia = new("Somewhere in Asia", "Asia");

      Africa = new("Africa Mission", "Africa", "As you approach the middle screen, the mission description says ”SAFE ENDANGERED KORDOFAN GIRAFFE FROM A WILDFIRE” You scream the keyword ”Africa!”. The floor below you opens, and you fall into the pipe. As you go down, it takes a sharp twist left, and you suddenly find yourself on the comfortable leather chair of a small submarine");

      Pacific = new("Ocean", "You are standing now in the pacific ocean. From now you have to decide either going to the warship or to the submarine.To the east you have to nuke the proachers, to the south you have to cut the proacher's lines, and to the west you have to flip their ship.");



      Asia.SetExit("hub", Hub); //among the hub and other rooms we could use just typos like hub, asia, pacific...
      Africa.SetExit("hub", Hub);
      Pacific.SetExit("hub", Hub);

      Hub.SetExit("asia", Asia); //set exit for asia -> to hub
      Hub.SetExit("africa", Africa);
      Hub.SetExit("pacific", Pacific);



      currentRoom = Hub;
    }

    /// <summary>
    /// main game loop - here it asks for user input decides it and moves to different rooms
    /// </summary>
    /// <exception cref="Exception">returns error if currentRoom is empty</exception>
    public void Play()
    {

      //Print Welcome
      Console.ForegroundColor = ConsoleColor.Black;
      Messages.PrintWelcome();

      //Get username and inicials
      Messages.PrintAskForNameMessage();
      string inputName = GameConsole.Input();
      GetInicialOfThePlayer(inputName);

      //Greet user
      GameConsole.WriteLine(
        $"\nHello {inputName}, just a little reminder, for better game experience, do not forget to make your terminal fullscreen. Enjoy!",
         font: FontTheme.Info
      );

      GameConsole.WriteLine("\n" + currentRoom?.LongDescription, fgColor: ConsoleColor.DarkYellow, breakline: false);

      Messages.PrintShowcaseOfMissions();

      Messages.PrintHelp();

      //Main game loop
      while (continuePlaying)
      {
        GameConsole.WriteLine("\n" + currentRoom?.ShortDescription, fgColor: ConsoleColor.DarkGreen);


        if (currentRoom == null) throw new Exception("Error, no current room. You are nowhere");

        //Decide if should start room mission
        switch (currentRoom)
        {
          case var room when currentRoom.Equals(Asia):

            Asia.CurrentlyInAsiaRoom(ref currentRoom, ref previousRoom);
            GameConsole.WriteLine("Welcome back to the hub", font: FontTheme.Success);
            break;

          case var room when currentRoom.Equals(Africa):
            Africa.StartAfricaMission(ref currentRoom, ref previousRoom);

            GameConsole.WriteLine("Welcome back to the hub", font: FontTheme.Success);
            break;

          default:
            break;
        }

        //back to hub
        previousRoom = null;
        currentRoom = Hub;

        //get a command
        command = AskForCommand();

        //Action dispatcher returns true or false depending if the player wants to quit the game
        continuePlaying = Actions.DecideAction(ref command, ref currentRoom, ref previousRoom);
      }

      Messages.PrintGoodbyeMessage();
    }

    /// <summary>
    /// Function asks for user command and keeps asking until the input is not null and game knows the command passed
    /// </summary>
    /// <returns>returns Command object with command name</returns>
    public static Command AskForCommand()
    {
      //print message asking for command
      Messages.PrintAskForCommandMessage();
      Command? userCommand;

      //keep asking for command until user inputs valid command
      while ((userCommand = Parser.GetCommand(GameConsole.Input("", breakline: false))) == null)
      {
        Messages.PrintUnknownCommandMessage();
      }

      return userCommand;
    }

    /// <summary>
    /// Function setting global Initials of the player by the name inputed at the beginning
    /// </summary>
    /// <param name="initials"></param>
    public static void GetInicialOfThePlayer(string name)
    {
      Initials = name.ToUpper()[..1];
    }
  }
}
