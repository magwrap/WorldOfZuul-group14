using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    private MissionRoom? jungle;
    //SUBMARINE NPC
    readonly NPC hal = new("HAL");

    //CAMP NPS
    //npc1
    private readonly NPC Kenny = new("Kenny");
    //npc2
    private readonly NPC Josh = new("Josh");
    //npc3
    private readonly NPC Manadrine = new("Mandarine");
    //giraffe
    private readonly NPC giraffe = new("Elizabeth");
    private readonly Enemy poachers = new("Gang of poachers");
    readonly Quest saveTheGiraffe = new Quest("Save the giraffe!", "Elizabeth the giraffe is stuck between the roots of a tree. She can't get out! You need to help her.");

    private bool fireStarted = false;
    MissionGameRooms? JsonAfricaRooms = null;
    bool continuePlaying = true;

    readonly string MessageOnArrival = "";

    public AfricaRoom(
      string? shortDesc,
      string? longDesc,
      string? msgOnArrival
    ) : base(shortDesc, longDesc)
    {
      MessageOnArrival = msgOnArrival ?? "";
    }

    public void StartAfricaMission(ref Room? currentRoom, ref Room? previousRoom)
    {
      JsonAfricaRooms = JsonFileReader.GetAfricaRooms();

      GameConsole.WriteLine(LongDescription, font: FontTheme.HighligtedText);
      GameConsole.WriteLine(MessageOnArrival, font: FontTheme.Success);

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

      jungle = new(
        JsonAfricaRooms.Rooms[(int)AfricaRoomsEnum.JUNGLE].ShortDesc,
        JsonAfricaRooms.Rooms[(int)AfricaRoomsEnum.JUNGLE].LongDesc,
        JsonAfricaRooms.Rooms[(int)AfricaRoomsEnum.JUNGLE].MissionDescription,
        JsonAfricaRooms.Rooms[(int)AfricaRoomsEnum.JUNGLE].MessageOnArrival,
        new Map(height: 20)

      );

      Messages.PrintMissionHelp();
      previousRoom = null;

      currentRoom = submarine;
      InSubmarine();

      Actions.ShowMap(ref currentRoom, ref previousRoom);

      while (continuePlaying)
      {
        if (currentRoom == submarine && !submarine.RoomMap.mapEntities.IsAnyQuestAvailable())
        {
          currentRoom = camp;
          InCamp();
          Actions.ShowMap(ref currentRoom, ref previousRoom);
        }

        else if (currentRoom == camp && !camp.RoomMap.mapEntities.IsAnyQuestAvailable())
        {
          currentRoom = jungle;
          InJungle();
          Actions.ShowMap(ref currentRoom, ref previousRoom);
        }

        else if (currentRoom == jungle && saveTheGiraffe.IsCompleted && !fireStarted)
        {
          //giraffe coords
          Messages.PrintFaceOfGiraffe();
          fireStarted = true;
          BuildPoachers();
          BuildJungleExit();
          Actions.ShowMap(ref currentRoom, ref previousRoom);
        }

        else if (currentRoom == jungle && !jungle.RoomMap.mapEntities.IsAnyQuestAvailable())
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
        continuePlaying = Actions.DecideAction(ref command, ref currentRoom, ref previousRoom, true, "africa");
      }
    }

    private void InSubmarine()
    {
      submarine?.DisplayMessageOnArrival();
      BuildHal();
      //Submarine quest + hals
      Quest startMission = new("Enbark on a mission!", "Talk to hal in order to start the mission");
      MapObject halMpOb = new(3, 6, MapObjectsEnum.NPC, false, true, "Hal!", startMission, npc: hal);
      submarine?.RoomMap.mapEntities.AddMapObject(halMpOb);
    }

    private void InCamp()
    {
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
    private void InJungle()
    {
      jungle?.DisplayMessageOnArrival();
      jungle?.RoomMap.SetXandY(15, 2);
      BuildJungleTrees();
      BuildGiraffes();
      MapObject giraffeOb = new MapObject(25, 17, MapObjectsEnum.GIRAFFE, true, npc: giraffe, quest: saveTheGiraffe);
      MapObject exitOb = new MapObject(15, 1, MapObjectsEnum.PLACE);

      jungle?.RoomMap.mapEntities.AddMapObject(exitOb);
      jungle?.RoomMap.mapEntities.AddMapObject(giraffeOb);
    }
    private void BuildJungleExit()
    {
      jungle?.RoomMap.mapEntities.RemoveMapObject(15, 1);
      Quest escapeTheFire = new Quest("Escape from the fire!", "The wildfire kept spreading while you were busy helping the giraffe. Now you have to run!");

      MapObject exitOb = new MapObject(15, 1, MapObjectsEnum.PLACE, true, quest: escapeTheFire);

      jungle?.RoomMap.mapEntities.AddMapObject(exitOb);
    }
    private void BuildPoachers()
    {
      DialogOption sheWentEastOption =
                ("she went east", new ChoiceBranch(1, "Well I guess, we will have to believe you. Off you go, we have no more use of you.", isItGoodEnding: true, repGain: 11));

      DialogOption sheWentEastBadOption =
                ("she went east...", new ChoiceBranch(1, "Well I guess, we will have to believe you. Off you go, we have no more use of you.", isItGoodEnding: true, repGain: -8));

      DialogOption fightOptionPoachers = (
        "Fight them", new ChoiceBranch(2, "You hit first guy on the left and he drops down like a brick, unconscious. Other two poachers start walking towards you, while their boss kept looking from the distance.", new DialogOption[] {
          ("Attack the boss", new ChoiceBranch(1, "You plunged towards their cheif, and nearly reached him, but then felt a strong hand grabing you by the collar. Other par of hands grabbed you tight around your arms so that you couldn't move by an inch.\n Now you will tell us where did she go.", new DialogOption[] { sheWentEastBadOption })),
          ("Fight the two men", new ChoiceBranch(2, "You grab` the guy on the right by his hand arm, pull it as hard as you can and he falls like a wooden toy. His companion looked at now two bodies laying underneath you, spat and said 'I ain't getting beaten over some giraffe', and run. As fast as he could. You looked at their boss, he shrugged his shoulders, turned his back and also run.", isItGoodEnding: true, repGain: 4))
        })
      );

      DialogOption talkOptionPoaches =
          ("Maybe we can try to talk it out?", new ChoiceBranch(1, "What do you want to talk about?! We need HER, and that's it.", new DialogOption[] {
            ("Why do you do this?", new ChoiceBranch(1, "Haa, if you want to persuade us, and tell us how we can try other, less harmful ways you're wrong. We hate every single thing that walks on this planet. Yes we hunt those animals, the money is good. But we're not desperate, we just don't care.", new DialogOption[] {
              ("Is there really nothing I can't do?", new ChoiceBranch(1, "We can leave you here die, The fire we've planted, had already scared enough of animals out, that after we hunt them, for us to life in luxury for the next few years.", new DialogOption[] {
                sheWentEastOption,
                ("she went north", new ChoiceBranch(2, "DON'T LIE TO US.\nWe came from north, and noone spotted her. Where. Did. She. GO?!" ,
                new DialogOption[] { sheWentEastOption})),
                ("she went south (lead them into the fire)", new ChoiceBranch(3, "You want us killed then huh? WE set the fire, we know from which direction it's coming. That is what you get out of noble animal saving 'heros' like you.", new DialogOption[] {
                  sheWentEastBadOption
                } ))
              }))
            })),
            fightOptionPoachers,
          }));

      DialogOption playDumbOptionPoachers = (
        "What giraffe?", new ChoiceBranch(3, "Do you take us as a band of fools? We know you helped her! The perfect opportunity wasted just because someone wanted to play a hero! Now WHERE DID SHE GO", new DialogOption[] {
          talkOptionPoaches,
          fightOptionPoachers
        })
      );


      var poachersChoices = new DialogOption[] {
        talkOptionPoaches,
        fightOptionPoachers,
        playDumbOptionPoachers
      };

      poachers.TreeOfChoices = new ChoiceBranch(1, "Tell us where did she go or we won't let you pass!", poachersChoices);
      Quest fightPoachers = new Quest("Get past the poachers.", "Turning back into the direction you came from, you can see the group of shabby looking men.\nThat can only mean one thing... Poachers.\nThere is no other way. You'll have to face them.");
      MapObject poachersOb = new MapObject(11, 8, MapObjectsEnum.ENEMY, true, quest: fightPoachers, npc: poachers);
      jungle?.RoomMap.mapEntities.AddMapObject(poachersOb);
    }

    private void BuildGiraffes()
    {
      DialogOption talkOptionGiraffe = (
        "Try to pull her leg", new ChoiceBranch(1, "She screams, tries to jump, but her leg is still in the same place. She gets more tense with fire coming closer and closer still.",
        new DialogOption[] {
          ("Try to calm her", new ChoiceBranch(1,"As you speak softly, you can feel her leg getting more relaxed.", new DialogOption[] {
            ("Say her name('elizabeth'), don't worry I'll save you", new ChoiceBranch(1, "Now she can trust you completely, you know her name. Only people who know her name want good for you.", new DialogOption[] {
              ("Pull her leg out", new ChoiceBranch(1, "She let's you gently put her leg out from between roots. She is greatful", new DialogOption[] {
                ("Run away girl!", new ChoiceBranch(1, "She runs west as fast as she can", isItGoodEnding: true, repGain: 11)),
                ("wait here", new ChoiceBranch(2, "She waits for 5 seconds but then sees flames coming towards her. She gets scared and runs west", isItGoodEnding: true, repGain:8))
              }))
            })),
            ("Say her name('Caroline'), don't worry I'll save you", new ChoiceBranch(2, "When she heard you saying name of her biggest enemy Caroline. Elizabeth lost all her trust and stopped cooparating with you."))
          }))
        })
      );

      DialogOption forceOptionGiraffe = (
        "Push her as hard as you can", new ChoiceBranch(2, "Her leg gets free but it's badly damaged", isItGoodEnding: true, repGain: -2)
      );
      DialogOption quitOptionGiraffe = (
             "leave her", new ChoiceBranch(3, "")
      );
      var giraffeChoices = new DialogOption[] {
        talkOptionGiraffe,
        forceOptionGiraffe,
        quitOptionGiraffe
      };

      giraffe.TreeOfChoices = new ChoiceBranch(1, "You can see giraffes leg entangeled in one of the roots of a tree, she is paralized with fear", giraffeChoices);
    }

    private void BuildJungleTrees()
    {
      jungle?.RoomMap.mapEntities.PopulateMap(
        new MapObject[]{
          new(1, 7, MapObjectsEnum.TREE),
          new(1, 5, MapObjectsEnum.TREE),
          new(3, 8, MapObjectsEnum.TREE),
          new(3, 7, MapObjectsEnum.TREE),
          new(3, 18, MapObjectsEnum.TREE),
          new(4, 8, MapObjectsEnum.TREE),
          new(5, 6, MapObjectsEnum.TREE),
          new(5, 9, MapObjectsEnum.TREE),
          new(5, 4, MapObjectsEnum.TREE),
          new(7, 8, MapObjectsEnum.TREE),
          new(9, 7, MapObjectsEnum.TREE),
          new(9, 6, MapObjectsEnum.TREE),
          new(11, 17, MapObjectsEnum.TREE),
          new(11, 4, MapObjectsEnum.TREE),
          new(11, 10, MapObjectsEnum.TREE),
          new(11, 3, MapObjectsEnum.TREE),
          new(13, 9, MapObjectsEnum.TREE),
          new(13, 7, MapObjectsEnum.TREE),
          new(15, 8, MapObjectsEnum.TREE),
          new(15, 7, MapObjectsEnum.TREE),
          new(17, 6, MapObjectsEnum.TREE),
          new(17, 5, MapObjectsEnum.TREE),
          new(18, 6, MapObjectsEnum.TREE),
          new(18, 11, MapObjectsEnum.TREE),
          new(19, 8, MapObjectsEnum.TREE),
          new(20, 7, MapObjectsEnum.TREE),
          new(21, 4, MapObjectsEnum.TREE),
          new(21, 13, MapObjectsEnum.TREE),
          new(22, 6, MapObjectsEnum.TREE),
          new(23, 12, MapObjectsEnum.TREE),
          new(24, 10, MapObjectsEnum.TREE),
          new(25, 9, MapObjectsEnum.TREE),
          new(26, 7, MapObjectsEnum.TREE),
          new(27, 6, MapObjectsEnum.TREE),
          new(28, 8, MapObjectsEnum.TREE),
          new(29, 6, MapObjectsEnum.TREE),
          new(30, 2, MapObjectsEnum.TREE),


          new(1, 2, MapObjectsEnum.TREE),
          new(1, 15, MapObjectsEnum.TREE),
          new(1, 9, MapObjectsEnum.TREE),
          new(2, 3, MapObjectsEnum.TREE),
          new(3, 16, MapObjectsEnum.TREE),
          new(3, 17, MapObjectsEnum.TREE),
          new(3, 11, MapObjectsEnum.TREE),
          new(5, 20, MapObjectsEnum.TREE),
          new(5, 15, MapObjectsEnum.TREE),
          new(5, 9, MapObjectsEnum.TREE),
          new(6, 15, MapObjectsEnum.TREE),
          new(7, 14, MapObjectsEnum.TREE),
          new(7, 17, MapObjectsEnum.TREE),
          new(9, 19, MapObjectsEnum.TREE),
          new(9, 16, MapObjectsEnum.TREE),
          new(11, 13, MapObjectsEnum.TREE),
          new(11, 17, MapObjectsEnum.TREE),
          new(11, 10, MapObjectsEnum.TREE),
          new(11, 16, MapObjectsEnum.TREE),
          new(13, 10, MapObjectsEnum.TREE),
          new(13, 17, MapObjectsEnum.TREE),
          new(15, 16, MapObjectsEnum.TREE),
          new(15, 20, MapObjectsEnum.TREE),
          new(15, 4, MapObjectsEnum.TREE),
          new(17, 11, MapObjectsEnum.TREE),
          new(17, 9, MapObjectsEnum.TREE),
          new(18, 13, MapObjectsEnum.TREE),
          new(18, 11, MapObjectsEnum.TREE),
          new(19, 9, MapObjectsEnum.TREE),
          new(19, 3,MapObjectsEnum.TREE),
          new(20, 14, MapObjectsEnum.TREE),
          new(21, 20, MapObjectsEnum.TREE),
          new(21, 10, MapObjectsEnum.TREE),
          new(22, 20, MapObjectsEnum.TREE),
          new(23, 12, MapObjectsEnum.TREE),
          new(23, 3, MapObjectsEnum.TREE),
          new(24, 8, MapObjectsEnum.TREE),
          new(25, 13, MapObjectsEnum.TREE),
          new(26, 10, MapObjectsEnum.TREE),
          new(27, 11, MapObjectsEnum.TREE),
          new(27, 4, MapObjectsEnum.TREE),
          new(28, 19, MapObjectsEnum.TREE),
          new(28, 2, MapObjectsEnum.TREE),
          new(29, 12, MapObjectsEnum.TREE),
          new(29, 1, MapObjectsEnum.TREE),
          new(30, 20, MapObjectsEnum.TREE),
          new(30, 5, MapObjectsEnum.TREE),
    }
    );
    }

    private void BuildCampNpcs()
    {
      BuildKenny();
      BuildJosh();
      BuildMandarine();
    }
    public void BuildRiver()
    {
      camp?.RoomMap.mapEntities.PopulateMap(
              new MapObject[]{
                new(1, 11, MapObjectsEnum.DIAGONALWALL_RIGHT),
                new(3, 10, MapObjectsEnum.VERTICALWALL),
                new(3, 9, MapObjectsEnum.DIAGONALWALL_RIGHT),
                new(5, 8, MapObjectsEnum.VERTICALWALL),
                new(3, 7, MapObjectsEnum.DIAGONALWALL_LEFT),
                new(3, 6, MapObjectsEnum.VERTICALWALL),
                new(3, 5, MapObjectsEnum.DIAGONALWALL_RIGHT),
                new(5, 4, MapObjectsEnum.VERTICALWALL),
                new(5, 3, MapObjectsEnum.DIAGONALWALL_RIGHT),
                new(7, 2, MapObjectsEnum.VERTICALWALL),
                new(5, 1, MapObjectsEnum.DIAGONALWALL_LEFT),

                new(11, 1, MapObjectsEnum.DIAGONALWALL_LEFT),
                new(13, 2, MapObjectsEnum.VERTICALWALL),
                new(11, 3, MapObjectsEnum.DIAGONALWALL_RIGHT),
                new(11, 4, MapObjectsEnum.VERTICALWALL),
                new(9, 5, MapObjectsEnum.DIAGONALWALL_RIGHT),
                new(9, 6, MapObjectsEnum.VERTICALWALL),
                new(9, 7, MapObjectsEnum.DIAGONALWALL_LEFT),
                new(11, 8, MapObjectsEnum.VERTICALWALL),
                new(9, 9, MapObjectsEnum.DIAGONALWALL_RIGHT),
                new(9, 10, MapObjectsEnum.VERTICALWALL),
                new(7, 11, MapObjectsEnum.DIAGONALWALL_RIGHT)
      });
    }
    public void BuildCampHut()
    {
      camp?.RoomMap.mapEntities.PopulateMap(
              new MapObject[]{
                new(29, 3, MapObjectsEnum.VERTICALWALL),
                new(29, 4, MapObjectsEnum.VERTICALWALL),
                new(29, 5, MapObjectsEnum.VERTICALWALL),
                new(29, 7, MapObjectsEnum.VERTICALWALL),
                new(29, 8, MapObjectsEnum.VERTICALWALL),
                new(29, 9, MapObjectsEnum.VERTICALWALL),

                new(29, 2, MapObjectsEnum.HORIZONTALWALL),
                new(30, 2, MapObjectsEnum.HORIZONTALWALL),
                new(31, 2, MapObjectsEnum.HORIZONTALWALL),
                new(32, 2, MapObjectsEnum.HORIZONTALWALL),
                new(33, 2, MapObjectsEnum.HORIZONTALWALL),
                new(34, 2, MapObjectsEnum.HORIZONTALWALL),
                new(35, 2, MapObjectsEnum.HORIZONTALWALL),
                new(36, 2, MapObjectsEnum.HORIZONTALWALL),
                new(37, 2, MapObjectsEnum.HORIZONTALWALL),
                new(38, 2, MapObjectsEnum.HORIZONTALWALL),
                new(39, 2, MapObjectsEnum.HORIZONTALWALL),

                new(29, 9, MapObjectsEnum.HORIZONTALWALL),
                new(30, 9, MapObjectsEnum.HORIZONTALWALL),
                new(31, 9, MapObjectsEnum.HORIZONTALWALL),
                new(32, 9, MapObjectsEnum.HORIZONTALWALL),
                new(33, 9, MapObjectsEnum.HORIZONTALWALL),
                new(34, 9, MapObjectsEnum.HORIZONTALWALL),
                new(35, 9, MapObjectsEnum.HORIZONTALWALL),
                new(36, 9, MapObjectsEnum.HORIZONTALWALL),
                new(37, 9, MapObjectsEnum.HORIZONTALWALL),
                new(38, 9, MapObjectsEnum.HORIZONTALWALL),
                new(39, 9, MapObjectsEnum.HORIZONTALWALL),
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

      DialogOption chitchatOptionJosh = ("What's your favourite kind of ice cream?", new ChoiceBranch(2, "I love chocolate ones. At least I used to now it looks like mud to me. You know what? I'll give you advise, for beign nice enough to talk to me. REMEBER: never trust anyone in the jungle. You can't even imagine howλ sly poachers can be! Anyway thank you for the effort, you're very kind.", isItGoodEnding: true));

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
        "Can we start the mission?", new ChoiceBranch(1,
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
