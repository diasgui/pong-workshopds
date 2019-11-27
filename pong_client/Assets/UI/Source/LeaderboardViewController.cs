

using System;
using System.Collections.Generic;

public class LeaderboardViewController : ViewController<LeaderboardView>
{   
    public LeaderboardViewController(LeaderboardView view) : base(view)
    {
    }

    public void Setup(List<PlayerInfo> ranking, Action backCallback)
    {
        View.Setup(backCallback);
        ranking.ForEach(player => View.AddPlayerCell(player));
    }
    
    
    
}
