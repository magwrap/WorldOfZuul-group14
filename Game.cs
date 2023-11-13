using WorldOfZuul.Africa;

namespace WorldOfZuul
{
  public class Game
  {
    private Room? currentRoom;
    private Room? previousRoom;
    private AsiaRoom? Asia;
    private Room? Hub;
    private AfricaRoom? Africa;
    private Command? command;

    private bool continuePlaying = true;
    public static string? Initials { get; set; }

    public Game()
    {
      CreateRooms();
    }

    private void CreateRooms()
    {
      Asia = new("Somewhere in Asia", "Asia");
      Hub = new("Hub", "You stand in the Ranger Headquarter’s Council Room.\nAs you look around you feel amazed yet again at how this building constructed underwater has such an amazing view into the ocean and its diverse wildlife from the council. \nAdditionally, you see three screens, each with its mission objective and details. On the left screen you see the details for the Pacific (pacific) deployment. On the right screen you see the Asian (asia) mission. On the middle screen you see the information for the Africa (africa) deployment.");
      Africa = new(ChangeRoom, "Africa Mission", "", "As you approach the middle screen, the mission description says ”SAFE ENDANGERED KORDOFAN GIRAFFE FROM A WILDFIRE” You scream the keyword ”Africa!”. The floor below you opens, and you fall into the pipe. As you go down, it takes a sharp twist left, and you suddenly find yourself on the comfortable leather chair of a small submarine");


      Asia.SetExit("hub", Hub); //among the hub and other rooms we could use just typos like hub, asia, pacific...
      Africa.SetExit("hub", Hub);
      Hub.SetExit("asia", Asia); //set exit for asia -> to hub
      Hub.SetExit("africa", Africa);

      currentRoom = Hub;
    }

    public void Play()
    {
      Messages.PrintWelcome();


      Messages.PrintAskForNameMessage();
      string inputName = GameConsole.Input();
      GetInicialOfThePlayer(inputName);

      GameConsole.WriteLine(
        $"\nHello {inputName}, just a little reminder, for better game experience, do not forget to make your terminal fullscreen. Enjoy!\n",
         font: FontTheme.Info
      );

      while (continuePlaying)
      {
        GameConsole.WriteLine("\n" + currentRoom?.ShortDescription, fgColor: ConsoleColor.DarkGreen);


        //Decide action
        if (currentRoom == null) throw new Exception("Error, no current room. You are nowhere");

        switch (currentRoom)
        {
          case var currentRoom when currentRoom.Equals(Asia):

            // LoadingAnimation.Loading("Loading");
            Asia.CurrentlyInAsiaRoom();
            GameConsole.WriteLine("Welcome back to the hub", font: FontTheme.Success);
            Actions.Move("hub", ref currentRoom, ref previousRoom);
            break;

          case var currentRoom when currentRoom.Equals(Africa):

            Africa.StartAfricaMission();

            GameConsole.WriteLine("Welcome back to the hub", font: FontTheme.Success);
            currentRoom = Hub;
            break;
          default:
            break;
        }

        command = AskForCommand();
        Console.WriteLine(command?.Name);

        continuePlaying = Actions.DecideAction(ref command, ref currentRoom, ref previousRoom);
      }

      Messages.PrintGoodbyeMessage();
    }



    public static Command AskForCommand()
    {
      Messages.PrintAskForCommandMessage();
      Command? userCommand;

      while ((userCommand = Parser.GetCommand(GameConsole.Input("", breakline: false))) == null)
      {
        GameConsole.WriteLine(

          font: FontTheme.Danger
        );
      }

      return userCommand;
    }

    public void ChangeRoom(Room room)
    {
      previousRoom = currentRoom;
      currentRoom = room;

    }

    public static void GetInicialOfThePlayer(string initials)
    {
      Initials = initials.ToUpper().Substring(0, 1);
    }
  }
}
