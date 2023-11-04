namespace WorldOfZuul
{
  public class MessagePrinter
  {
    /// <summary>
    /// 
    /// Class made for printing messages
    /// 
    /// <info>
    ///   /// ADDING MESSAGE PIPELINE:
    /// - create function PrintNameOfTheMessage
    /// - add to GameMessages class the message you want to add
    /// - add to messages.json contents of the message that you want to add
    ///     <warning>
    ///      !!!WARNING - the name of the message in messages.json has to be the same as in GameMessages!!!
    ///     <warining>
    /// <info>
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
      PrintHelp();
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
      GameConsole.WriteLine(messages.AskForCommandMessage, bgColor: ConsoleColor.DarkBlue);
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