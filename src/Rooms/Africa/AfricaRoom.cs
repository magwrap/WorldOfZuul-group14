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
    readonly NPC hal = new("HAL");
    MissionGameRooms? JsonAfricaRooms = null;

    // private MissionRoom? jungle;
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
        JsonAfricaRooms.Rooms[(int)AfricaRoomsEnum.CAMP].MessageOnArrival
      );
      //TODO: add river to thh map

      BuildChoiceTree();
      InitializeObjects();
      //TODO: fix map objects are not initialized
      submarine.SetExit("camp", camp);
      submarine.DisplayMessageOnArrival();

      previousRoom = null;
      currentRoom = submarine;

      Command? cmnd = new Command("map on");
      Actions.DecideAction(ref cmnd, ref currentRoom, ref previousRoom, true);

      while (continuePlaying)
      {


        Command? command = Game.AskForCommand();
        continuePlaying = Actions.DecideAction(ref command, ref currentRoom, ref previousRoom, true, "africa");
      }
    }

    private void SubmarineLoop()
    {

    }

    private void InitializeObjects()
    {


      Quest startMission = new("Enbark on a mission!", "Talk to hal in order to start the mission");

      MapObject halMpOb = new(3, 1, MapObjectsEnum.NPC, false, false, "sdffd", startMission, npc: hal);
      submarine?.RoomMap.mapEntities.AddMapObject(halMpOb);
    }

    private void BuildChoiceTree()
    {
      BuildHal();
    }

    private void BuildHal()
    {
      //choices for josh

      if (submarine?.MissionDescription == null) throw new Exception("Submarine Mission Description empty");

      //option 1
      //option 2
      var talkOption = (
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


      var choices = new DialogOption[] {
        talkOption, // second option so nr 1 
      };

      hal.TreeOfChoices = new ChoiceBranch(1, "Hi I'm hal", choices);
    }
  }
}
