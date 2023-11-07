namespace WorldOfZuul
{
  interface INPC
  {
    public string? Name { get; set; }
    public string? Dialogue { get; set; }

    void Speak()
    {
      // Display NPC's dialogue
      Console.WriteLine($"{Name}: {Dialogue}");
    }
  }
}