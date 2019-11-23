using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer
{
    PlayerClient _playerClient;
    PlayerCache _playerCache;
    ViewControllerFactory _viewControllerFactory;
    MatchFactory _matchFactory;
    
    public GameInitializer(ClientRequester clientRequester)
    {
        _playerCache = new PlayerCache();
        _playerClient = new PlayerClient(clientRequester, _playerCache);
        _viewControllerFactory = new ViewControllerFactory();
        _matchFactory = new MatchFactory();
    }

    public void Initialize()
    {
        // TODO: Load some loading screen assets
        if(_playerCache.HasId) _playerClient.AuthenticatePlayer();
        else _playerClient.CreatePlayer();
        // TODO: Display Main Menu
    }
}
