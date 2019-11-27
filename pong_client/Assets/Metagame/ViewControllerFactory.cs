using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewControllerFactory
{
    private readonly SceneWireframe _wireframe;
    private readonly AssetLoader _assetLoader;

    private readonly PlayerCache _playerCache;
    private readonly PlayerClient _playerClient;

    public ViewControllerFactory(SceneWireframe wireframe, AssetLoader assetLoader, PlayerCache playerCache, PlayerClient playerClient)
    {
        _wireframe = wireframe;
        _assetLoader = assetLoader;
        _playerCache = playerCache;
        _playerClient = playerClient;
    }

    public MainMenuViewController CreateMainMenuViewController()
    {
        var view = _assetLoader.LoadView<MainMenuView>("MainMenuView");
        return new MainMenuViewController(view, _playerCache, _playerClient, this, _wireframe);
    }
    
    public LeaderboardViewController CreatLeaderboardViewController()
    {
        var view = _assetLoader.LoadView<LeaderboardView>("LeaderboardView");
        return new LeaderboardViewController(view);
    }

    public ChangeNameViewController CreateChangeNameViewController()
    {
        var view = _assetLoader.LoadView<ChangeNameView>("ChangeNameView");
        return new ChangeNameViewController(view, _playerClient);
    }
}
