///<summary>
///
/// My refactorization of world of zuul
/// now everything is in different file
/// it makes everything easier to read and implement new stuff
/// 
/// in order to run the game you just have to run
/// npm start - write down in terminal in order to run the game
/// 
///<summary>

namespace WorldOfZuul
{
  public class Game
  {
    private protected Room? currentRoom;
    private protected Room? previousRoom;
    private protected Command? command;
    private protected bool continuePlaying = true;
    private protected static MessagePrinter messagePrinter = new();
    private protected Actions actions = new(ref messagePrinter);
    private readonly Parser parser = new();
    private readonly User user = new();

    public Game()
    {
      currentRoom = actions.CreateRooms();
    }

    public void Play()
    {
      messagePrinter.PrintWelcome();
      user.AskForUserName();
      user.AskForPlayerClass();

      GameConsole.WriteLine($"Hello {user.GetUsername()}");

      while (continuePlaying)
      {
        GameConsole.WriteLine(currentRoom?.ShortDescription);
        AskForCommand();

        continuePlaying = actions.DecideAction(
         ref command,
        ref currentRoom,
        ref previousRoom
        );
      }
      messagePrinter.PrintGoodbyeMessage();
      Thread.Sleep(300);
      messagePrinter.PrintShutdownMessage();

    }

    public void AskForCommand()
    {
      messagePrinter.PrintAskForCommandMessage();

      while ((command = parser.GetCommand(GameConsole.Input())) == null)
      {
        messagePrinter.PrintUnknownCommandMessage();
      }
    }
  }
}
