namespace WorldOfZuul
{
  class AsiaRoom : Room
  {
    bool continuePlaying = true;

    public static string? Inicials { get; set; }

    static NPC parkRanger = new("Park Ranger");

    public static bool AsiaMission = true;

    public void InProccess(bool isInProcess)
    {
      AsiaMission = continuePlaying;
    }

    public AsiaRoom(string shortDesc, string longDesc) : base(shortDesc, longDesc)
    {
      InitializeObjects();
    }

    public void CurrentlyInAsiaRoom(ref Room? currentRoom, ref Room? previousRoom)
    {
      LoadingAnimation.Loading("Mission Loading");
      GameConsole.Clear();

      InProccess(true);

      PrintIntroductionToTheRoom();
      previousRoom = null;

      while (continuePlaying)
      {
        Command? command = Game.AskForCommand();
        continuePlaying = Actions.DecideAction(ref command, ref currentRoom, ref previousRoom, true, "asia");
      }

    }
    public static void PrintIntroductionToTheRoom()
    {
      parkRanger.Speak("Poaching across Asia is reaching critical levels, driven by an unrelenting demand for illegal wildlife products.\nI am be here to guide you through the brief introduction into the quest, the rest falls upon your individual choices. \nHope you are up for the task, the poachers around here are relentless!");
    }

    private void InitializeObjects()
    {
      if (AsiaRoom.AsiaMission)
      {
        Console.WriteLine("Initializing objects...");

        Quest interceptPoachers = new Quest("Intercept Poachers", "Stop the poachers from brutally murdering your mama");
        Quest enterBuilding = new Quest("Enter the Building", "Enter the council building");

        interceptPoachers.AddQuest();
        enterBuilding.AddQuest();

        interceptPoachers.AddPrerequisite(enterBuilding);

        // Create MapObjects
        MapObject council = new(5, 4, MapObjectsEnum.PLACE, false, false, "You have entered the building", enterBuilding);
        // Add MapObjects to the map
        roomMap.AddMapObject(council); // First coordinate always uneven!

        MapObject poachers = new(11, 6, MapObjectsEnum.ENEMY, true, false, "You intercepted poachers", interceptPoachers);
        roomMap.AddMapObject(poachers);

        MapObject wall1 = new(3, 2, MapObjectsEnum.VERTICALWALL, false, true);
        roomMap.AddMapObject(wall1);
        MapObject wall2 = new(3, 3, MapObjectsEnum.VERTICALWALL, false, true);
        roomMap.AddMapObject(wall2);
        MapObject wall3 = new(1, 3, MapObjectsEnum.HORIZONTALWALL, false, true);
        roomMap.AddMapObject(wall3);


      }
    }
  }
}