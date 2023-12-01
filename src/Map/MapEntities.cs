using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldOfZuul.src.Map
{
  public class MapEntities
  {

    public Dictionary<(int X, int Y), MapObject> mapObjects = new Dictionary<(int, int), MapObject>();

    // it's jsut a stack of keys to the mapObjects dict so it takes less memory
    public Queue<(int X, int Y)> MapObjectsQuestsKeysQueue = new();

    /// <summary>
    /// This function takes an array of mapObjects and adds them onto the map
    /// </summary>
    /// <param name="objects">
    /// array of MapObjects you want to add onto the map
    /// </param>
    /// 
    public void PopulateMap(MapObject[] objects)
    {
      foreach (var mapObject in objects)
      {
        AddMapObject(mapObject);
      }
    }

    /// <summary>
    /// Function for adding one map object onto the map
    /// </summary>
    /// <param name="mapObject">Pass MapObject with set x and y coordinates</param>
    public void AddMapObject(MapObject mapObject)
    {
      mapObjects[(mapObject.MapCordX, mapObject.MapCordY)] = mapObject;

      if (mapObject.Quest != null)
      {
        MapObjectsQuestsKeysQueue.Enqueue((mapObject.MapCordX, mapObject.MapCordY));
      }
    }

    /// <summary>
    /// Function for removing object at coordinates x and y
    /// </summary>
    /// <param name="x"> x coordinate of a map object</param>
    /// <param name="y">y coordinate of a map object</param>
    private void RemoveMapObject(int x, int y)
    {
      if (mapObjects.ContainsKey((x, y)))
      {
        mapObjects.Remove((x, y));
      }
    }
    public void CheckQuestCompletion()
    {
      foreach (var mapObjectKey in MapObjectsQuestsKeysQueue)
      {
        var quest = mapObjects[mapObjectKey].Quest;
        if (quest != null && quest.IsCompleted)
        {
          GameConsole.WriteLine("Quest Completed: " + quest.Title);
        }
      }
    }

    public Quest? GetCurrentQuest()
    {
      if (MapObjectsQuestsKeysQueue.Count == 0) return null;

      var mapObjectKey = MapObjectsQuestsKeysQueue.First();
      return mapObjects[mapObjectKey].Quest;
    }

    public void CompleteCurrentQuest()
    {
      if (MapObjectsQuestsKeysQueue.Count > 0)
      {
        var mapObjectKey = MapObjectsQuestsKeysQueue.Dequeue();
        Quest? currentQuest = mapObjects[mapObjectKey].Quest;

        GameConsole.WriteLine($"{currentQuest?.Description}\n", font: FontTheme.HighligtedText);

        currentQuest?.MarkCompleted();
        GameConsole.WriteLine("Completed Task: " + currentQuest?.Title + "\n", font: FontTheme.Success);

        if (mapObjects[mapObjectKey].RemoveAfterCompletition())
        {
          RemoveMapObject(mapObjectKey.X, mapObjectKey.Y);
        }
      }
    }
    public void DisplayCurrentQuest()
    {
      if (GetCurrentQuest() != null)
      {

        GameConsole.WriteLine($"Current Quest: {GetCurrentQuest()?.Title}", font: FontTheme.Info);
      }
      else
      {
        GameConsole.WriteLine("All quests completed!", font: FontTheme.Success);
      }
    }
  }
}