using System.Text.Json;

namespace WorldOfZuul
{
  public class JsonFileReader
  {
    /// <summary>
    /// This class reads contents of *.json files
    /// returns a readable text objects
    /// </summary>
    protected private GameMessages messages;
    protected private GameDialogs dialogs;
    public JsonFileReader()
    {
      string fileName, jsonString;

      fileName = "src\\assets\\messages.json";
      jsonString = File.ReadAllText(fileName);
      messages = JsonSerializer.Deserialize<GameMessages>(jsonString)!;

      fileName = "src\\assets\\dialogs.json";
      jsonString = File.ReadAllText(fileName);
      dialogs = JsonSerializer.Deserialize<GameDialogs>(jsonString)!;
    }

    public GameMessages GetMessages()
    {
      return messages;
    }
    public GameDialogs GetDialogs()
    {
      return dialogs;
    }
  }
}