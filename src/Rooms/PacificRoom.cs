using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldOfZuul
{
  public class PacificRoom : Room
  {
    private MissionRoom? ship;
    private MissionRoom? poachersShip;
    private bool continuePlaying = true;
    readonly string MessageOnArrival = "";
    readonly static string[] decideText = new string[] { "Decide", "As you grab your silenced handgun and a heartbeat sensor synced with your crew, that allows you to between the vital signs of allies and foes the decision hangs in the balance.\n Left: attack\t Right: wait" };
    readonly Quest decideAttack = new Quest(decideText[0], decideText[1]);
    readonly Quest decideWait = new Quest(decideText[0], decideText[1]);
    readonly Quest talkToMan = new Quest("Talk to the man", "Start a coversation with man about details of your mission");
    private bool isDecisionBuilt = false;
    public PacificRoom(
      string? shortDesc,
      string? longDesc,
    string? msgOnArrival
    ) : base(shortDesc, longDesc)
    {
      MessageOnArrival = msgOnArrival ?? "";

    }
    public void StartPacificMission(ref Room? currentRoom, ref Room? previousRoom)
    {

      GameConsole.WriteLine(LongDescription, font: FontTheme.HighligtedText);
      GameConsole.WriteLine(MessageOnArrival, font: FontTheme.Success);

      ship = new MissionRoom("Battle ship", "As you take in the surroundings, the ship's impressive features underscore the severity of the threat. The urgency in the man's voice resonates with the high-tech equipment and the dedicated crew bustling around, preparing for the mission ahead. The fate of these endangered creatures hangs in the balance, and the responsibility to protect them weighs heavily on your shoulders.", "", "", new Map(4, 6));

      poachersShip = new MissionRoom("Poachers Ship", "", "", "", new Map(15, 20));


      Messages.PrintMissionHelp();
      previousRoom = null;
      currentRoom = ship;
      BuildShip();
      Actions.ShowMap(ref currentRoom, ref previousRoom);

      while (continuePlaying)
      {

        if (currentRoom == ship && talkToMan.IsCompleted && !isDecisionBuilt)
        {
          BuildDecision();
          Actions.ShowMap(ref currentRoom, ref previousRoom);
        }

        else if (currentRoom == ship && (decideAttack.IsCompleted || decideWait.IsCompleted))
        {
          currentRoom = poachersShip;
          BuildPoachersShip();
          Actions.ShowMap(ref currentRoom, ref previousRoom);
        }


        // if (!RoomMap.mapEntities.IsAnyQuestAvailable())
        // {
        //   Thread.Sleep(4000);
        //   GameConsole.Clear();
        //   GameConsole.WriteLine("Congratulations, you finished the mission!", font: FontTheme.Success);
        //   Thread.Sleep(3000);
        //   GameConsole.Clear();
        //   Hub.isAfricaCompleted = true;
        //   continuePlaying = false;
        //   Hub.isAfricaCompleted = true;
        //   return;
        // }

        currentRoom?.DisplayShortDescription();

        Command? command = Game.AskForCommand();
        continuePlaying = Actions.DecideAction(ref command, ref currentRoom, ref previousRoom, true, "pacific");
      }
    }

    private void BuildAttack()
    {
      ship?.RoomMap.mapEntities.RemoveMapObject(5, 3);

    }

    private void BuildWait()
    {

      ship?.RoomMap.mapEntities.RemoveMapObject(1, 3);
    }

    private void BuildPoachersShip()
    {

      if (decideAttack.IsCompleted)
      {
        BuildAttack();
      }

      else if (decideWait.IsCompleted)
      {
        BuildWait();
      }
    }
    private void BuildDecision()
    {
      isDecisionBuilt = true;
      GameConsole.WriteLine("The choice before you poses a strategic dilemma: launching an attack now, under the unforgiving daylight, risks alerting the enemy. However, it significantly minimizes the likelihood of their escape. The islands act as a natural barrier, obscuring both your ship from their view and their presence from your sight.\nOn the other hand, waiting for the cover of night offers the possibility of a stealthier approach, capitalizing on the element of surprise. But it comes with a palpable risk—the darkness might provide them with the opportunity to slip away under the shroud of night, evading your grasp.\n\n...\n\nArriving at the designated coordinates, the sun sits high in the sky, casting its luminous glow across the vast expanse of water. However, despite the clear visibility brought by the daylight, your target remains hidden, shielded by a series of islands standing sentinel between your vessel and their location.", fgColor: ConsoleColor.Blue);

      DialogOption cancel = new DialogOption("Cancel", new ChoiceBranch(2, "Cancelling..."));
      NPC attackNow = new NPC("attack now")
      {
        TreeOfChoices = new ChoiceBranch(1, "Do you want to attack now? ", new DialogOption[] {
          ("Yes", new ChoiceBranch(1, "STARTING THE ATTACK!", isItGoodEnding: true)),
          cancel
          })
      };
      NPC wait = new NPC("Wait for the nightfall")
      {
        TreeOfChoices = new ChoiceBranch(1, "Do you want to wait? ", new DialogOption[] {
          ("Yes", new ChoiceBranch(1, "Waiting for the nightfall...", isItGoodEnding: true)),
          cancel
          })
      };

      MapObject attackNowOb = new MapObject(1, 3, MapObjectsEnum.PLACE, true, false, npc: attackNow, quest: decideAttack);

      MapObject waitOb = new MapObject(5, 3, MapObjectsEnum.PLACE, true, false, npc: wait, quest: decideWait);

      ship?.RoomMap.mapEntities.PopulateMap(
        new MapObject[]
        {
          attackNowOb,
          waitOb
        }
      );
    }

    private void BuildShip()
    {
      ship?.RoomMap.SetXandY(3, 1);
      NPC man = new NPC("Man")
      {
        TreeOfChoices = new ChoiceBranch(1, "Sir,' he starts, his tone urgent, 'our contacts have sent word—the head of the most notorious whale and shark poaching organization has been sighted near the Cook Islands. Their operations have caused catastrophic damage to the populations in these waters.'", new DialogOption[] {
        ("...", new ChoiceBranch(1, "The man gestures towards the horizon, emphasizing the gravity of the situation. The ship itself stands as a testament to modern engineering—a sleek, advanced vessel built for speed and stealth. Its streamlined design hints at its purpose: swift and covert movements to intercept those harming the marine life.", isItGoodEnding: true))
      })
      };

      MapObject manOb = new MapObject(3, 2, MapObjectsEnum.NPC, isRemovable: true, npc: man, quest: talkToMan);

      ship?.RoomMap.mapEntities.AddMapObject(manOb);
    }
  }
}