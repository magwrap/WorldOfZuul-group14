using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldOfZuul.src.reputation;

namespace WorldOfZuul.src
{
  public static class Reputation
  {
    public static int ReputationScore { get; set; }

    public static int NumberOfMissionsFinished { get; set; }


    public static void Initialize()
    {
      ReputationScore = 50;
      NumberOfMissionsFinished = 0;
    }
    public static void MadeGoodDecision()
    {
      ReputationScore += 6;
    }

    public static void MadeBadDecision()
    {
      ReputationScore -= 5;
    }

    /// <summary>
    /// Function envoked when player does something - deceeases reputation
    /// </summary>
    public static void AnnoyedSomeone()
    {
      ReputationScore -= 1;
    }

    /// <summary>
    /// Function envoked when player does a good deed - addes reputation
    /// </summary>
    public static void MadeSomeoneSmile()
    {
      ReputationScore += 2;
    }

    /// <summary>
    /// function envoked when mission is completed - adds reputation and increases missions completed counter
    /// </summary>
    public static void MissionFailed()
    {
      ReputationScore -= 10;
    }

    /// <summary>
    /// function envoked when mission is completed - adds reputation and increases missions completed counter
    /// </summary>
    public static void MissionCompleted(int reputationGain = 11)
    {
      // ReputationScore += reputationGain;
      NumberOfMissionsFinished++;
    }

    public static void DialogCompleted(int reputationGain = 2)
    {
      ReputationScore += reputationGain;
    }
  }
}