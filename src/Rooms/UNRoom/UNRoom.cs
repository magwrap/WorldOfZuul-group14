using WorldOfZuul.src;

namespace WorldOfZuul
{
  class UNRoom
  {
    private static Dictionary<int, int> AssembyReputationGain = new Dictionary<int, int>
    {
      [1] = 0,
      [2] = 5,
      [3] = 10
    };
    public static void StartLastMission()
    {
      GameConsole.Clear();
      GameConsole.WriteLine("Congratulations! You have successfully completed all three missions, let's make your work count!", font: FontTheme.Success);
      Thread.Sleep(5000);
      GameConsole.Clear();

      if (Reputation.ReputationScore < 50) //number of points that determines the scenario for the player 
        ReputationScore1();
      else if (Reputation.ReputationScore < 125)
        ReputationScore2();
      else if (Reputation.ReputationScore > 125)
        ReputationScore3();
      FinalReputation();
    }
    //optimized PrntDeaker color dialogue
    static void PrntD(string speaker, ConsoleColor speakerColor, string dialogue)
    {
      Console.ForegroundColor = speakerColor;
      Console.Write($"\n{speaker}");
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine($": {dialogue}");
    }

    //1 star scenario
    private static void ReputationScore1()
    {
      Console.WriteLine(
          "\nAs the ship docks, a sinking feeling hits. You anticipate a lukewarm reception and potential disappointment from peers.\n" +
          "Inside headquarters, the atmosphere feels tepid, with hesitant nods and weary glances reflecting disappointment.\n" +
          "Grumbles and murmurs linger, indicating discontentment. The reception lacks usual camaraderie as your assistant approaches reservedly.");

      PrntD("Diana", ConsoleColor.Green, "Welcome back. The council is expecting your presence in the meeting room.");

      // Dialogue options
      bool exitDialogue1 = false;
      bool optionChosen1 = false;

      while (!exitDialogue1)
      {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("\n1.");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("Ask about mission feedback");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("\n2.");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("Express readiness for the meeting");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("\n3.");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("Inquire about the atmosphere");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("\n4.");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("Leave conversation");

        System.Console.Write("\n\U000027A4 ");
        string dialogueInput1 = Console.ReadLine() ?? "\n";

        if (!int.TryParse(dialogueInput1, out int dialogueOption1))
        {
          GameConsole.Clear();
          GameConsole.WriteLine("Invalid input, please enter number of a valid option.", font: FontTheme.Danger);
          // Handle invalid input scenario here, possibly continue the loop or take appropriate action
        }
        else
        {
          GameConsole.Clear();
          switch (dialogueOption1)
          {
            case 1:
              PrntD("Ranger", ConsoleColor.Red, "Any initial reports or feedback from the missions?");
              PrntD("Diana", ConsoleColor.Green, "The reports indicate some setbacks.\n" +
              "It seems there were concerns raised by our contacts regarding your team's actions during the missions.");
              PrntD("Ranger", ConsoleColor.Red, "Understood.");
              optionChosen1 = true;
              break;
            case 2:
              PrntD("Ranger", ConsoleColor.Red, "I'm prepared to brief the council. Are they looking to meet immediately?");
              PrntD("Diana", ConsoleColor.Green, "Yes, they've been expecting your return. They're ready whenever you're prepared to present your findings.");
              PrntD("Ranger", ConsoleColor.Red, "Okay, I'll head there right away to give them an update.");
              optionChosen1 = true;
              break;
            case 3:
              PrntD("Ranger", ConsoleColor.Red, "How's the morale around here?");
              PrntD("Diana", ConsoleColor.Green, "Morale's a bit subdued, given the recent challenges. The council is interested in understanding the field dynamics and challenges faced.");
              PrntD("Ranger", ConsoleColor.Red, "I'll make sure to address those concerns in the meeting.");
              optionChosen1 = true;
              break;
            case 4:
              if (optionChosen1)
              {
                PrntD("Ranger", ConsoleColor.Red, "I'll head to the council now.");
                PrntD("Diana nods politely", ConsoleColor.Green, "Of course. The council awaits you.");
                exitDialogue1 = true;
              }
              else
              {
                PrntD("Diana watches the ranger depart with a visibly concerned expression, then hurriedly adds", ConsoleColor.Green, "They're awaiting your presence in the council room. It's crucial that you join them promptly.");
                exitDialogue1 = true;
              }
              break;
            default:
              Console.WriteLine("Invalid choice.");
              return;
          }
        }
      }

      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("\n1.");
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine("Take a look through the window of the reception hall");
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("\n2.");
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine("Go to the meeting room");

      System.Console.Write("\n\U000027A4 ");
      string meetInput1 = Console.ReadLine() ?? "\n";

      if (!int.TryParse(meetInput1, out int meetOption1))
      {
        GameConsole.Clear();
        GameConsole.WriteLine("Invalid input, please enter number of a valid option.", font: FontTheme.Danger);
      }
      else
      {
        GameConsole.Clear();
        switch (meetOption1)
        {
          case 1:
            Console.WriteLine("The Atlantic view fails to captivate; wildlife ignores offerings, echoing the headquarters' tepid atmosphere.\n" +
            "\nYou make your way to the meeting room, but the journey feels lackluster. The corridors seem unusually desolate, reflecting the subdued atmosphere.\n" +
            "The anticipation for the meeting is tempered by a sense of disappointment in your recent performance.");

            break;
          case 2:
            Console.WriteLine("You make your way to the meeting room, but the journey feels lackluster. The corridors seem unusually desolate, reflecting the subdued atmosphere.\n" +
            "The anticipation for the meeting is tempered by a sense of disappointment in your recent performance.");
            break;
          default:
            Console.WriteLine("Invalid choice.");
            return;
        }
      }
      Console.WriteLine("\nEntering the meeting room, you feel a palpable sense of disappointment in the air. The 13 council members' expressions reflect a blend of concern and reservation.\n" +
      "As you take your seat, the atmosphere feels strained, indicating a dissatisfaction with recent outcomes.");

      PrntD("Council Member 1", ConsoleColor.Green, "The suggestion to send our headmaster ranger to the UN Assembly... do we need to reassess that decision?");
      PrntD("Council Member 2", ConsoleColor.Green, "Agreed. The recent setbacks are concerning. Can we risk having our representation compromised?");
      PrntD("Council Member 3", ConsoleColor.Green, "But let's not disregard their immense dedication and invaluable experience. Despite recent challenges, their expertise remains unmatched.");
      PrntD("Council Member 4", ConsoleColor.Green, "Their years of service shouldn't be dismissed due to a few missteps. We need someone who comprehends the complexities of our mission.");
      PrntD("Council Member 5", ConsoleColor.Green, "Precisely. Their presence holds significant weight, and we must consider the continuity of our efforts.");
      PrntD("Council Member 6", ConsoleColor.Green, "disagree. Recent shortcomings could damage our reputation. Shouldn't we consider an alternative?");
      PrntD("Council Member 7", ConsoleColor.Green, "Yes, it's a risk. We must ensure our representation is at its best.");
      PrntD("Council Member 8", ConsoleColor.Green, "I side with Council Member 3. Their dedication to our cause outweighs these recent events.");
      PrntD("Council Member 9", ConsoleColor.Green, "I concur. Their expertise is crucial, especially given the urgency of the global stage.");
      PrntD("Council Member 1", ConsoleColor.Green, "Despite differing opinions, it seems the majority leans towards reaffirming our choice. Their experience and understanding of our mission are unparalleled.");
      PrntD("Council Member 10", ConsoleColor.Green, "I agree. With proper support and guidance, they can effectively advocate for us.");
      PrntD("Council Member 11", ConsoleColor.Green, "It's a tough decision, but their long-standing commitment shouldn't be disregarded.");
      PrntD("Council Member 12", ConsoleColor.Green, "I too support the decision. They're the face of our fight against poaching.");

      Console.WriteLine("\nCouncil members debate the headmaster ranger's UN representation, amid concerns about recent setbacks.\n" +
      "Some push for reconsideration, emphasizing flawless representation, while others stress the ranger's dedication and expertise.\n" +
      "Despite setbacks, they emphasize the ranger's invaluable role in fighting poaching.");

      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("\n1.");
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine("Show gratitude");
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("\n2.");
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine("Express determination");
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("\n3.");
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine("Acknowlegde concerns");
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("\n4.");
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine("Manifest doubt");

      System.Console.Write("\n\U000027A4 ");
      string councilInput1 = Console.ReadLine() ?? "\n";

      if (!int.TryParse(councilInput1, out int councilOption1))
      {
        GameConsole.Clear();
        GameConsole.WriteLine("Invalid input, please enter number of a valid option.", font: FontTheme.Danger);
      }
      else
      {
        GameConsole.Clear();
        switch (councilOption1)
        {
          case 1:
            PrntD("Ranger", ConsoleColor.Red, "I'm honored by the council's trust. I assure you, I'll dedicate myself to represent our cause diligently and uphold the integrity of our mission.");
            PrntD("Council Member 1", ConsoleColor.Green, "Your dedication and commitment are commendable. We appreciate your assurance.\n" +
            "We trust that you'll effectively represent our cause with the same passion and determination you've always shown. Your efforts are crucial to our mission's success.");
            break;
          case 2:
            PrntD("Ranger", ConsoleColor.Red, "I hear the concerns, but I'm determined to showcase our efforts on this global stage. Together, we'll make a compelling case for our cause.");
            PrntD("Council Member 1", ConsoleColor.Green, "Your dedication and commitment are commendable. We appreciate your assurance.\n" +
            "We trust that you'll effectively represent our cause with the same passion and determination you've always shown. Your efforts are crucial to our mission's success.");
            break;
          case 3:
            PrntD("Ranger", ConsoleColor.Red, "I understand the reservations. I'm committed to addressing recent setbacks and ensuring that our message resonates effectively at the assembly.");
            PrntD("Council Member 1", ConsoleColor.Green, "We understand your apprehension, but don't doubt your capability. Your experience and dedication speak volumes.\n" +
            "We acknowledge recent setbacks, but we believe in your ability to rise above them. You've been instrumental in our cause, and we stand behind your capability to represent us effectively.");
            break;
          case 4:
            PrntD("Ranger", ConsoleColor.Red, "I appreciate the confidence, but recent events have made me question if I'm the best choice for this critical task. I'm worried my recent performance might hinder our cause.");
            PrntD("Council Member 1", ConsoleColor.Green, "We understand your apprehension, but don't doubt your capability. Your experience and dedication speak volumes.\n" +
            "We acknowledge recent setbacks, but we believe in your ability to rise above them. You've been instrumental in our cause, and we stand behind your capability to represent us effectively.");
            break;
          default:
            Console.WriteLine("Invalid choice.");
            return;
        }
      }
    }


