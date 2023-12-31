namespace WorldOfZuul
{
  public class NPC
  {
    public string? Name { get; protected set; }

    public ChoiceBranch? TreeOfChoices = null;
    public NPC(string? name)
    {
      this.Name = name;
    }

    public void Speak(string dialogue)
    {
      // Add custom behavior for AsiaNPC's speech
      GameConsole.Write(Name + ": ", font: FontTheme.NPC);
      GameConsole.WriteLine(dialogue + "\n", breakline: true, delay: 10);
    }
  }

}