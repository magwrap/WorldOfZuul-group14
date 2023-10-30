
namespace WorldOfZuul
{
  public class Command
  {
    public string Name { get; }
    public string[]? Arguments { get; } // this might be used for future expansions, such as "take apple".

    public Command(string name, string[]? secondWord = null)
    {
      Name = name;
      Arguments = secondWord;
    }
  }
}
