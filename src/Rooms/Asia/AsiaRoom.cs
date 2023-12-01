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
      // LoadingAnimation.Loading("Mission Loading");
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

        interceptPoachers.AddPrerequisite(enterBuilding);

        // Create MapObjects
        MapObject council = new(5, 4, MapObjectsEnum.PLACE, false, false, "You have entered the building", enterBuilding);
        // Add MapObjects to the map
        RoomMap.mapEntities.AddMapObject(council); // First coordinate always uneven!

        Enemy GregoryPoacher = new("Gregory");
        GregoryPoacher.TreeOfChoices = new ChoiceBranch(0, "I'm gregory the poacher and I will kill all the animalls!");


        MapObject poachers = new(11, 6, MapObjectsEnum.ENEMY, true, false, "You intercepted poachers", interceptPoachers, GregoryPoacher);
        RoomMap.mapEntities.AddMapObject(poachers);

        MapObject wall1 = new(3, 2, MapObjectsEnum.VERTICALWALL, false, true);
        RoomMap.mapEntities.AddMapObject(wall1);
        MapObject wall2 = new(3, 3, MapObjectsEnum.VERTICALWALL, false, true);
        RoomMap.mapEntities.AddMapObject(wall2);
        MapObject wall3 = new(1, 3, MapObjectsEnum.HORIZONTALWALL, false, true);
        RoomMap.mapEntities.AddMapObject(wall3);
      }
    }
  }
}