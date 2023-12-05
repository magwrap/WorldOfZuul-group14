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
        new Map(width: 40)
      );
      //TODO: add river to thh map
      InSubmarine();

      Messages.PrintMissionHelp();
      submarine.DisplayMessageOnArrival();

      previousRoom = null;
      currentRoom = submarine;

      Command? cmnd = new Command("map on");
      Actions.DecideAction(ref cmnd, ref currentRoom, ref previousRoom, true);

      while (continuePlaying)
      {
        if (!submarine.RoomMap.mapEntities.IsAnyQuestAvailable() && currentRoom == submarine)
        {
          currentRoom = camp;
          InCamp();
        }

        Messages.PrintMissionHelp();
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

      //quests
      Quest talkToKennyQuest = new("Talk to the boss!", "It seems that Kenny is the chief of whole operation, I should talk to him.");
      Quest getCluesFromMandarine = new("Ask Mandarine about ____", "Kenny told you that Mandarine know how to ______", new List<Quest> { talkToKennyQuest });
      Quest leaveBuildingQuest = new("Save the giraffe!", "There is no time to waste you need to get out of the building and try your best to save them.", new List<Quest> { talkToKennyQuest, getCluesFromMandarine });


      //npcs
      MapObject kennyMpOb = new(3, 6, MapObjectsEnum.NPC, quest: talkToKennyQuest, npc: Kenny);
      MapObject mandarineMpOb = new(5, 6, MapObjectsEnum.NPC, quest: getCluesFromMandarine, npc: Manadrine);
      MapObject joshMpOb = new(7, 6, MapObjectsEnum.NPC, npc: Josh);


      //exit
      MapObject exitMpOb = new(39, 6, MapObjectsEnum.PLACE, quest: leaveBuildingQuest);

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
      MapObject river1 = new(3, 3, MapObjectsEnum.DIAGONALWALL);
      MapObject river2 = new(3, 4, MapObjectsEnum.VERTICALWALL);


      //TODO: river
      camp?.RoomMap.mapEntities.PopulateMap(
        new MapObject[]{
          river1,
          river2
        }
      );
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
        "End conversation", new ChoiceBranch(1, "...bye then!")
      );


      var choices = new DialogOption[] {
        talkOption, // second option so nr 1 
        quitOption
      };

      hal.TreeOfChoices = new ChoiceBranch(1, "Hi I'm hal", choices);
    }
  }
}
