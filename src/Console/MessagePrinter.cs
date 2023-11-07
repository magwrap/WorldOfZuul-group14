namespace WorldOfZuul
{
  //TODO: FOR THE FUTURE REFACTOR
  public class MessagePrinter
  {
    /// <summary>
    /// 
    /// Class made for printing messages
    /// 
    /// 
    /// </summary>
    private protected GameMessages? messages;
    private protected Command? command;

    public MessagePrinter()
    {
      GetMessages();
    }
    public void GetMessages()
    {
      messages = JsonFileReader.GetMessages();
    }

    public void PrintWelcome()
    {
      if (messages == null) return;
      GameConsole.WriteLine(messages.WelcomeMessage, fgColor: ConsoleColor.Green);
    }
    public void PrintHelp()
    {
      if (messages == null) return;
      GameConsole.WriteLine(messages.HelpMessage, fgColor: ConsoleColor.Blue);
    }

    public void PrintUnknownCommandMessage()
    {
      if (messages == null) return;
      GameConsole.WriteLine(messages.UnknownCommandMessage, fgColor: ConsoleColor.Red);
    }

    public void PrintAskForCommandMessage()
    {
      if (messages == null) return;
      GameConsole.WriteLine(messages.AskForCommandMessage, fgColor: ConsoleColor.DarkBlue);
    }
    public void PrintGoodbyeMessage()
    {
      if (messages == null) return;
      GameConsole.WriteLine(messages.GoodbyeMessage, fgColor: ConsoleColor.Green);
    }

    public void PrintShutdownMessage()
    {
      if (messages == null) return;
      GameConsole.WriteLine(messages.ShutdownMessage, delay: 75);
    }
  }
}