using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldOfZuul;
namespace WorldOfZuul.Africa
{
  public class AfricaRoom : Room
  {
    /// <summary>
    /// Space for africa and its inner rooms
    /// </summary>
    /// 
    const int CAMP_WIDTH = 40;

    private MissionRoom? submarine;
    private MissionRoom? camp;
    // private MissionRoom? jungle;

    //SUBMARINE NPC
    readonly NPC hal = new("HAL");

    //CAMP NPS
    //npc1
    readonly NPC Kenny = new("");
    //TODO: create quests
    //npc2
    readonly NPC Josh = new("");
    //npc3
    readonly NPC Manadrine = new("");

    MissionGameRooms? JsonAfricaRooms = null;
    bool continuePlaying = true;

    public AfricaRoom(
      string? shortDesc,
      string? longDesc,
      string? msgOnArrival
    ) : base(shortDesc, longDesc)
    {
    }

    public void StartAfricaMission(ref Room? currentRoom, ref Room? previousRoom)
    {
      JsonAfricaRooms = JsonFileReader.GetAfricaRooms();

      GameConsole.WriteLine(LongDescription, font: FontTheme.HighligtedText);

      if (JsonAfricaRooms == null || JsonAfricaRooms.Rooms == null)
      {
        throw new Exception("No africa rooms");
      };

      submarine = new(
        JsonAfricaRooms.Rooms[(int)AfricaRoomsEnum.SUBMARINE].ShortDesc,
        JsonAfricaRooms.Rooms[(int)AfricaRoomsEnum.SUBMARINE].LongDesc,
        JsonAfricaRooms.Rooms[(int)AfricaRoomsEnum.SUBMARINE].MissionDescription,
        JsonAfricaRooms.Rooms[(int)AfricaRoomsEnum.SUBMARINE].MessageOnArrival,
        new Map(height: 6, width: 8)
        );

      camp = new(
        JsonAfricaRooms.Rooms[(int)AfricaRoomsEnum.CAMP].ShortDesc,
        JsonAfricaRooms.Rooms[(int)AfricaRoomsEnum.CAMP].LongDesc,
        JsonAfricaRooms.Rooms[(int)AfricaRoomsEnum.CAMP].MissionDescription,
        JsonAfricaRooms.Rooms[(int)AfricaRoomsEnum.CAMP].MessageOnArrival,
        new Map(width: CAMP_WIDTH, height: 11),
        JsonAfricaRooms.Rooms[(int)AfricaRoomsEnum.CAMP].ExtendedDescription
      );
      //TODO: add river to thh map
      InSubmarine();

      Messages.PrintMissionHelp();
      submarine.DisplayMessageOnArrival();

      previousRoom = null;
      // currentRoom = submarine;
      currentRoom = camp;
      InCamp();

      Command? cmnd = new Command("map", "on");
      Actions.DecideAction(ref cmnd, ref currentRoom, ref previousRoom, true);

      while (continuePlaying)
      {
        if (currentRoom == submarine && !submarine.RoomMap.mapEntities.IsAnyQuestAvailable())
        {
          currentRoom = camp;
          InCamp();
        }

        if (currentRoom == camp && !camp.RoomMap.mapEntities.IsAnyQuestAvailable())
        {
          Console.WriteLine("Move to jungle");
        }

        // Messages.PrintMissionHelp();
        currentRoom?.DisplayShortDescription();

        Command? command = Game.AskForCommand();
        continuePlaying = Actions.DecideAction(ref command, ref currentRoom, ref previousRoom, true, "africa");
      }
    }

    private void InSubmarine()
    {
      BuildHal();
      //Submarine quest + hals
      Quest startMission = new("Enbark on a mission!", "Talk to hal in order to start the mission");
      MapObject halMpOb = new(3, 6, MapObjectsEnum.NPC, false, true, "Hal!", startMission, npc: hal);
      submarine?.RoomMap.mapEntities.AddMapObject(halMpOb);
    }

