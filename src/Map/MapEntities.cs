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
    public void RemoveMapObject(int x, int y)
    {
      if (mapObjects.ContainsKey((x, y)))
      {
        mapObjects.Remove((x, y));
      }
    }
    public bool IsAnyQuestAvailable()
    {
      return MapObjectsQuestsKeysQueue.Count > 0;
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

    public bool? StartCurrentQuest()
    {
      // GameConsole.WriteLine("Starting mission...");
      LoadingAnimation.Loading("Starting mission");
      GameConsole.Write("\n");


      var mapObjectKey = MapObjectsQuestsKeysQueue.First();
      MapObject? mapObject = mapObjects[mapObjectKey];

      GameConsole.WriteLine($"{mapObject.Quest?.Description}\n", font: FontTheme.HighligtedText);

      if (mapObject.MapObjectType is MapObjectsEnum.NPC || mapObject.MapObjectType is MapObjectsEnum.ENEMY || mapObject.MapObjectType is MapObjectsEnum.PLACE || mapObject.MapObjectType is MapObjectsEnum.PRISON)
      {
        return mapObject?.Npc?.TreeOfChoices?.StartDialog();
      }
      return true;
    }
    public void CompleteCurrentQuest(int reputationGain = 10)
    {
      if (MapObjectsQuestsKeysQueue.Count > 0)
      {
        var mapObjectKey = MapObjectsQuestsKeysQueue.Dequeue();
        Quest? currentQuest = mapObjects[mapObjectKey].Quest;


        currentQuest?.MarkCompleted();
        GameConsole.WriteLine("\nCompleted Task: " + currentQuest?.Title + "\n", font: FontTheme.Success);
        Reputation.MissionCompleted();

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

        GameConsole.WriteLine("\n---------------------------------------------------------", delay: 0);
        GameConsole.WriteLine($"Current Quest: {GetCurrentQuest()?.Title}", font: FontTheme.Info);
        GameConsole.WriteLine($"{GetCurrentQuest()?.Description}");
        GameConsole.WriteLine("---------------------------------------------------------\n", delay: 0);
      }
      else
      {
        GameConsole.WriteLine("All quests completed!", font: FontTheme.Success);
      }
    }
  }
}