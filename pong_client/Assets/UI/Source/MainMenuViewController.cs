

public class MainMenuViewController : ViewController<MainMenuView>
{
    readonly IPlayerCacheReadOnly _playerCache;
    
    public MainMenuViewController(MainMenuView view, IPlayerCacheReadOnly playerCache):base(view)
    {
        _playerCache = playerCache;
    }

    public void Setup()
    {
        View.UpdatePlayerInfo(
            _playerCache.PlayerName.ToUpper(),
            _playerCache.Wins,
            _playerCache.Loses
            );
        View.AddButton("PLAY", Play);
        View.AddButton("LEADERBOARD", Leaderboard);
        View.AddButton("CHANGE NAME", ChangeName);
    }

    void Play()
    {
        
    }

    void Leaderboard()
    {
        
    }

    void ChangeName()
    {
        
    }
}
