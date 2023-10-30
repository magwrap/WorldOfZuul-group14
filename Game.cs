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
    private protected Command? command;
    private protected bool continuePlaying = true;
    private protected static MessagePrinter messagePrinter = new();
    private protected Rooms rooms = new();
    private readonly User user = new();
    private readonly ActionDispatcher actionDispatcher;

    public Game()
    {
      rooms.CreateRooms();
      actionDispatcher = new(ref rooms);
    }

    public void Play()
    {
      messagePrinter.PrintWelcome();
      user.AskForUserName();
      user.AskForPlayerClass();

      GameConsole.WriteLine($"Hello {user.GetUsername()}");

      while (continuePlaying)
      {
        GameConsole.WriteLine(rooms.CurrentRoom?.ShortDescription);
        AskForCommand();


        continuePlaying = actionDispatcher.DecideAction(
         ref command
        );
      }

      messagePrinter.PrintGoodbyeMessage();
      Thread.Sleep(300);
      messagePrinter.PrintShutdownMessage();

    }

    public void AskForCommand()
    {
      messagePrinter.PrintAskForCommandMessage();

      while ((command = Parser.GetCommand(GameConsole.Input())) == null)
      {
        messagePrinter.PrintUnknownCommandMessage();
      }
    }
  }
}
