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

    //TODO: adjust the values of thresholds and rewards/punishments so it's optimal

    public static void Initialize()
    {
      ReputationScore = 50;
    }
    public static void MadeGoodDecision()
    {
      ReputationScore += 6;
    }

    public static void MadeBadDecision()
    {
      ReputationScore -= 5;
    }

    public static void AnnoyedSomeone()
    {
      ReputationScore -= 1;
    }

    public static void MadeSomeoneSmile()
    {
      ReputationScore += 2;
    }

    public static void FailedMission()
    {
      ReputationScore -= 10;
    }

    public static void FinishedMission()
    {
      ReputationScore += 11;
    }

    public static void GetReputationState()
    {
      //TODO: make responses more witty
      switch (ReputationScore)
      {
        case var _ when ReputationScore >= (int)ReputationThresholds.PERFECT:
          GameConsole.WriteLine("You have a perfect reputation!", font: FontTheme.Success);
          return;
        case var _ when ReputationScore >= (int)ReputationThresholds.GOOD:
          GameConsole.WriteLine("You have a good reputation!", font: FontTheme.Success);
          return;
        case var _ when ReputationScore >= (int)ReputationThresholds.BAD:
          GameConsole.WriteLine("Sorry, your reputations is bad...", font: FontTheme.Danger);
          return;
        case var _ when ReputationScore >= (int)ReputationThresholds.HORRIBLE:
          GameConsole.WriteLine("Your reputation is HORRIBLE!!", font: FontTheme.Danger);
          return;

      }
    }
  }
}