    //2 stars scenario
    private static void ReputationScore2()
    {
      Console.WriteLine(
           "\nAs the ship glides into the docks, you expect the usual acknowledgment, \nbut uncertainty lingers about whether your performance will meet the anticipated standards.\n" +
           "A mix of confidence and subtle nervousness tingles at the edge of your thoughts.\n" +
           "Upon your return to headquarters, the atmosphere carries an air of anticipation. \n" +
           "Fellow rangers and employees nod in acknowledgment, a mix of reserved smiles and neutral expressions greeting your arrival. \n" +
           "There's a sense of usual routine in the air, suggesting an expected outcome from your missions.\n" +
           "As you make your way through the corridors, your assistant approaches you smiling, maintaining a professional bearing."
       );
      PrntD("Diana", ConsoleColor.Green, "Good to have you back. The Council is awaiting you in the meeting room.");

      // Dialogue options
      bool exitDialogue2 = false;
      bool optionChosen2 = false;

      while (!exitDialogue2)
      {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("\n1.");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("Ask about mission feedback");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("\n2.");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("Express readiness for the meeting");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("\n3.");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("Inquire about the atmosphere");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("\n4.");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("Leave conversation");

        System.Console.Write("\n\U000027A4 ");
        string dialogueInput2 = Console.ReadLine() ?? "\n";

        if (!int.TryParse(dialogueInput2, out int dialogueOption2))
        {
          GameConsole.Clear();
          GameConsole.WriteLine("Invalid input, please enter number of a valid option.", font: FontTheme.Danger);
        }
        else
        {
          GameConsole.Clear();
          switch (dialogueOption2)
          {
            case 1:
              PrntD("Ranger", ConsoleColor.Red, "Any initial reports or feedback from the missions?");
              PrntD("Diana", ConsoleColor.Green, "Initial feedback suggests a positive reception from the missions. You delivered as expected.");
              PrntD("Ranger", ConsoleColor.Red, "Understood. Good to hear.");
              optionChosen2 = true;
              break;
            case 2:
              PrntD("Ranger", ConsoleColor.Red, "I'm prepared to brief the council. Are they looking to meet immediately?");
              PrntD("Diana", ConsoleColor.Green, "Absolutely, they've been awaiting your report. They're ready whenever you're set to be present.");
              PrntD("Ranger", ConsoleColor.Red, "Good, I'll head over to update them promptly.");
              optionChosen2 = true;
              break;
            case 3:
              PrntD("Ranger", ConsoleColor.Red, "How's the morale around here?");
              PrntD("Diana", ConsoleColor.Green, "Morale remains steady, as usual. I've observed multiple discussions circulating about your missions, consistently highlighting your commendable performance, as expected.");
              PrntD("Ranger", ConsoleColor.Red, "Thank you. It's encouraging to know that my efforts are being noticed and appreciated by everyone.");
              optionChosen2 = true;
              break;
            case 4:
              if (optionChosen2)
              {
                PrntD("Ranger", ConsoleColor.Red, "I'll head to the council now.");
                PrntD("Diana nods politely and offers a small smile.", ConsoleColor.Green, "Certainly. They're eager to hear from you.");
                exitDialogue2 = true;
              }
              else
              {
                PrntD("Diana notices the player's departure, a faint concern evident in her expression, sounding slightly disappointed.", ConsoleColor.Green, "\nWhen you're prepared, please head to the council. Your insights matter to them... and to me.");
                exitDialogue2 = true;
              }
              break;
            default:
              Console.WriteLine("Invalid choice.");
              return;
          }
        }
      }

      GameConsole.Clear();

      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("\n1.");
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine("Take a look through the window of the reception hall");
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("\n2.");
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine("Go to the meeting room");

      System.Console.Write("\n\U000027A4 ");
      string meetInput2 = Console.ReadLine() ?? "\n";

      if (!int.TryParse(meetInput2, out int meetOption2))
      {
        GameConsole.Clear();
        GameConsole.WriteLine("Invalid input, please enter number of a valid option.", font: FontTheme.Danger);
      }
      else
      {
        GameConsole.Clear();
        switch (meetOption2)
        {
          case 1:
            Console.WriteLine("Looking through the window, you observe the familiar expanse of the Atlantic, appreciating the serene beauty of the ocean.\n" +
            "Some wildlife tentatively approaches the dispensed food provided by the automatic feeder, though it lacks the usual spectacle.\n" +
            "\nHeading to the meeting room, you traverse the corridors that exude a sense of routine.\n" +
            "Colleagues acknowledge your presence with reserved smiles, indicating a familiarity that lacks enthusiasm.");
            break;
          case 2:
            Console.WriteLine("Heading to the meeting room, you traverse the corridors that exude a sense of routine.\n" +
            "Colleagues acknowledge your presence with reserved smiles, indicating a familiarity that lacks enthusiasm.");
            break;
          default:
            Console.WriteLine("Invalid choice.");
            return;
        }
      }

      GameConsole.Clear();

      Console.WriteLine("\nStepping into the meeting room, the 13 council members acknowledge your arrival with composed nods, maintaining a professional demeanour.\n" +
      "Taking your seat, the atmosphere remains steady, portraying a sense of predictability without much fervor.");

      PrntD("Council Member 1", ConsoleColor.Green, "Considering our headmaster ranger for the UN Assembly... any need for reevaluation?");
      PrntD("Council Member 2", ConsoleColor.Green, "Valid point. Recent hurdles might affect our representation.");
      PrntD("Council Member 3", ConsoleColor.Green, "But their dedication and experience shouldn't be overlooked due to recent challenges.");
      PrntD("Council Member 4", ConsoleColor.Green, "Their extensive service is invaluable. We need someone who truly grasps our mission's complexities.");
      PrntD("Council Member 5", ConsoleColor.Green, "Their presence carries substantial weight for our ongoing efforts.");
      PrntD("Council Member 6", ConsoleColor.Green, "Differing perspective here. Recent issues could harm our reputation. Should we explore other options?");
      PrntD("Council Member 7", ConsoleColor.Green, "True, it's a risk. Our representation has to be flawless.");
      PrntD("Council Member 8", ConsoleColor.Green, "Agreeing with Council Member 3. Dedication outweighs recent events.");
      PrntD("Council Member 9", ConsoleColor.Green, "Their expertise is vital, especially considering the global urgency.");
      PrntD("Council Member 1", ConsoleColor.Green, "Despite differing opinions, it seems the majority is inclined to support our decision. Their understanding and experience are commendable.");
      PrntD("Council Member 10", ConsoleColor.Green, "I concur. With adequate backing, they can advocate effectively.");
      PrntD("Council Member 11", ConsoleColor.Green, "It's a challenging situation, but their commitment speaks volumes.");
      PrntD("Council Member 12", ConsoleColor.Green, "I too back the decision. They epitomize our anti-poaching mission.");

      Console.WriteLine("\nIn the midst of diverging views, the council deliberates on the headmaster ranger's potential representation at the United Nations General Assembly.\n" +
      "While some push for reassessment for flawless representation, the majority stresses the ranger's dedication and experience.\n" +
      "Ultimately, despite acknowledging recent challenges, the majority leans towards reaffirming their decision.");

      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("\n1.");
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine("Show gratitude");
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("\n2.");
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine("Express determination");
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("\n3.");
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine("Concern about perfection");

      System.Console.Write("\n\U000027A4 ");
      string councilInput2 = Console.ReadLine() ?? "\n";

      if (!int.TryParse(councilInput2, out int councilOption2))
      {
        GameConsole.Clear();
        GameConsole.WriteLine("Invalid input, please enter number of a valid option.", font: FontTheme.Danger);
      }
      else
      {
        GameConsole.Clear();
        switch (councilOption2)
        {
          case 1:
            PrntD("Ranger", ConsoleColor.Red, "I'm honored by the council's trust. I assure you, I'll dedicate myself to represent our cause diligently and uphold the integrity of our mission.");
            PrntD("Council Member 1", ConsoleColor.Green, "Your dedication and commitment are commendable. We appreciate your assurance.\n" +
            "We trust that you'll effectively represent our cause with the same passion and determination you've always shown. Your efforts are crucial to our mission's success.");
            break;
          case 2:
            PrntD("Ranger", ConsoleColor.Red, "I hear the concerns, but I'm determined to showcase our efforts on this global stage. Together, we'll make a compelling case for our cause.");
            PrntD("Council Member 1", ConsoleColor.Green, "Your dedication and commitment are commendable. We appreciate your assurance.\n" +
            "We trust that you'll effectively represent our cause with the same passion and determination you've always shown. Your efforts are crucial to our mission's success.");
            break;
          case 3:
            PrntD("Ranger", ConsoleColor.Red, "I acknowledge the emphasis on flawless representation. My concern is whether my recent contributions suffice for this level of responsibility.\n" +
            "I'm worried that despite my efforts, it might not be enough to convey our cause effectively on a global platform.");
            PrntD("Council Member 1", ConsoleColor.Green, "We understand your worries. Your recent efforts have been commendable. We recognize the challenges but have faith in your ability to bridge any gaps.\n" +
            "Your dedication is commendable, and we're confident in your capacity to represent us effectively.");
            break;
          default:
            Console.WriteLine("Invalid choice.");
            return;
        }
      }
    }


