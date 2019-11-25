using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer
{
    SceneWireframe _wireframe;
    PlayerClient _playerClient;
    PlayerCache _playerCache;
    ViewControllerFactory _viewControllerFactory;
    MatchFactory _matchFactory;
    
    public GameInitializer(ClientRequester clientRequester, SceneWireframe wireframe, AssetLoader assetLoader)
    {
        _wireframe = wireframe;
        
        _playerCache = new PlayerCache();
        _playerClient = new PlayerClient(clientRequester, _playerCache);
        _viewControllerFactory = new ViewControllerFactory(wireframe, assetLoader, _playerCache, _playerClient);
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
        _wireframe.PresentView(vc.View);
        
//        _playerClient.MatchEnded("fb0e1c63-9466-49a8-80ad-8fec7929bec7", _playerCache.PlayerId);
    }
}
