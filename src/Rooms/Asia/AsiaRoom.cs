using WorldOfZuul.src;
using WorldOfZuul.src.Map;

namespace WorldOfZuul
{
  class AsiaRoom : Room
  {
    bool continuePlaying = true;

    public static string? Inicials { get; set; }

    static NPC parkRanger = new("Park Ranger");

    public static bool AsiaMission = true;

    public void InProccess()
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

      InProccess();

      PrintIntroductionToTheRoom();
      previousRoom = null;

      while (continuePlaying)
      {
        CheckForMapChanges();

        if (!RoomMap.mapEntities.IsAnyQuestAvailable())
        {
          Reputation.ReputationScore += 17;
          Thread.Sleep(4000);
          GameConsole.Clear();
          GameConsole.WriteLine("Congratulations, you finished the mission!", font: FontTheme.Success);
          Thread.Sleep(3000);
          GameConsole.Clear();
          continuePlaying = false;
          Hub.isAsiaCompleted = true;
          return;
        }

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
      if (RoomMap.mapEntities.GetCurrentQuest()?.Title == "Find a poachers trap in the jungle")
      {
        RoomMap.mapEntities.RemoveMapObject(23, 7);
        RoomMap.mapEntities.RemoveMapObject(24, 8);
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
      parkRanger.Speak("Welcom to Asia ranger! Your task is to take care of the protected area, your task will be always displayed on the top of the screen. You got this!");
      Messages.PrintMissionHelp();
    }

    private void InitializeObjects()
    {
      if (AsiaRoom.AsiaMission)
      {

        //add quests 

        // Define quests
        Quest enterBuilding = new("Enter the headquarters building", "Enter the headquarters building for a mission briefing");
        Quest interceptPoachers = new("Intercept Poachers", "Gates are now open! We got a report of poachers hunting a tiger. \nObjective is clear, stop them!");
        interceptPoachers.AddPrerequisite(enterBuilding);
        Quest arrestPoachers = new("Take the poachers to prison", "You arrested the poachers. Take them for questioning");
        arrestPoachers.AddPrerequisite(interceptPoachers);
        Quest disassembleTrap = new("Find a poachers trap in the jungle", "Find and carefully disassemble one of the poachers traps used to hunt tigers");
        disassembleTrap.AddPrerequisite(arrestPoachers);

        // Initialize NPC HeadRanger and set up dialog
        NPC headRanger = new("Head Ranger");
        InitializeDialogHeadRanger(headRanger);

        // Initialize enemy Poacher and set up dialog
        Enemy poacher = new("Poacher");
        InitializeDialogPoacher(poacher);

        // Add council to the map with HeadRanger inside it
        MapObject council = new(5, 3, MapObjectsEnum.PLACE, false, false, "You have entered the operations centre", enterBuilding, headRanger); //head ranger
        RoomMap.mapEntities.AddMapObject(council); // First coordinate always uneven!

        // Add poachers to the map with interceptPoachers quest
        MapObject poachers = new(17, 6, MapObjectsEnum.ENEMY, true, false, "You intercepted poachers", interceptPoachers);
        RoomMap.mapEntities.AddMapObject(poachers);

        // Add prison to the map with arrestPoachers quest and Poacher inside it
        MapObject prison = new(7, 1, MapObjectsEnum.PRISON, false, false, "You have entered the prison", arrestPoachers, poacher); //poacher 
        RoomMap.mapEntities.AddMapObject(prison);

        // Add trap into the forest
        MapObject trap = new(25, 9, MapObjectsEnum.TRAP, true, false, "That was close! You almost stepped into the trap, great work though, you disassembled the trap.", disassembleTrap);
        RoomMap.mapEntities.AddMapObject(trap);

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
          new MapObject(23, 7, MapObjectsEnum.TREE, false, true, treeWarning), //remove later
          new MapObject(25, 7, MapObjectsEnum.TREE, false, true, treeWarning),
          new MapObject(20, 8, MapObjectsEnum.TREE, false, true, treeWarning),
          new MapObject(22, 8, MapObjectsEnum.TREE, false, true, treeWarning),
          new MapObject(24, 8, MapObjectsEnum.TREE, false, true, treeWarning), //remove later
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
              ("Yeah of course, I have been briefed before the mission", new ChoiceBranch(1, "Great, you will get more information throughout the course of the mission. \nYour first task should be now displayed on your screen shortly. \nGood luck!", isItGoodEnding: true, repGain: 10)),
              ("Unofrtunatelly, I am not quite sure", new ChoiceBranch(2, "Currently, we are facing a severe poaching crisis in Asia, particularly affecting tigers. \n Illegal trade driven by demand for tiger parts, believed to have medicinal properties and used in luxury goods, poses a grave threat to their existence. \nDespite conservation efforts, habitat loss, and strict law enforcement, the survival of these iconic big cats is in jeopardy. \nCombating the crisis requires international collaboration, anti-poaching measures, community engagement, and a shift in cultural attitudes. \nStriking a balance between economic development and wildlife conservation is essential to secure a future for tigers in the region.", //display some of the statistics in asia
                  new DialogOption[] {
                    ("Thank you, now I am ready!", new ChoiceBranch(1, "Good luck! You will need it.", isItGoodEnding: true, repGain: 17)),
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

      npc.TreeOfChoices = new ChoiceBranch(1, "Poaching across Asia is reaching critical levels, driven by an unrelenting demand for illegal wildlife products.\nI am be here to guide you through the brief introduction into the quest, the rest falls upon your individual choices. \nAre you are up for the task? Don't be mistaken, poachers around here are relentless!", choices);
    }

    private static void InitializeDialogPoacher(Enemy enemy)
    {

      //DialogOption heardEnoughOptionIndex2 = ("Thank you, I have heard enough", new ChoiceBranch(2, "Hey! What about my sentence. I thought we had an agreement."));

      //8
      DialogOption[] endingOptionArr = new DialogOption[]
      {
        ("Welcome aboard, you have been very helpful", new ChoiceBranch(1, "Hope you now understand that there's more to be done than just locking people up. \nIf you really want to stop poaching, you need to change the game.", isItGoodEnding: true, repGain: 16))
      };

      //7
      DialogOption[] soulutionsOptionArr = new DialogOption[]
      {
        ("Are you suggesting we should work together on these solutions?", new ChoiceBranch(1, "I'm no saint, but I know the game. If there's a way for me to make a living without looking over my shoulder, maybe I'd consider it.", endingOptionArr)),
        ("Can you use your experience to help transition poachers to legal, sustainable practices?", new ChoiceBranch(2, "I'm no saint, but I know the game. If there's a way for me to make a living without looking over my shoulder, maybe I'd consider it..", endingOptionArr))

      };

      //6
      DialogOption[] tackleIssueOptionArr = new DialogOption[]
      {
          ("So, what solution do you propose to tackle these issues?", new ChoiceBranch(1, "Start by investing in sustainable industries. Eco-tourism, for example, can provide jobs without harming the environment. It's about making legal options more appealing.", soulutionsOptionArr)),
          ("Do you have anything specific in mind?", new ChoiceBranch(2, "Start by investing in sustainable industries. \nEco-tourism, for example, can provide jobs without harming the environment. \nIt's about making legal options more appealing.", soulutionsOptionArr))

      };

      //5
      DialogOption[] rootOfTheProblemOptionArr = new DialogOption[]
      {
          ("What is the root of the problem, is it lack of education?", new ChoiceBranch(1, "That's part of it. People need to know what's at stake, not just for the environment but for their own futures. Education is key.", tackleIssueOptionArr)),
          ("Are there economic issues driving people towards poaching?", new ChoiceBranch(2, "Many folks, including me, see poaching as a quick way to make a living. \nIf you want them to stop, give them alternatives that pay just as well", tackleIssueOptionArr))

      };

      //4
      DialogOption[] proposeChangeOptionArr = new DialogOption[]
      {
          ("What do you propose to change this?", new ChoiceBranch(1, "Look, if you really want to make a change and stop guys like me, you need to understand the root of the problem", rootOfTheProblemOptionArr)),
          ("So you do not think this can be changed?", new ChoiceBranch(2, "Look, if you really want to make a change and stop guys like me, you need to understand the root of the problem.",rootOfTheProblemOptionArr))

      };

      //3
      DialogOption[] goOnOptionArr = new DialogOption[]
      {
      ("Go on, I am listening", new ChoiceBranch(1, "The penalties are high, but the demand for exotic animals and their parts is even higher. \nThe risk seemed worth the potential rewards.",
          new DialogOption[]
          {
              ("Have you considered legal alternatives that could bring financial benefits without harming the environment?", new ChoiceBranch(1, "It's all about the money. Conservation efforts don't put food on the table.\n I don't care about the long-term consequences; I need to survive today", proposeChangeOptionArr)),
              ("Don't you mind exploiting the natural resources?", new ChoiceBranch(2, "I'm just one person. If I don't exploit the resources, someone else will. The system needs to change if you want people like me to stop.",proposeChangeOptionArr)),
          }
      )),
      ("Thank you, I have heard enough", new ChoiceBranch(2, "Hey! What about my sentence. I thought we had an agreement."))
      };

      //2
      DialogOption[] makeHimTalkOptionArr = new DialogOption[]
      {
        ("I thought you agreed to talk", new ChoiceBranch(1, "Jobs are limited, and the allure of quick cash from poaching is hard to resist. \nIt's not just about survival; it's about the lack of alternatives.", goOnOptionArr)),
        ("No, I am just a ranger, but you are in my custody. And I can alter your life in any direction I want!", new ChoiceBranch(2, "Okay, okay. Money's tight, and jobs are scarce. \nI saw an opportunity to make a quick profit by supplying exotic animals and their parts to the black market. It's survival for me, too.", goOnOptionArr )),
      };

      //1
      DialogOption talkOption = (
          "If you cooperate and let me see your side of the story, your sentence will be lighter",
          new ChoiceBranch(1, "I am all ears.",
              new DialogOption[] {
                ("What drove you to become a poacher in this unique environment?", new ChoiceBranch(1, "What are you, a psychologist?", makeHimTalkOptionArr)),
                ("Do you have a history of poaching, or is this a recent turn to illegal activities?", new ChoiceBranch(2, "What are you, a psychologist?", makeHimTalkOptionArr)),
              }
          )
      );

      DialogOption doNotTalkOption = ("I am done with you here, your actions will have consequences", new ChoiceBranch(2, "I couldn't care less about your precious environment. \nI will be out soon and your efforts will come in vain. Call my lawyer!"));

      var choices = new DialogOption[] {
        talkOption, // first option so nr 1
        doNotTalkOption, // second option so nr 2
      };

      enemy.TreeOfChoices = new ChoiceBranch(1, "You think you can lock me up and stop me? I have been doing this for years!", choices);
    }

  }
}
