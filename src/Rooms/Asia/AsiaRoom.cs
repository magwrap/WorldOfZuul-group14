using WorldOfZuul.src.Map;

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
        CheckForMapChanges();
        Command? command = Game.AskForCommand();
        continuePlaying = Actions.DecideAction(ref command, ref currentRoom, ref previousRoom, true, "asia");

        
      }

    }
    private void CheckForMapChanges()
    {
        if (RoomMap.mapEntities.GetCurrentQuest()?.Title == "Intercept Poachers")
        {
          RoomMap.mapEntities.RemoveMapObject(11, 3);
          RoomMap.mapEntities.RemoveMapObject(11, 4);
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

        // Define quests
        Quest enterBuilding = new("Enter the headquarters building", "Enter the headquarters building for a mission briefing");
        Quest interceptPoachers = new("Intercept Poachers", "Gates are now open! We got a report of poachers hunting a tiger. \nObjective is clear, stop them!");
        interceptPoachers.AddPrerequisite(enterBuilding);
        Quest arrestPoachers = new("Take the poachers to prison", "Take the poachers for questioning");
        arrestPoachers.AddPrerequisite(interceptPoachers);
        Quest disassembleTrap = new("Find a poachers trap in the forest", "Find and carefully disassemble one of the poachers traps used to hunt tigers");
        disassembleTrap.AddPrerequisite(arrestPoachers);

        // Initialize NPC HeadRanger and set up dialog
        NPC headRanger = new("Head Ranger");
        InitializeDialogHeadRanger(headRanger);

        // Add council to the map with HeadRanger inside it
        MapObject council = new(5, 3, MapObjectsEnum.PLACE, false, false, "You have entered the operations centre", enterBuilding, headRanger);

        RoomMap.mapEntities.AddMapObject(council); // First coordinate always uneven!

        // Initialize enemy Poacher and set up dialog
        Enemy poacher = new("Poacher");
        InitializeDialogPoacher(poacher);

        // Add poachers to the map with interceptPoachers quest
        MapObject poachers = new(17, 6, MapObjectsEnum.ENEMY, true, false, "You intercepted poachers", interceptPoachers);
        RoomMap.mapEntities.AddMapObject(poachers);

        // Add prison to the map with arrestPoachers quest and Poacher inside it
        MapObject prison = new(7, 1, MapObjectsEnum.PRISON, false, false, "You have entered the prison", arrestPoachers, poacher);
        RoomMap.mapEntities.AddMapObject(prison);

        // Add trap into the forest
        MapObject trap = new(25, 9, MapObjectsEnum.TRAP, true, false, "That was close! You almost stepped into the trap, great work though, just disassemble the trap.", disassembleTrap);
        RoomMap.mapEntities.AddMapObject(trap);


        //left right 1 3 5 7 9 11 13 15 17 19 21  //up down 1 2 3 4 5 6 7 8 9
        //add walls
        MapObject[] mapWalls = new MapObject[]
        {
          new MapObject(11, 1, MapObjectsEnum.VERTICALWALL, false, true),
          new MapObject(11, 2, MapObjectsEnum.VERTICALWALL, false, true),
          new MapObject(11, 3, MapObjectsEnum.VERTICALWALL, false, true),
          new MapObject(11, 4, MapObjectsEnum.VERTICALWALL, false, true),
          new MapObject(11, 5, MapObjectsEnum.VERTICALWALL, false, true),
          new MapObject(11, 6, MapObjectsEnum.VERTICALWALL, false, true),
          new MapObject(10, 6, MapObjectsEnum.HORIZONTALWALL, false, true),
          new MapObject(9, 6, MapObjectsEnum.HORIZONTALWALL, false, true),
          new MapObject(8, 6, MapObjectsEnum.HORIZONTALWALL, false, true),
          new MapObject(7, 6, MapObjectsEnum.HORIZONTALWALL, false, true),
          new MapObject(6, 6, MapObjectsEnum.HORIZONTALWALL, false, true),
          new MapObject(5, 6, MapObjectsEnum.HORIZONTALWALL, false, true),
          new MapObject(4, 6, MapObjectsEnum.HORIZONTALWALL, false, true),
          new MapObject(3, 6, MapObjectsEnum.HORIZONTALWALL, false, true),
          new MapObject(2, 6, MapObjectsEnum.HORIZONTALWALL, false, true),
          new MapObject(1, 6, MapObjectsEnum.HORIZONTALWALL, false, true),
        };
        RoomMap.mapEntities.PopulateMap(mapWalls);

        string treeWarning = "Be careful to not bump into trees, we are protecting all organisms of the ecosystem";
        
        MapObject[] mapTrees = new MapObject[]
        { 
          new MapObject(22, 5, MapObjectsEnum.TREE, false, true, treeWarning),
          new MapObject(26, 5, MapObjectsEnum.TREE, false, true, treeWarning),
          new MapObject(20, 6, MapObjectsEnum.TREE, false, true, treeWarning),
          new MapObject(24, 6, MapObjectsEnum.TREE, false, true, treeWarning),
          new MapObject(27, 6, MapObjectsEnum.TREE, false, true, treeWarning),
          new MapObject(19, 7, MapObjectsEnum.TREE, false, true, treeWarning),
          new MapObject(21, 7, MapObjectsEnum.TREE, false, true, treeWarning),
          new MapObject(25, 7, MapObjectsEnum.TREE, false, true, treeWarning),
          new MapObject(20, 8, MapObjectsEnum.TREE, false, true, treeWarning),
          new MapObject(22, 8, MapObjectsEnum.TREE, false, true, treeWarning),
          new MapObject(26, 8, MapObjectsEnum.TREE, false, true, treeWarning),
          new MapObject(28, 8, MapObjectsEnum.TREE, false, true, treeWarning),
          new MapObject(19, 9, MapObjectsEnum.TREE, false, true, treeWarning),
          new MapObject(21, 9, MapObjectsEnum.TREE, false, true, treeWarning),
          new MapObject(23, 9, MapObjectsEnum.TREE, false, true, treeWarning),
          new MapObject(27, 9, MapObjectsEnum.TREE, false, true, treeWarning),
          new MapObject(29, 9, MapObjectsEnum.TREE, false, true, treeWarning),
          new MapObject(22, 10, MapObjectsEnum.TREE, false, true, treeWarning),
          new MapObject(25, 10, MapObjectsEnum.TREE, false, true, treeWarning),
          new MapObject(27, 10, MapObjectsEnum.TREE, false, true, treeWarning),


        };
        RoomMap.mapEntities.PopulateMap(mapTrees);

      }
    }
    private static void InitializeDialogHeadRanger(NPC npc)
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

    private static void InitializeDialogPoacher(Enemy enemy)
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