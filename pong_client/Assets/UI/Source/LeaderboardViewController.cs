

using System;
using System.Collections.Generic;

public class LeaderboardViewController : ViewController<LeaderboardView>
{   
    public LeaderboardViewController(LeaderboardView view, IPlayerCacheReadOnly playerCache) : base(view)
    {
    }

    public void Setup(List<PlayerInfo> ranking, Action backCallback)
    {
        
    }
    
    
    
}
