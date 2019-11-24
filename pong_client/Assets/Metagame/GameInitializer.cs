using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer
{
    PlayerClient _playerClient;
    PlayerCache _playerCache;
    ViewControllerFactory _viewControllerFactory;
    MatchFactory _matchFactory;
    
    public GameInitializer(ClientRequester clientRequester, SceneWireframe wireframe, AssetLoader assetLoader)
    {
        _playerCache = new PlayerCache();
        _playerClient = new PlayerClient(clientRequester, _playerCache);
        _viewControllerFactory = new ViewControllerFactory(wireframe, assetLoader, _playerCache);
        _matchFactory = new MatchFactory();
    }

    public void Initialize()
    {
        // TODO: Add loading screen
        if(_playerCache.HasId) _playerClient.AuthenticatePlayer(DisplayMainMenu);
        else _playerClient.CreatePlayer(DisplayMainMenu);
    }

    void DisplayMainMenu()
    {
        Debug.Log($"{_playerCache.PlayerName}: ({_playerCache.Wins}/{_playerCache.Loses})");
        
        var vc = _viewControllerFactory.CreateMainMenuViewController();
        vc.Setup();
        vc.Present();
    }
}
