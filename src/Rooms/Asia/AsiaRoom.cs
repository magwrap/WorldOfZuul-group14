namespace WorldOfZuul
{
  class AsiaRoom : Room
  {
    bool continuePlaying = true;

    public static string? Inicials { get; set; }

    static NPC parkRanger = new("Park Ranger");

    private static bool AsiaMission { get; set; }

    public void InProccess(bool isInProcess)
    {
      AsiaMission = isInProcess;
    }
    public static bool OngoingMission()
    {
      return AsiaMission;
    }

    public AsiaRoom(string shortDesc, string longDesc) : base(shortDesc, longDesc)
    {

    }

    public void CurrentlyInAsiaRoom(ref Room? currentRoom, ref Room? previousRoom)
    {
      LoadingAnimation.Loading("Mission Loading");
      GameConsole.Clear();

      InProccess(true);

      PrintIntroductionToTheRoom();
      previousRoom = null;

      while (continuePlaying)
      {
        Command? command = Game.AskForCommand();
        continuePlaying = Actions.DecideAction(ref command, ref currentRoom, ref previousRoom, true, "asia");
      }
    }
    public static void PrintIntroductionToTheRoom()
    {
      parkRanger.Speak("Poaching across Asia is reaching critical levels, driven by an unrelenting demand for illegal wildlife products.\nI am be here to guide you through the brief introduction into the quest, the rest falls upon your individual choices. \nHope you are up for the task, the poachers around here are relentless!");
    }

  }
}