    private void InCamp()
    {
      //TODO: reset player coordinates 
      Messages.PrintShowcaseOfJungle();
      camp?.DisplayMessageOnArrival();
      BuildCampNpcs();
      BuildRiver();
      BuildCampHut();

      camp?.RoomMap.SetXandY(13, 6);

      //quests
      Quest talkToKennyQuest = new("Talk to the boss!", "It seems that Kenny is the chief of whole operation, I should talk to him.");
      Quest getCluesFromMandarine = new("Gather more data.", "Appears that Kenny doesn't know too much. I'll have to ask Mandarine for more information.", new List<Quest> { talkToKennyQuest });
      Quest sayHiToJoshQuest = new("Say hi to Josh.", "Mandarine said that Josh were felling a bit down recently. It wouldn't harm you just to say hi to him. Right?", new List<Quest> { talkToKennyQuest, getCluesFromMandarine });
      Quest leaveBuildingQuest = new("Save the giraffe!", "There is no time to waste you need to get out of the building and try your best to save them.", new List<Quest> { talkToKennyQuest, getCluesFromMandarine, sayHiToJoshQuest });


      //npcs
      MapObject kennyMpOb = new(37, 4, MapObjectsEnum.NPC, quest: talkToKennyQuest, npc: Kenny, occupiedMessage: "NPC: Kenny - Boss");
      MapObject mandarineMpOb = new(31, 8, MapObjectsEnum.NPC, quest: getCluesFromMandarine, npc: Manadrine, occupiedMessage: "NPC: Mandarine - Brains");
      MapObject joshMpOb = new(31, 3, MapObjectsEnum.NPC, npc: Josh, quest: sayHiToJoshQuest, occupiedMessage: "NPC: Josh - Muscles");

      //exit
      MapObject exitMpOb = new(39, 6, MapObjectsEnum.PLACE, quest: leaveBuildingQuest, occupiedMessage: "Exit");

      camp?.RoomMap.mapEntities.PopulateMap(
        new MapObject[]{
          kennyMpOb,
          mandarineMpOb,
          joshMpOb,
          exitMpOb
        }
      );
    }
    //TODO: npc
    private void BuildCampNpcs()
    {
      BuildKenny();
      BuildJosh();
      BuildMandarine();
    }

