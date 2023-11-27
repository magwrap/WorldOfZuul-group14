using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldOfZuul.Africa
{
  public class AfricaRoom : Room
  {
    /// <summary>
    /// Space for africa and its inner rooms
    /// </summary>

    private MissionRoom? submarine;
    private MissionRoom? camp;
    private ChoiceTree choiceTree = new();
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
      //TODO: add river to thh ma


      choiceTree.AddMultipleBranches(new ChoiceBranch[] {
          new("1", "option 1"),
          new("2", "option 2"),
          new("3", "option 3")
      });

      string dialogOption1 = GameConsole.Input("What dialog option do you choose?");

      Console.WriteLine($"{choiceTree.Branches[dialogOption1]}");
      // choiceTree.Branches["1"].Branches["1"]

      submarine.SetExit("camp", camp);
      submarine.DisplayMissionDesc();
      previousRoom = null;
      currentRoom = submarine;

      while (continuePlaying)
      {
        Command? command = Game.AskForCommand();
        continuePlaying = Actions.DecideAction(ref command, ref currentRoom, ref previousRoom, true, "africa");
      }
    }
  }
}