using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldOfZuul
{
  public class PacificRoom : Room
  {
    private bool continuePlaying = true;
    public PacificRoom(
      string? shortDesc,
      string? longDesc
    // string? msgOnArrival
    ) : base(shortDesc, longDesc)
    {
    }
    public void StartPacificMission(ref Room? currentRoom, ref Room? previousRoom)
    {

      GameConsole.WriteLine(LongDescription, font: FontTheme.HighligtedText);


      Messages.PrintMissionHelp();
      previousRoom = null;

      Actions.ShowMap(ref currentRoom, ref previousRoom);

      while (continuePlaying)
      {


        if (RoomMap.mapEntities.IsAnyQuestAvailable())
        {
          Thread.Sleep(4000);
          GameConsole.Clear();
          GameConsole.WriteLine("Congratulations, you finished the mission!", font: FontTheme.Success);
          Thread.Sleep(3000);
          GameConsole.Clear();
          Hub.isAfricaCompleted = true;
          continuePlaying = false;
          Hub.isAfricaCompleted = true;
          return;
        }

        currentRoom?.DisplayShortDescription();

        Command? command = Game.AskForCommand();
        continuePlaying = Actions.DecideAction(ref command, ref currentRoom, ref previousRoom, true, "pacific");
      }
    }
  }
}