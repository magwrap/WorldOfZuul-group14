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
      LoadingAnimation.Loading("Mission Loading"); 
      GameConsole.Clear();

      GameConsole.WriteLine(LongDescription, font: FontTheme.HighligtedText);
      GameConsole.WriteLine(MessageOnArrival, font: FontTheme.Success);

      ship = new MissionRoom("Battle ship", "As you take in the surroundings, the ship's impressive features underscore the severity of the threat. The urgency in the man's voice resonates with the high-tech equipment and the dedicated crew bustling around, preparing for the mission ahead. The fate of these endangered creatures hangs in the balance, and the responsibility to protect them weighs heavily on your shoulders.", "", "", new Map(4, 6));

      poachersShip = new MissionRoom("Poachers capitan cabin", "Normal ship cabin... Nothing special", "", "", new Map(3, 9));


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

        else if (currentRoom == ship && !ship.RoomMap.mapEntities.IsAnyQuestAvailable())
        {
          currentRoom = poachersShip;
          BuildPoachersShip();
          Actions.ShowMap(ref currentRoom, ref previousRoom);
        }


        if (currentRoom == poachersShip && !poachersShip.RoomMap.mapEntities.IsAnyQuestAvailable())
        {
          Thread.Sleep(4000);
          GameConsole.Clear();
          GameConsole.WriteLine("Congratulations, you finished the mission!", font: FontTheme.Success);
          Thread.Sleep(3000);
          GameConsole.Clear();
          continuePlaying = false;
          Hub.isPacificCompleted = true;
          return;
        }

        currentRoom?.DisplayShortDescription();

        Command? command = Game.AskForCommand();
        continuePlaying = Actions.DecideAction(ref command, ref currentRoom, ref previousRoom, true, "pacific");
      }
    }

    private void BuildPoachersShip()
    {
      poachersShip?.RoomMap.SetXandY(3, 2);
      NPC capitan = new NPC("Capitan of poachers ship")
      {
        TreeOfChoices = new ChoiceBranch(1, $"Ah, the famous {Game.PlayerName} graces me with their presence at last. I've heard tales of your exploits.", new DialogOption[] {
          ("I'm here to put an end to your poaching operations.", new ChoiceBranch(1, "End? Ha! I've amassed riches beyond your wildest dreams, all while the world turns a blind eye to their dying oceans. What do you think you can change?", new DialogOption[] {
            ("It's not just about wealth. It's about preserving life, protecting our oceans, and ensuring a future for all creatures, not just for profit.", new ChoiceBranch(1, "Ah, noble ideals, but the world cares little for such sentiments. Fame, fortune—that's what truly matters. No one will miss a few fish or whales.", new DialogOption[] {
              ("Every life matters, even if you can't see beyond your greed.", new ChoiceBranch(1, "Enough talk! I'll show you what really matters!", new DialogOption[] {
                ("Shoot him", new ChoiceBranch(1, "Once the captain is shoot, his crew surrenders without a fight, recognizing the loss of their leader. \nTheir resignation becomes evident in their demeanor as they lower their weapons and offer no resistance, \nacknowledging the futility of further confrontation without their captain's command.\nAmidst this unexpected surrender, your crew maintains their vigilance, ensuring a peaceful transition. \nThe subdued atmosphere echoes the unspoken understanding among the captured crew, accepting the change in leadership.\nWith the situation under control and the capitan now dead, you guide your crew back to the ship, the tension easing as the captured crew members seem willing to cooperate. \nThe return journey to headquarters remains cautious, a silent testament to the success of your mission and the crew's adeptness in handling a delicate situation with minimal disruption.", isItGoodEnding: true, repGain: -2)),
                ("Disarm and arrest him", new ChoiceBranch(2, "Following the confrontation and the neutralization of the captain, his crew surrenders without a fight, recognizing  cowardness of their leader. \nYou exit the room to witness your crew looking up at you, their faces lit with relief and jubilation. \nThe victory against the poaching threat resonates through the ship as your crew celebrates, acknowledging your pivotal role in securing it.\nWith a nod of satisfaction, you make your way back to your ship, each step affirming the success achieved. \nThe engines hum to life, and the vessel sets its course back to headquarters, propelled by the shared triumph over the ruthless exploitation of marine life. \nThe weight of the accomplishment drives you forward, steering the ship towards new challenges, and the ongoing mission to safeguard the world's oceans.", isItGoodEnding: true, repGain: 11))
              }))
            }))
          }))
        })
      };

      Quest stopPoachers = new Quest("Stop the poachers!", "");
      MapObject exit = new MapObject(1, 2, MapObjectsEnum.PLACE, occupiedMessage: "You can't leave now, you have to talk to the capitan first");
      MapObject capitanOb = new MapObject(7, 2, MapObjectsEnum.ENEMY, quest: stopPoachers, npc: capitan);

      poachersShip?.RoomMap.mapEntities.PopulateMap(new MapObject[] {
        capitanOb,
        exit
      });
    }

    private void BuildDecision()
    {
      isDecisionBuilt = true;

      GameConsole.WriteLine("The choice before you poses a strategic dilemma: launching an attack now, under the unforgiving daylight, risks alerting the enemy. \nHowever, it significantly minimizes the likelihood of their escape. \nThe islands act as a natural barrier, obscuring both your ship from their view and their presence from your sight.\nOn the other hand, waiting for the cover of night offers the possibility of a stealthier approach, capitalizing on the element of surprise. \nBut it comes with a palpable risk—the darkness might provide them with the opportunity to slip away under the shroud of night, evading your grasp.\n\n...\n\nArriving at the designated coordinates, the sun sits high in the sky, casting its luminous glow across the vast expanse of water. \nHowever, despite the clear visibility brought by the daylight, your target remains hidden,\nshielded by a series of islands standing sentinel between your vessel and their location.", fgColor: ConsoleColor.Blue);

      string[] decideText = new string[] { "Decide", "As you grab your silenced handgun and a heartbeat sensor synced with your crew, the decision hangs in the balance." };

      Quest decide = new Quest(decideText[0], decideText[1]);
      DialogOption cancel = new DialogOption("Cancel", new ChoiceBranch(2, "Cancelling..."));

      ChoiceBranch attack = new ChoiceBranch(1, "Do you want to attack now? ", new DialogOption[] {
          ("Yes", new ChoiceBranch(1, "STARTING THE ATTACK!", new DialogOption[] {
            ("GO!", new ChoiceBranch(1, "The moment you signal for action, the crew mobilizes with practiced efficiency. \nCommands echo across the deck as the ship's engines thrum to life, propelling you forward toward the concealed threat.\nYour decision to strike now is met with swift and coordinated manoeuvres. \nThe vessel navigates the maze of islands, inching closer to the elusive target. \nA sense of urgency permeates the air as the crew readies the ship's defences and arms themselves for the impending confrontation.\nAs your crew launches torpedoes to prevent the target's escape, they deftly maneuver the ship closer. \nFending off any resistance, your team expertly handles adversaries, swiftly silencing those who dare oppose. \nPositioned strategically, you leap onto the enemy vessel, leading your boarders with determined resolve.", isItGoodEnding: true, repGain: 5))
            })),
          cancel
          });

      ChoiceBranch wait = new ChoiceBranch(2, "Do you want to wait? ", new DialogOption[] {
          ("Yes", new ChoiceBranch(1, "Waiting for the nightfall...", new DialogOption[] {
            ("Nightfall: let's start the mission!",new ChoiceBranch(1, "Under the cover of the stealthy approach, your vessel glides closer to the brink of visibility, cautiously skirting the edge of being detected. \nWith calculated precision, the ship reaches a point where any further approach risks exposure.\nYou and a select group from your crew transition to a smaller, more discreet boat, designed for silent manoeuvring. \nCarefully, you navigate the small craft towards the target, edging along the ship's shadowed periphery to evade notice.\nOnce close enough, your team deftly climbs the anchor, utilizing the cover of darkness and the ship's structure to conceal your approach. \nSilently scaling the vessel, your crew gains entry, each step calculated to avoid any undue attention.", isItGoodEnding: true,repGain: 9))
          })),
          cancel
        });

      NPC decision = new NPC("Decide")
      {
        TreeOfChoices = new ChoiceBranch(1, "Decide what you want to do: ", new DialogOption[] {
        ("attack", attack),
        ("wait", wait)
        })
      };

      MapObject decideOb = new MapObject(3, 1, MapObjectsEnum.PLACE, true, false, npc: decision, quest: decide);

      ship?.RoomMap.mapEntities.PopulateMap(
        new MapObject[]
        {
          decideOb,
        }
      );
    }

    private void BuildShip()
    {
      ship?.RoomMap.SetXandY(3, 1);
      NPC man = new NPC("Man")
      {
        TreeOfChoices = new ChoiceBranch(1, "Sir,' he starts, his tone urgent, 'our contacts have sent word—the head of the most notorious \nwhale and shark poaching organization has been sighted near the Cook Islands. \nTheir operations have caused catastrophic damage to the populations in these waters.'", new DialogOption[] {
        ("...", new ChoiceBranch(1, "The man gestures towards the horizon, emphasizing the gravity of the situation. \nThe ship itself stands as a testament to modern engineering—a sleek, advanced vessel built for speed and stealth. \nIts streamlined design hints at its purpose: swift and covert movements to intercept those harming the marine life.", isItGoodEnding: true))
      })
      };

      MapObject manOb = new MapObject(3, 4, MapObjectsEnum.NPC, isRemovable: true, npc: man, quest: talkToMan);

      ship?.RoomMap.mapEntities.AddMapObject(manOb);
    }
  }
}