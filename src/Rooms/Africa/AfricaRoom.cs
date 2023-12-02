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
    readonly NPC josh = new("josh");


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
      MissionGameRooms africaRooms = JsonFileReader.GetAfricaRooms();
      GameConsole.WriteLine(LongDescription, font: FontTheme.HighligtedText);

      if (africaRooms == null || africaRooms.Rooms == null)
      {
        throw new Exception("No africa rooms");
      };

      submarine = new(
        africaRooms.Rooms[(int)AfricaRoomsEnum.SUBMARINE].ShortDesc,
        africaRooms.Rooms[(int)AfricaRoomsEnum.SUBMARINE].LongDesc,
        africaRooms.Rooms[(int)AfricaRoomsEnum.SUBMARINE].MissionDescription,
        africaRooms.Rooms[(int)AfricaRoomsEnum.SUBMARINE].MessageOnArrival
        );

      camp = new(
        africaRooms.Rooms[(int)AfricaRoomsEnum.CAMP].ShortDesc,
        africaRooms.Rooms[(int)AfricaRoomsEnum.CAMP].LongDesc,
        africaRooms.Rooms[(int)AfricaRoomsEnum.CAMP].MissionDescription,
        africaRooms.Rooms[(int)AfricaRoomsEnum.CAMP].MessageOnArrival
      );
      //TODO: add river to thh map

      BuildChoiceTree();

      submarine.SetExit("camp", camp);
      submarine.DisplayMissionDesc();
      previousRoom = null;
      currentRoom = submarine;

      while (continuePlaying)
      {
        Command? command = Game.AskForCommand();
        continuePlaying = Actions.DecideAction(ref command, ref currentRoom, ref previousRoom, true, "africa");
        bool? joshGoodEnding = josh.TreeOfChoices?.StartDialog();

        if (joshGoodEnding != null && joshGoodEnding == true)
        {
          GameConsole.WriteLine("Good ending!!");
        }
      }
    }

    private void BuildChoiceTree()
    {

      //choices for josh

      //option 1
      var fightOption = ("Kick him", new ChoiceBranch(1, "Auch that wasn't nice"));


      //option 2
      var talkOption = (
        "Talk with him", new ChoiceBranch(2, "What do you want to talk about?",
            new DialogOption[] {
              ("Weather", new ChoiceBranch(1, "the weather is very pretty today")),
              ("Africa", new ChoiceBranch(2, "hmm I think that it is very empty at the moment",
                  new DialogOption[] {
                    ("Argue", new ChoiceBranch(1, "alright alright it's beautiful")),
                    ("Agree", new ChoiceBranch(2, "But don't worry you still have plenty of time left : )", isItGoodEnding: true))
                  }
                )
              )
            }
        )
      );


      var choices = new DialogOption[] {
        fightOption, // first option so nr 1
        talkOption, // second option so nr 2
      };

      josh.TreeOfChoices = new ChoiceBranch(1, "Hello My name is Josh", choices);
    }
  }
}
