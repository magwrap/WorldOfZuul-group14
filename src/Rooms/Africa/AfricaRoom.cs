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
        new Map(width: CAMP_WIDTH, height: 11)
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
        if (!submarine.RoomMap.mapEntities.IsAnyQuestAvailable() && currentRoom == submarine)
        {
          currentRoom = camp;
          InCamp();
        }

        // Messages.PrintMissionHelp();
        currentRoom?.DisplayShortDescription();

        Command? command = Game.AskForCommand();
        continuePlaying = Actions.DecideAction(ref command, ref currentRoom, ref previousRoom, true, "africa");
      }
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
      Quest getCluesFromMandarine = new("Ask Mandarine about ____", "Kenny told you that Mandarine know how to ______", new List<Quest> { talkToKennyQuest });
      Quest leaveBuildingQuest = new("Save the giraffe!", "There is no time to waste you need to get out of the building and try your best to save them.", new List<Quest> { talkToKennyQuest, getCluesFromMandarine });


      //npcs
      MapObject kennyMpOb = new(37, 4, MapObjectsEnum.NPC, quest: talkToKennyQuest, npc: Kenny, occupiedMessage: "NPC: Kenny - Boss");
      MapObject mandarineMpOb = new(31, 8, MapObjectsEnum.NPC, quest: getCluesFromMandarine, npc: Manadrine, occupiedMessage: "NPC: Mandarine - Brains");
      MapObject joshMpOb = new(31, 3, MapObjectsEnum.NPC, npc: Josh, occupiedMessage: "NPC: Josh - Muscles");


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
        talkOption, // first option so nr 1 
        quitOption
      };

      hal.TreeOfChoices = new ChoiceBranch(1, "Hi I'm hal", choices);
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


    private void InSubmarine()
    {
      BuildHal();
      //Submarine quest + hals
      Quest startMission = new("Enbark on a mission!", "Talk to hal in order to start the mission");
      MapObject halMpOb = new(3, 6, MapObjectsEnum.NPC, false, true, "Hal!", startMission, npc: hal);
      submarine?.RoomMap.mapEntities.AddMapObject(halMpOb);
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
