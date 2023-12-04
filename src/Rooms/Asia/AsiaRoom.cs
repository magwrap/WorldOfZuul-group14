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
        //intended asia mission, first all of the base walls are closed - you ahve to enter the operational center for anti-poaching crisis in asia, where you talk to an npc who briefs you 
        //then after you are finished talking with him, the walls open and you are supposed to capture the poachers
        //once you capture the poachers you have to bring them back to OCAPCA 
        //SHIELD Asia
        // Sustainable
        // Habitat
        // Intervention and
        // Ecological
        // Law
        // Defenders

        //add quests 
        Quest interceptPoachers = new Quest("Intercept Poachers", "Stop the poachers from brutally murdering your mama");
        Quest enterBuilding = new Quest("Enter the Building", "Enter the council building");
        interceptPoachers.AddPrerequisite(enterBuilding);

        // Create MapObjects
        MapObject council = new(5, 4, MapObjectsEnum.PLACE, false, false, "You have entered the building", enterBuilding);
        // Add MapObjects to the map
        RoomMap.mapEntities.AddMapObject(council); // First coordinate always uneven!

        Enemy GregoryPoacher = new("Gregory");
        GregoryPoacher.TreeOfChoices = new ChoiceBranch(1, "I'm gregory the poacher and I will kill all the animalls!");


        MapObject poachers = new(13, 6, MapObjectsEnum.ENEMY, true, false, "You intercepted poachers", interceptPoachers, GregoryPoacher);
        RoomMap.mapEntities.AddMapObject(poachers);
        //left right 1 3 5 7 9 11 13 15 17 19 21  //up down 1 2 3 4 5 6 7 8 9
        //add walls
        MapObject wallBase111 = new(11, 1, MapObjectsEnum.VERTICALWALL, false, true);
        RoomMap.mapEntities.AddMapObject(wallBase111);
        MapObject wallBase112 = new(11, 2, MapObjectsEnum.VERTICALWALL, false, true);
        RoomMap.mapEntities.AddMapObject(wallBase112);
        MapObject wallBase113 = new(11, 3, MapObjectsEnum.VERTICALWALL, false, true);
        RoomMap.mapEntities.AddMapObject(wallBase113);
        MapObject wallBase114 = new(11, 4, MapObjectsEnum.VERTICALWALL, false, true);
        RoomMap.mapEntities.AddMapObject(wallBase114);
        MapObject wallBase115 = new(11, 5, MapObjectsEnum.VERTICALWALL, false, true);
        RoomMap.mapEntities.AddMapObject(wallBase115);
        MapObject wallBase116 = new(11, 6, MapObjectsEnum.VERTICALWALL, false, true);
        RoomMap.mapEntities.AddMapObject(wallBase116);
        MapObject wallBase106 = new(10, 6, MapObjectsEnum.HORIZONTALWALL, false, true);
        RoomMap.mapEntities.AddMapObject(wallBase106);
        MapObject wallBase96 = new(9, 6, MapObjectsEnum.HORIZONTALWALL, false, true);
        RoomMap.mapEntities.AddMapObject(wallBase96);
        MapObject wallBase86 = new(8, 6, MapObjectsEnum.HORIZONTALWALL, false, true);
        RoomMap.mapEntities.AddMapObject(wallBase86);
        MapObject wallBase76 = new(7, 6, MapObjectsEnum.HORIZONTALWALL, false, true);
        RoomMap.mapEntities.AddMapObject(wallBase76);
        MapObject wallBase66 = new(6, 6, MapObjectsEnum.HORIZONTALWALL, false, true);
        RoomMap.mapEntities.AddMapObject(wallBase66);
        MapObject wallBase56 = new(5, 6, MapObjectsEnum.HORIZONTALWALL, false, true);
        RoomMap.mapEntities.AddMapObject(wallBase56);
        MapObject wallBase46 = new(4, 6, MapObjectsEnum.HORIZONTALWALL, false, true);
        RoomMap.mapEntities.AddMapObject(wallBase46);
        MapObject wallBase36 = new(3, 6, MapObjectsEnum.HORIZONTALWALL, false, true);
        RoomMap.mapEntities.AddMapObject(wallBase36);
        MapObject wallBase26 = new(2, 6, MapObjectsEnum.HORIZONTALWALL, false, true);
        RoomMap.mapEntities.AddMapObject(wallBase26);
        MapObject wallBase16 = new(1, 6, MapObjectsEnum.HORIZONTALWALL, false, true);
        RoomMap.mapEntities.AddMapObject(wallBase16);

      }
    }
  }
}