using System.IO.Enumeration;
using System.Text.Json;

namespace WorldOfZuul
{
  public class JsonFileReader
  {
    /// <summary>
    /// This class reads contents of *.json files
    /// returns a readable text objects
    /// </summary>
    private const string BASE_PATH = "src\\assets\\";


    public static GameMessages GetMessages()
    {
      GameMessages messages = new();
      TryToGetJSON<GameMessages>(BASE_PATH + "messages.json", ref messages);
      return messages;
    }
    public static GameDialogs GetDialogs()
    {
      GameDialogs dialogs = new();
      TryToGetJSON<GameDialogs>(BASE_PATH + "dialogs.json", ref dialogs);
      return dialogs;
    }
    public static GameNpcs GetNpcs()
    {
      GameNpcs npcs = new();
      TryToGetJSON<GameNpcs>(BASE_PATH + "npcs.json", ref npcs);
      return npcs;
    }
    public static GameRooms GetMainRooms()
    {
      GameRooms rooms = new();
      TryToGetJSON<GameRooms>(BASE_PATH + "rooms.json", ref rooms);
      return rooms;
    }

    private static void TryToGetJSON<T>(string filePath, ref T entity)
    {
      try
      {
        string jsonString = File.ReadAllText(filePath);
        entity = JsonSerializer.Deserialize<T>(jsonString)!;
      }
      catch (Exception err)
      {
        GameConsole.WriteLine($"There was a problem getting a file: {filePath}\n{err.Message}", fgColor: ConsoleColor.Red);
      }
    }
  }
}
