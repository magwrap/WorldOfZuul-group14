namespace WorldOfZuul
{
  public class Parser
  {
    /// <summary>
    /// Class parsing input into command
    /// </summary>
    public static Command? GetCommand(string inputLine)
    {
      string[] words = inputLine.Split();

      //checks if the command is valid in commandWords, if it isn't returns null

      if (words.Length == 0 || !Enum.IsDefined(typeof(ActionsEnum), words[0]))
      {
        return null;
      }

      if (words.Length > 1)
      {
        return new Command(words[0], words[1..]);
      }

      return new Command(words[0]);
    }
  }

}
