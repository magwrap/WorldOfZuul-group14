namespace WorldOfZuul
{
    public class User
    {
      private protected string? Username { get; set;}
      private protected string? PlayerClass {get; set;}

      public void AskForUserName()
      {
        Username = GameConsole.Input("Whats your name?");
      }

      public void AskForPlayerClass()
      {
        PlayerClass = GameConsole.Input("Class: ");
      }

      public string GetUsername()
      {
      if (string.IsNullOrEmpty(Username)) return "User not found";
        return Username;
      }
        
    }
}