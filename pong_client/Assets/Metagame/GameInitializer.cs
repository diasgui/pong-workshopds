using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameInitializer
{
    SceneWireframe _wireframe;
    PlayerClient _playerClient;
    PlayerCache _playerCache;
    ViewControllerFactory _viewControllerFactory;
    
    public GameInitializer(ClientRequester clientRequester, SceneWireframe wireframe, AssetLoader assetLoader, PongNetworkManager networkManager)
    {
        _wireframe = wireframe;
        
        _playerCache = new PlayerCache();
        _playerClient = new PlayerClient(clientRequester, _playerCache);
        _viewControllerFactory = new ViewControllerFactory(wireframe, assetLoader, _playerCache, _playerClient, networkManager);
    }

    public void Initialize()
    {
        // TODO: Add loading screen
        if(_playerCache.HasId) _playerClient.AuthenticatePlayer(DisplayMainMenu);
        else _playerClient.CreatePlayer(DisplayMainMenu);
    }

    void DisplayMainMenu()
    {
        Debug.Log($"{_playerCache.PlayerName}: ({_playerCache.Wins}/{_playerCache.Losses})");
        
        var vc = _viewControllerFactory.CreateMainMenuViewController();
        vc.Setup();
        _wireframe.PresentViewController(vc);
    }
}
