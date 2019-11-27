
using UnityEngine.Networking;

public class ViewControllerFactory
{
    private readonly SceneWireframe _wireframe;
    private readonly AssetLoader _assetLoader;

    private readonly PlayerCache _playerCache;
    private readonly PlayerClient _playerClient;
    private readonly PongNetworkManager _networkManager;

    public ViewControllerFactory(SceneWireframe wireframe, AssetLoader assetLoader, PlayerCache playerCache, PlayerClient playerClient, PongNetworkManager networkManager)
    {
        _wireframe = wireframe;
        _assetLoader = assetLoader;
        _playerCache = playerCache;
        _playerClient = playerClient;
        _networkManager = networkManager;
    }

    public MainMenuViewController CreateMainMenuViewController()
    {
        return new MainMenuViewController(
            _assetLoader.LoadView<MainMenuView>("MainMenuView"),
            _playerCache,
            _playerClient,
            this,
            _wireframe
            );
    }
    
    public LeaderboardViewController CreatLeaderboardViewController()
    {
        return new LeaderboardViewController(_assetLoader.LoadView<LeaderboardView>("LeaderboardView"));
    }

    public ChangeNameViewController CreateChangeNameViewController()
    {
        return new ChangeNameViewController(
            _assetLoader.LoadView<ChangeNameView>("ChangeNameView"),
            _playerClient
            );
    }

    public GameplayViewController CreateGameplayViewController()
    {
        return new GameplayViewController(
            _assetLoader.LoadView<GameplayView>("GameplayView"),
            _networkManager
            );
    } 
}
