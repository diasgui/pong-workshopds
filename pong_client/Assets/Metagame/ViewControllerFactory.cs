using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewControllerFactory
{
    private SceneWireframe _wireframe;
    private AssetLoader _assetLoader;

    private PlayerCache _playerCache;

    public ViewControllerFactory(SceneWireframe wireframe, AssetLoader assetLoader, PlayerCache playerCache)
    {
        _wireframe = wireframe;
        _assetLoader = assetLoader;
        _playerCache = playerCache;
    }

    public MainMenuViewController CreateMainMenuViewController()
    {
        var view = _assetLoader.LoadView<MainMenuView>("MainMenu");
        return new MainMenuViewController(view, _playerCache);
    }
}