    public void BuildRiver()
    {
      MapObject river22 = new(1, 11, MapObjectsEnum.DIAGONALWALL_RIGHT);
      MapObject river10 = new(3, 10, MapObjectsEnum.VERTICALWALL);
      MapObject river9 = new(3, 9, MapObjectsEnum.DIAGONALWALL_RIGHT);
      MapObject river8 = new(5, 8, MapObjectsEnum.VERTICALWALL);
      MapObject river7 = new(3, 7, MapObjectsEnum.DIAGONALWALL_LEFT);
      MapObject river6 = new(3, 6, MapObjectsEnum.VERTICALWALL);
      MapObject river5 = new(3, 5, MapObjectsEnum.DIAGONALWALL_RIGHT);
      MapObject river4 = new(5, 4, MapObjectsEnum.VERTICALWALL);
      MapObject river3 = new(5, 3, MapObjectsEnum.DIAGONALWALL_RIGHT);
      MapObject river2 = new(7, 2, MapObjectsEnum.VERTICALWALL);
      MapObject river1 = new(5, 1, MapObjectsEnum.DIAGONALWALL_LEFT);

      MapObject river11 = new(11, 1, MapObjectsEnum.DIAGONALWALL_LEFT);
      MapObject river12 = new(13, 2, MapObjectsEnum.VERTICALWALL);
      MapObject river13 = new(11, 3, MapObjectsEnum.DIAGONALWALL_RIGHT);
      MapObject river14 = new(11, 4, MapObjectsEnum.VERTICALWALL);
      MapObject river15 = new(9, 5, MapObjectsEnum.DIAGONALWALL_RIGHT);
      MapObject river16 = new(9, 6, MapObjectsEnum.VERTICALWALL);
      MapObject river17 = new(9, 7, MapObjectsEnum.DIAGONALWALL_LEFT);
      MapObject river18 = new(11, 8, MapObjectsEnum.VERTICALWALL);
      MapObject river19 = new(9, 9, MapObjectsEnum.DIAGONALWALL_RIGHT);
      MapObject river20 = new(9, 10, MapObjectsEnum.VERTICALWALL);
      MapObject river21 = new(7, 11, MapObjectsEnum.DIAGONALWALL_RIGHT);
      camp?.RoomMap.mapEntities.PopulateMap(
              new MapObject[]{
                river1,
                river2,
                river3,
                river4,
                river5,
                river6,
                river7,
                river8,
                river9,
                river10,
                river11,
                river12,
                river13,
                river14,
                river15,
                river16,
                river17,
                river18,
                river19,
                river20,
                river21,
                river22
      });
    }
    public void BuildCampHut()
    {
      MapObject wall4 = new(29, 3, MapObjectsEnum.VERTICALWALL);
      MapObject wall5 = new(29, 4, MapObjectsEnum.VERTICALWALL);
      MapObject wall6 = new(29, 5, MapObjectsEnum.VERTICALWALL);

      MapObject wall7 = new(29, 7, MapObjectsEnum.VERTICALWALL);
      MapObject wall8 = new(29, 8, MapObjectsEnum.VERTICALWALL);
      MapObject wall9 = new(29, 9, MapObjectsEnum.VERTICALWALL);

      MapObject wall10 = new(29, 2, MapObjectsEnum.HORIZONTALWALL);
      MapObject wall11 = new(30, 2, MapObjectsEnum.HORIZONTALWALL);
      MapObject wall12 = new(31, 2, MapObjectsEnum.HORIZONTALWALL);
      MapObject wall16 = new(32, 2, MapObjectsEnum.HORIZONTALWALL);
      MapObject wall18 = new(33, 2, MapObjectsEnum.HORIZONTALWALL);
      MapObject wall19 = new(34, 2, MapObjectsEnum.HORIZONTALWALL);
      MapObject wall20 = new(35, 2, MapObjectsEnum.HORIZONTALWALL);
      MapObject wall21 = new(36, 2, MapObjectsEnum.HORIZONTALWALL);
      MapObject wall22 = new(37, 2, MapObjectsEnum.HORIZONTALWALL);
      MapObject wall23 = new(38, 2, MapObjectsEnum.HORIZONTALWALL);
      MapObject wall24 = new(39, 2, MapObjectsEnum.HORIZONTALWALL);

      MapObject wall13 = new(29, 9, MapObjectsEnum.HORIZONTALWALL);
      MapObject wall25 = new(30, 9, MapObjectsEnum.HORIZONTALWALL);
      MapObject wall26 = new(31, 9, MapObjectsEnum.HORIZONTALWALL);
      MapObject wall14 = new(32, 9, MapObjectsEnum.HORIZONTALWALL);
      MapObject wall15 = new(33, 9, MapObjectsEnum.HORIZONTALWALL);
      MapObject wall17 = new(34, 9, MapObjectsEnum.HORIZONTALWALL);
      MapObject wall27 = new(35, 9, MapObjectsEnum.HORIZONTALWALL);
      MapObject wall28 = new(36, 9, MapObjectsEnum.HORIZONTALWALL);
      MapObject wall29 = new(37, 9, MapObjectsEnum.HORIZONTALWALL);
      MapObject wall30 = new(38, 9, MapObjectsEnum.HORIZONTALWALL);
      MapObject wall31 = new(39, 9, MapObjectsEnum.HORIZONTALWALL);
      camp?.RoomMap.mapEntities.PopulateMap(
              new MapObject[]{
                wall4,
                wall5,
                wall6,
                wall7,
                wall8,
                wall9,
                wall10,
                wall11,
                wall12,
                wall13,
                wall14,
                wall15,
                wall16,
                wall17,
                wall18,
                wall19,
                wall20,
                wall21,
                wall22,
                wall23,
                wall24,
                wall25,
                wall26,
                wall27,
                wall28,
                wall29,
                wall30,
                wall31,

      });

    }
    private void BuildJosh()
    {
      DialogOption talkOptionJosh = (
        "Hello mister?", new ChoiceBranch(1, "Ah yes! you must be the guy!",
        new DialogOption[] {
          ("Which guy?", new ChoiceBranch(1,"You know the guy! One who will solve all of our problems just by showing up and talking to people!", new DialogOption[] {
            ("Are you making fun of me?", new ChoiceBranch(1, "Never! I really admire you. But it's hard for me to believe that one man can make a difference... Sadly.", isItGoodEnding: true)),
            ("That's me! You've described me perfectly.", new ChoiceBranch(2, "Of course I'm never mistaken. Thank you for talking with me, it means a lot to me.", isItGoodEnding: true))
          }))
        })
      );

      DialogOption chitchatOptionJosh = ("What's your favourite kind of ice cream?", new ChoiceBranch(2, "I love chocolate ones. At least I used to now it looks like mud to me. You know what? I'll give you advise, for beign nice enough to talk to me. REMEBER: never trust anyone in the jungle. You can't even imagine how sly poachers can be! Anyway thank you for the effort, you're very kind.", isItGoodEnding: true));

      DialogOption quitOptionJosh = (
             "End conversation", new ChoiceBranch(3, "Nice meeting you. I needed to talk to someone.", isItGoodEnding: true)
      );
      var joshChoices = new DialogOption[] {
        talkOptionJosh,
        chitchatOptionJosh,
        quitOptionJosh,
      };

      Josh.TreeOfChoices = new ChoiceBranch(1, "Josh. Big, very muscular guy.", joshChoices);
    }
    private void BuildMandarine()
    {
      if (camp?.MissionDescription == null || camp?.ExtendedDescription == null) throw new Exception("Camp Mission Description empty");

      string sendToJosh = "\nBy the way... Josh is not feeling good. Could you just drop by and say hi to him?";
      DialogOption[] talkOptionsMandarine = new DialogOption[]{
        ("Overall goal", new ChoiceBranch(1, camp.MissionDescription , new DialogOption[] {
          ("What is she like?", new ChoiceBranch(1, camp.ExtendedDescription + sendToJosh, isItGoodEnding: true)),
          ("What is your number ;)", new ChoiceBranch(2, "You've crossed a line. Bye."))
        })),
        ("What is she like?", new ChoiceBranch(2, camp.ExtendedDescription, new DialogOption[] {
          ("Overall goal", new ChoiceBranch(1, camp.MissionDescription + sendToJosh, isItGoodEnding: true)),
          ("What is your number ;)", new ChoiceBranch(2, "You've crossed a line. Bye."))
        })),
        ("What is your number ;)", new ChoiceBranch(3, "You've crossed a line. Bye."))
      };

      DialogOption quitOptionMandarine = (
             "End conversation", new ChoiceBranch(3, "Bye? That was weird...")
      );

      DialogOption chitchatOptionMandarine = ("What's your favourite kind of ice cream?", new ChoiceBranch(4, "I don't like ice cream"));

      var mandarineChoices = new DialogOption[] {
        ("Could some tell me what is going on here!", new ChoiceBranch(1,"Well, don't be so nervous. I'm Mandarine by the way, and yes I can help you. What do you need to know?", talkOptionsMandarine)),
        ("Sorry. I'm new here. Was sent here for the mission but I'll need more details if I'm going to head right into the jungle.", new ChoiceBranch(2, "Of course you need more details hun! My name is Mandarine. I'll tell you all you need to know.", talkOptionsMandarine)),
        quitOptionMandarine,
        chitchatOptionMandarine
      };

      Manadrine.TreeOfChoices = new ChoiceBranch(1, "Mandarine :)", mandarineChoices);
    }
    private void BuildKenny()
    {
      //Kenny
      if (camp?.MissionDescription == null) throw new Exception("Camp Mission Description empty");
      DialogOption talkOptionKenny = (
        "Hi I've heard that you're the boss?", new ChoiceBranch(1, "That I am.",
          new DialogOption[] {
            ("I'm here to help. Could you introduce me to the whole situation that's going on in here?", new ChoiceBranch(1, "Sure I can.",
              new DialogOption[] {
                ("so.....?", new ChoiceBranch(1, "OK FINE, I'll tell you. Ekhem...\nemmm we are... helping animals? ...\nYou know what. Don't bother me with such a nonsens. Why don't you ask Mandarine? She loves to talk about all those THINGS. pff", isItGoodEnding: true))
              }
            )),
            ("Tell me all the stuff and let me be gone from here!", new ChoiceBranch(2, "Oh no, no, no. I won't accept this kind of rudness. I won't talk with you any longer!"))
          }
        )
      );
      DialogOption quitOptionKenny = (
             "End conversation", new ChoiceBranch(2, "ugh")
      );

      DialogOption chitchatOptionKenny = ("What's your favourite kind of ice cream?", new ChoiceBranch(3, "Are you serious?! We're dealing with far more serious topics than Ice Cream here. Go back to your sandbox where you came from!"));

      var kennyChoices = new DialogOption[] {
        talkOptionKenny, // first option so nr 1 
        quitOptionKenny,
        chitchatOptionKenny
      };
      Kenny.TreeOfChoices = new ChoiceBranch(1, "Kenny. The Boss.", kennyChoices);
    }
    private void BuildHal()
    {
      //choices for josh
      if (submarine?.MissionDescription == null) throw new Exception("Submarine Mission Description empty");

      DialogOption talkOption = (
        "Give me a deeper insight of the mission", new ChoiceBranch(1,
        submarine.MissionDescription,
            new DialogOption[] {
              ("Pick up the earphone", new ChoiceBranch(1, "Great! Now we can start the mission",
                   new DialogOption[] {
                      ("Let's go!", new ChoiceBranch(1, "You feel sudden jerk and submarine starts getting closer to the surffice faster than you'd expect. Hal tells you to move to the other ship and your journey begins...", isItGoodEnding: true)),
                   }
              )),
              ("Hit the steering panel", new ChoiceBranch(2, "Auuch! What are you doing??"))
            }
        )
      );

      DialogOption quitOption = (
        "End conversation", new ChoiceBranch(2, "...bye then!")
      );


      var choices = new DialogOption[] {
        talkOption, // second option so nr 1 
        quitOption
      };

      hal.TreeOfChoices = new ChoiceBranch(1, "Hi I'm hal", choices);
    }
  }
}
