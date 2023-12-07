using WorldOfZuul.src;

namespace WorldOfZuul
{
    class UNRoom
    {
        public static void StartLastMission()
        {   
            GameConsole.Clear();
            GameConsole.WriteLine("Congratulations! You have successfully completed all three missions, let's make your work count!");
            Thread.Sleep(5000);
            GameConsole.Clear();

            if(Reputation.ReputationScore < 50) //number of points that determines the scenario for the player 
                ReputationScore1(); 
            else if(Reputation.ReputationScore < 99)
                ReputationScore2(); 
            else
                ReputationScore3();

        }


        //1 star scenario
        private static void ReputationScore1()
        {
            
        }


        //2 stars scenario
        private static void ReputationScore2()
        {

        }


        //3 stars scenario
        private static void ReputationScore3()
        {

        }
    }

   
}