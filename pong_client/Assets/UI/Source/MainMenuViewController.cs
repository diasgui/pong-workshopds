

using UnityEngine;

public class MainMenuViewController : ViewController<MainMenuView>
{
    private readonly IPlayerCacheReadOnly _playerCache;
    private readonly PlayerClient _playerClient;
    private readonly ViewControllerFactory _viewControllerFactory;
    private readonly SceneWireframe _wireframe;
    
    public MainMenuViewController(MainMenuView view, IPlayerCacheReadOnly playerCache, PlayerClient playerClient, ViewControllerFactory viewControllerFactory, SceneWireframe wireframe):base(view)
    {
        _playerCache = playerCache;
        _playerClient = playerClient;
        _viewControllerFactory = viewControllerFactory;
        _wireframe = wireframe;
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
        Debug.Log("PLAY");
    }

    void Leaderboard()
    {
        Debug.Log("LEADERBOARD");
        _playerClient.LeaderBoard(() =>
        {
            var vc = _viewControllerFactory.CreatLeaderboardViewController();
            //vc.Setup();
            _wireframe.PresentView(vc.View);
        }, () =>
        {
            Debug.LogError("Error getting leaderboard");
        });
    }

    void ChangeName()
    {
        Debug.Log("CHANGE NAME");
    }
}
