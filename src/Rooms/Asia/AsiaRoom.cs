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
      parkRanger.Speak("Come in...Over");
      Thread.Sleep(2000);
      GameConsole.Clear();
      parkRanger.Speak("Come in Ranger...Over");
      Thread.Sleep(3000);
      GameConsole.Clear();
      parkRanger.Speak("Welcom to Asia ranger! By using command 'map on', you will always be able to see the protected area, your task will be always displayed on the top of the screen. You got this!");
      Messages.PrintMissionHelp();
    }

    private void InitializeObjects()
    {
      if (AsiaRoom.AsiaMission)
      {
        //Console.WriteLine("Initializing objects...");
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
        Quest enterBuilding = new Quest("Enter the headquarters building", "Enter the headquarters building for a mission briefing"); //first string is shown as a current quest, second string is shown as a quest description
        interceptPoachers.AddPrerequisite(enterBuilding);

        //Add HeadRanger to the building, headquarters
        NPC HeadRanger = new("Head Ranger");
        InitializeDialogHeadRanger(HeadRanger);

        NewAsiaMission
        // Add coucil to the map with HeadRanger inside of it
        MapObject council = new(5, 4, MapObjectsEnum.PLACE, false, false, "You have entered the operations centre", enterBuilding, HeadRanger);
        RoomMap.mapEntities.AddMapObject(council); // First coordinate always uneven!

        Enemy Poacher = new("Poacher");
        InitializeDialogPoacher(Poacher);

        MapObject poachers = new(13, 6, MapObjectsEnum.ENEMY, true, false, "You intercepted poachers", interceptPoachers, Poacher);
        RoomMap.mapEntities.AddMapObject(poachers);


        //left right 1 3 5 7 9 11 13 15 17 19 21  //up down 1 2 3 4 5 6 7 8 9
        //add walls
        MapObject wallBase111 = new(11, 1, MapObjectsEnum.VERTICALWALL, false, true);
        RoomMap.mapEntities.AddMapObject(wallBase111);
        MapObject wallBase112 = new(11, 2, MapObjectsEnum.VERTICALWALL, false, true);
        RoomMap.mapEntities.AddMapObject(wallBase112);
        MapObject wallBase113 = new(11, 3, MapObjectsEnum.VERTICALWALL, false, true);
        RoomMap.mapEntities.AddMapObject(wallBase113);
        
        //MapObject wallBase114 = new(11, 4, MapObjectsEnum.VERTICALWALL, false, true);
        //RoomMap.mapEntities.AddMapObject(wallBase114);

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
    private void InitializeDialogHeadRanger(NPC npc)
    {

      var talkOption = (
         "Yes", new ChoiceBranch(1, "Love to hear that! Are you familiar with the issues we are currently fighting?",
             new DialogOption[] {
              ("Yeah of course, I have been briefed before the mission", new ChoiceBranch(1, "Great, you will get more information throughout the course of the mission. \nYour first task should be now displayed on your screen shortly. \nGood luck!", isItGoodEnding: true)),
              ("Unofrtunatelly, I am not quite sure", new ChoiceBranch(2, "Currently we are facing", //display some of the statistics in asia
                  new DialogOption[] {
                    ("Thank you, now I am ready!", new ChoiceBranch(1, "Good luck! You will need it.", isItGoodEnding: true)),
                  }
                )
              )
             }
         )
       );

      var doNotTalkOption = ("No", new ChoiceBranch(2, "I am sorry, to hear that, we really needed you."));

      var choices = new DialogOption[] {
        talkOption, // first option so nr 1
        doNotTalkOption, // second option so nr 2
        };

      npc.TreeOfChoices = new ChoiceBranch(0, "Poaching across Asia is reaching critical levels, driven by an unrelenting demand for illegal wildlife products.\nI am be here to guide you through the brief introduction into the quest, the rest falls upon your individual choices. \nAre you are up for the task? Don't be mistaken, poachers around here are relentless!", choices);
    }

    private void InitializeDialogPoacher(Enemy enemy)
    {
      var talkOption = (
      "Yes", new ChoiceBranch(1, "Good, let's talk. I'm here to understand your perspective. Why did you choose to engage in illegal poaching?",
          new DialogOption[] {
                ("Survival, I have no other option.", new ChoiceBranch(1, "I see. Poverty is a significant issue. Did you know there are alternative livelihood programs that can provide sustainable income without harming wildlife?")),
                ("I enjoy the thrill of hunting.", new ChoiceBranch(2, "Interesting. Hunting for sport can be harmful to ecosystems. Let me share some insights about the ecological impact of illegal poaching.",
                    new DialogOption[] {
                        ("Go ahead, enlighten me.", new ChoiceBranch(1, "Illegal poaching disrupts ecosystems and threatens biodiversity. Your actions contribute to the imbalance in nature. Have you considered the consequences?")),
                        ("I don't care about the environment.", new ChoiceBranch(2, "Your perspective is concerning. The health of our planet affects us all. Let me share some facts about the importance of wildlife conservation.",
                            new DialogOption[] {
                                ("Fine, tell me more.", new ChoiceBranch(1, "Biodiversity is essential for a stable environment. Poaching jeopardizes this delicate balance. Your role in conservation can make a difference.")),
                                ("I'm not interested.", new ChoiceBranch(2, "It's unfortunate that you're not interested. However, your cooperation is crucial for resolving this issue. Let's focus on the mission at hand.")),
                            }
                        )),
                    }
                )),
          }
        )
      );

      var doNotTalkOption = ("No", new ChoiceBranch(2, "Your silence won't help you or the environment. I hope you reconsider."));

      var choices = new DialogOption[] {
        talkOption, // first option so nr 1
        doNotTalkOption, // second option so nr 2
      };

      enemy.TreeOfChoices = new ChoiceBranch(0, "Greetings, poacher! I'm here to understand your situation. Willing to share your side of the story?", choices);
    }
  }
}