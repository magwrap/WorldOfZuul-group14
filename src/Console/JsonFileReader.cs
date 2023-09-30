using System.Text.Json;

namespace WorldOfZuul
{
  public class JsonFileReader
  {
    /// <summary>
    /// This class reads contents of messages.json file
    /// returns a readable messages objects
    /// </summary>
    protected private GameMessages messages;
    public JsonFileReader()
    {
      string fileName = "src\\assets\\messages.json";
      string jsonString = File.ReadAllText(fileName);
      messages = JsonSerializer.Deserialize<GameMessages>(jsonString)!;
    }

    public GameMessages GetMessages()
    {
      return messages;
    }
  }
}