    //3 stars scenario
    private static void ReputationScore3()
    {
      Console.WriteLine("\nThe ship's arrival fills you with an electric buzz of anticipation. You sense an impending celebration, perhaps even a hero's welcome.\n" +
      "There's a bubbling excitement for the reception awaiting your extraordinary accomplishments. You stride into the headquarters and an electric energy crackles through the air.\n" +
      "Cheers erupt, accompanied by enthusiastic clapping and hearty pats on the back from fellow rangers and staff.");
      PrntD("Diana", ConsoleColor.Green, "Fantastic job out there! The council is eager to hear from you. They asked you to come to the meeting room");

      // Dialogue options
      bool exitDialogue3 = false;
      bool optionChosen3 = false;

      while (!exitDialogue3)
      {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("\n1.");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("Ask about mission feedback");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("\n2.");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("Express readiness for the meeting");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("\n3.");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("Inquire about the atmosphere");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("\n4.");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("Leave conversation");

        System.Console.Write("\n\U000027A4 ");
        string dialogueInput3 = Console.ReadLine() ?? "\n";

        if (!int.TryParse(dialogueInput3, out int dialogueOption3))
        {
          GameConsole.Clear();
          GameConsole.WriteLine("Invalid input, please enter number of a valid option.", font: FontTheme.Danger);
        }
        else
        {
          GameConsole.Clear();
          switch (dialogueOption3)
          {
            case 1:
              PrntD("Ranger", ConsoleColor.Red, "Any initial reports or feedback from the missions?");
              PrntD("Diana", ConsoleColor.Green, "Feedback's overwhelmingly positive! Your successes have been well-noted.");
              PrntD("Ranger", ConsoleColor.Red, "Glad to hear that.");
              optionChosen3 = true;
              break;
            case 2:
              PrntD("Ranger", ConsoleColor.Red, "I'm prepared to brief the council. Are they looking to meet immediately?");
              PrntD("Diana", ConsoleColor.Green, "Absolutely! They're eager to hear about the outstanding achievements.");
              PrntD("Ranger", ConsoleColor.Red, "I'll be sure to provide as much detail as I can.");
              optionChosen3 = true;
              break;
            case 3:
              PrntD("Ranger", ConsoleColor.Red, "How's the morale around here?");
              PrntD("Diana", ConsoleColor.Green, "Morale's sky-high after your exceptional missions! The council's keen on replicating your success strategies.");
              PrntD("Ranger", ConsoleColor.Red, "Amazing! I'm thrilled to have such a positive impact on the team. I'll guarantee to highlight those strategies in the meeting.");
              optionChosen3 = true;
              break;
            case 4:
              if (optionChosen3)
              {
                PrntD("Ranger", ConsoleColor.Red, "I'll head to the council now.");
                PrntD("Diana nods politely and offers a small smile.", ConsoleColor.Green, "Certainly. They're eager to hear from you.");
                exitDialogue3 = true;
              }
              else
              {
                PrntD("Diana notices the player's departure, a faint concern evident in her expression, sounding slightly disappointed.", ConsoleColor.Green, "\nYour presence is expected in the council room. They're counting on your exceptional input... I was looking forward to hearing your thoughts too.");
                exitDialogue3 = true;
              }
              break;
            default:
              Console.WriteLine("Invalid choice.");
              return;
          }
        }
      }

      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("\n1.");
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine("Take a look through the window of the reception hall");
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("\n2.");
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine("Go to the meeting room");

      System.Console.Write("\n\U000027A4 ");
      string meetInput3 = Console.ReadLine() ?? "\n";

      if (!int.TryParse(meetInput3, out int meetOption3))
      {
        GameConsole.Clear();
        GameConsole.WriteLine("Invalid input, please enter number of a valid option.", font: FontTheme.Danger);
      }
      else
      {
        GameConsole.Clear();
        switch (meetOption3)
        {
          case 1:
            Console.WriteLine("Through the window, the majesty of the Atlantic unfolds before you.\n" +
            "Whales breach, dolphins playfully leap, and various birds create a mesmerizing display.\n" +
            "The spectacle outside mirrors the sense of triumph and grandeur you feel within the headquarters, leaving you in awe of the extraordinary sight.\n" +
            "As you stride toward the meeting room, the corridors come alive with bustling activity. Colleagues offer enthusiastic nods and cheerful greetings, buoyed by the celebratory atmosphere.\n" +
            "Your exceptional performance reflects in the animated interactions, setting a triumphant tone for the meeting ahead. The anticipation builds, resonating with the exhilaration of your recent achievements.");
            break;
          case 2:
            Console.WriteLine("As you stride toward the meeting room, the corridors come alive with bustling activity. Colleagues offer enthusiastic nods and cheerful greetings, buoyed by the celebratory atmosphere.\n" +
            "Your exceptional performance reflects in the animated interactions, setting a triumphant tone for the meeting ahead. The anticipation builds, resonating with the exhilaration of your recent achievements.");
            break;
          default:
            Console.WriteLine("Invalid choice.");
            return;
        }
      }
      Console.WriteLine("\nWalking into the meeting room, a wave of admiration and anticipation greets you.\n" +
      "The 13 council members offer enthusiastic greetings, their expressions reflecting admiration and respect for your recent accomplishments.\n" +
      "Taking the central seat, the atmosphere buzzes with excitement, setting the stage for a meeting charged with positive energy.");

      PrntD("Council Member 1", ConsoleColor.Green, "Considering our headmaster ranger for the UN Assembly... their recent performance speaks volumes, don't you agree?");
      PrntD("Council Member 2", ConsoleColor.Green, "Absolutely. Their track record is exceptional.");
      PrntD("Council Member 3", ConsoleColor.Green, "Their dedication and expertise are unparalleled. I don't see any reason for reconsideration.");
      PrntD("Council Member 4", ConsoleColor.Green, "Agreed. Their experience makes them the ideal choice.");
      PrntD("Council Member 5", ConsoleColor.Green, "Their presence would undoubtedly make a substantial impact.");
      PrntD("Council Member 6", ConsoleColor.Green, "No doubts here. Their recent accomplishments overshadow any concerns.");
      PrntD("Council Member 7", ConsoleColor.Green, "I fully support their representation. It's an opportunity we can't overlook.");
      PrntD("Council Member 8", ConsoleColor.Green, "Their contributions to our cause are undeniable.");
      PrntD("Council Member 9", ConsoleColor.Green, "Their expertise is vital, especially given the global urgency.");
      PrntD("Council Member 1", ConsoleColor.Green, "It's unanimous then. Their exceptional dedication and recent performance make them the clear choice.");

      Console.WriteLine("\nYour exemplary performance has left no room for diverging views or concerns.\n" +
      "The council unanimously agrees that your recent accomplishments and track record make you the ideal representative for the upcoming United Nations General Assembly.\n" +
      "The decision is swift and unchallenged, resonating with absolute confidence in your capabilities and contributions.");

      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("\n1.");
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine("Show humility");
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("\n2.");
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine("Express confidence");

      System.Console.Write("\n\U000027A4 ");
      string councilInput3 = Console.ReadLine() ?? "\n";

      if (!int.TryParse(councilInput3, out int councilOption3))
      {
        GameConsole.Clear();
        GameConsole.WriteLine("Invalid input, please enter number of a valid option.", font: FontTheme.Danger);
      }
      else
      {
        GameConsole.Clear();
        switch (councilOption3)
        {
          case 1:
            PrntD("Ranger", ConsoleColor.Red, "I'm honored by the council's trust. However, I must admit, I'm concerned about the high expectations.\n" +
            "I'll strive to meet them, but I hope my recent success won't overshadow our team's collective efforts.");
            PrntD("Council Member 1", ConsoleColor.Green, "Your humility is admirable, Ranger. Your concern for the team's efforts reflects your dedication.\n" +
            "Remember, your success is our collective achievement. We trust you'll represent us humbly and honorably.");
            break;
          case 2:
            PrntD("Ranger", ConsoleColor.Red, "I appreciate the confidence placed in me. I'm fully committed to representing our cause.\n" +
            "My recent achievements only fuel my determination to advocate for our mission on a global platform.");
            PrntD("Council Member 1", ConsoleColor.Green, "Confidence suits you well, Ranger. Your determination inspires us. We believe your recent success amplifies our cause.\n" +
            "Your commitment assures us that our mission will resonate globally.");
            break;
          default:
            Console.WriteLine("Invalid choice.");
            return;
        }
      }
    }
    public static void FinalReputation()
    {
      Console.WriteLine("\nYou step back into the Ranger's Reception Hall, a hive of activity in preparation for the upcoming UN General Assembly.\n" +
      "Diana is focused, typing away at her computer, while your team stands poised at the entrance, ready and awaiting instructions.\n" +
      "The air hums with purpose and anticipation, each member playing a vital role in the final arrangements for the significant event ahead.");

      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("\n1.");
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine("Approach Diana");
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("\n2.");
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine("Go to your team and depart for the Assembly");

      System.Console.Write("\n\U000027A4 ");
      string leaveInput = Console.ReadLine() ?? "\n";

      if (!int.TryParse(leaveInput, out int leaveOption))
      {
        GameConsole.Clear();
        GameConsole.WriteLine("Invalid input, please enter number of a valid option.", font: FontTheme.Danger);
      }
      else
      {
        GameConsole.Clear();
        switch (leaveOption)
        {
          case 1:
            PrntD("Diana", ConsoleColor.Green, "Everything in order for the assembly?");
            PrntD("Ranger", ConsoleColor.Red, "Yes, we're ready. Just tying up loose ends.");
            PrntD("Diana", ConsoleColor.Green, "Good to hear. Your presence there will make a difference.");
            PrntD("Ranger", ConsoleColor.Red, "I hope so. We're aiming for impact.");
            PrntD("Diana", ConsoleColor.Green, "have no doubt you'll represent us well.");
            PrntD("Ranger", ConsoleColor.Red, "Thank you. Your support means a lot.");
            PrntD("Diana", ConsoleColor.Green, "Safe travels and make us proud.");
            PrntD("Ranger", ConsoleColor.Red, "Will do. See you when we return.");
            Console.WriteLine("\nYou stride purposefully toward your team, exchanging nods of acknowledgment as you convey swift directives for the imminent departure.\n" +
            "With a few crisp instructions and words of encouragement, you set the course for action,\n" +
            "leaving the Reception Hall in determined strides, each step echoing readiness and commitment to the forthcoming assembly.");
            break;
          case 2:
            Console.WriteLine("\nYou stride purposefully toward your team, exchanging nods of acknowledgment as you convey swift directives for the imminent departure.\n" +
            "With a few crisp instructions and words of encouragement, you set the course for action,\n" +
            "leaving the Reception Hall in determined strides, each step echoing readiness and commitment to the forthcoming assembly.");
            break;
          default:
            Console.WriteLine("Invalid choice.");
            return;
        }
      }

      Console.WriteLine("\nStepping into the grand hall of the United Nations assembly, you're met with a sight that embodies global significance.\n" +
      "\nPeople from diverse cultures and backgrounds move about, each person seemingly carrying their own slice of the world's concerns.\n" +
      "The grandeur of the surroundings seems to instil a sense of humility, a recognition of the immense responsibility that comes with addressing global issues.\n" +
      "\nYou notice clusters of individuals engaged in intense conversations, some nodding with understanding, others debating animatedly.\n" +
      "\nYou are given way to the podium and prepare to present your point. Some questions related to the topic of poaching and SDGs 14 and 15 were prepared in advance of your arrival.");

      PrntD("Inquirer", ConsoleColor.Green, "What methods might address the complexities of poaching effectively?");

      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("\n1.");
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine("Increasing penalties for convicted poachers");
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("\n2.");
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine("Encouraging community involvement without sustainable alternatives");
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("\n3.");
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine("Creating alternative livelihoods for local communities coupled with strict law enforcement");

      System.Console.Write("\n\U000027A4 ");
      string firstQuestion = Console.ReadLine() ?? "\n";

      if (!int.TryParse(firstQuestion, out int firstOption))
      {
        GameConsole.Clear();
        GameConsole.WriteLine("Invalid input, please enter number of a valid option.", font: FontTheme.Danger);
      }
      else
      {
        DecideReputationGain(firstOption);
      }
      PrntD("Inquirer", ConsoleColor.Green, "What strategies are critical for bolstering terrestrial biodiversity under SDG 15?");

      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("\n1.");
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine("Focusing solely on preserving charismatic species");
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("\n2.");
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine("Relying on isolated conservation efforts without local involvement");
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("\n3.");
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine("Employing ecosystem-based approaches and engaging local communities in sustainable land management practices");

      System.Console.Write("\n\U000027A4 ");
      string secondQuestion = Console.ReadLine() ?? "\n";

      if (!int.TryParse(secondQuestion, out int secondOption))
      {
        GameConsole.Clear();
        GameConsole.WriteLine("Invalid input, please enter number of a valid option.", font: FontTheme.Danger);
      }
      else
      {

        DecideReputationGain(secondOption);

      }
      PrntD("Inquirer", ConsoleColor.Green, "Which initiatives might effectively curb illegal wildlife trade and poaching?");

      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("\n1.");
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine("Concentrating solely on law enforcement measures");
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("\n2.");
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine("Adopting international bans on wildlife products");
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("\n3.");
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine("Combining robust law enforcement with community engagement and addressing underlying socioeconomic factors");

      System.Console.Write("\n\U000027A4 ");
      string thirdQuestion = Console.ReadLine() ?? "\n";

      if (!int.TryParse(thirdQuestion, out int thirdOption))
      {
        GameConsole.Clear();
        GameConsole.WriteLine("Invalid input, please enter number of a valid option.", font: FontTheme.Danger);
      }
      else
      {
        DecideReputationGain(thirdOption);
      }
      PrntD("Inquirer", ConsoleColor.Green, "Which approach would effectively combat poaching while aligning with SDG 15 goals?");

      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("\n1.");
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine("Conducting occasional anti-poaching patrols with minimal community engagement");
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("\n2.");
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine("Implementing punitive measures alone without community involvement");
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("\n3.");
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine("Employing community-based conservation strategies addressing poaching drivers, involving local communities and fostering biodiversity protection");

      System.Console.Write("\n\U000027A4 ");
      string fourthQuestion = Console.ReadLine() ?? "\n";

      if (!int.TryParse(fourthQuestion, out int fourthOption))
      {
        GameConsole.Clear();
        GameConsole.WriteLine("Invalid input, please enter number of a valid option.", font: FontTheme.Danger);
      }
      else
      {
        DecideReputationGain(fourthOption);

      }
      PrntD("Inquirer", ConsoleColor.Green, "Which initiatives might effectively curb illegal wildlife trade and poaching?");

      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("\n1.");
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine("Concentrating solely on law enforcement measures");
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("\n2.");
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine("Adopting international bans on wildlife products");
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("\n3.");
      Console.ForegroundColor = ConsoleColor.Gray;
      Console.WriteLine("Combining robust law enforcement with community engagement and addressing underlying socioeconomic factors");

      System.Console.Write("\n\U000027A4 ");
      string fifthQuestion = Console.ReadLine() ?? "\n";

      if (!int.TryParse(fifthQuestion, out int fifthOption))
      {
        GameConsole.Clear();
        GameConsole.WriteLine("Invalid input, please enter number of a valid option.", font: FontTheme.Danger);
      }
      else
      {
        DecideReputationGain(fifthOption);
      }
      if (Reputation.ReputationScore < 60)
      {
        Console.WriteLine("\nAs the assembly concludes, your absence from critical discussions about poaching and the environmental concerns in the ocean is palpable.\n" +
        "While your presence was acknowledged, your contributions remained negligible.\n" +
        "The global efforts for Sustainable Development Goals 14 and 15 feel only partially reinforced, lacking the essential vigor needed to make a profound difference.\n" +
        "Your role, though acknowledged, did not significantly impact the resolutions made during the assembly.\n" +
        "But only time will tell if it had any impact in the world.");
      }
      else if (Reputation.ReputationScore >= 60 && Reputation.ReputationScore < 125)
      {
        Console.WriteLine("\nYour presence at the assembly was acknowledged, and your contributions to the discussions about poaching and the conservation efforts were recognized.\n" +
        "While your efforts were appreciated, their impact remained limited.\n" +
        "The resolutions adopted during the assembly reflect incremental progress rather than substantial change.\n" +
        "Your actions contributed, but the global efforts toward SDGs 14 and 15 could have been more profound with more substantial engagement from all stakeholders.\n" +
        "\nBut only time will tell if it had any impact in the world.");
      }
      else
      {
        Console.WriteLine("\nYour active engagement and influential contributions during the assembly discussions regarding poaching and ocean conservation had a monumental impact.\n" +
        "Your compelling arguments and innovative proposals steered the discussions towards more comprehensive resolutions.\n" +
        "The assembly adopted pioneering strategies, leveraging your insights, for the successful implementation of Sustainable Development Goals 14 and 15.\n" +
        "Your actions significantly accelerated the global efforts, setting a new standard for environmental conservation at a global scale.\n" +
        "\nBut only time will tell if it had any impact in the world.");
      }
    }

    private static void DecideReputationGain(int option)
    {
      GameConsole.Clear();
      switch (option)
      {
        case 1:
        case 2:
        case 3:
          Reputation.ReputationScore += AssembyReputationGain[option];
          break;
        default:
          Console.WriteLine("Invalid choice.");
          return;
      }
    }

  }


}