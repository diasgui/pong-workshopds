

using UnityEngine;

public class MainMenuViewController : ViewController<MainMenuView>, IPlayerCacheObserver
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
        
        _playerCache.RegisterObserver(this);
    }

    public override void Dismiss()
    {
        _playerCache.UnregisterObserver(this);
    }

    void Play()
    {
        Debug.Log("PLAY");
        _playerClient.FindMatch();
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
        var vc = _viewControllerFactory.CreateChangeNameViewController();
        vc.Setup(() =>
        {
            GameObject.Destroy(vc.View.gameObject);
        });
        vc.View.transform.SetParent(View.transform, false);
    }

    public void PlayerCacheChanged()
    {
        View.UpdatePlayerInfo(
            _playerCache.PlayerName.ToUpper(),
            _playerCache.Wins,
            _playerCache.Loses
        );
    }
}
