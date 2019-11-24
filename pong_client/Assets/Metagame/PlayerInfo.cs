
public struct PlayerInfo
{
   public string Name;
   public int Wins;
   public int Losses;

   public PlayerInfo(string playerName, int wins, int losses)
   {
      Name = playerName;
      Wins = wins;
      Losses = losses;
   }
}
