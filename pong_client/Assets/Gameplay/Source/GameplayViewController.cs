
using UnityEngine.Networking;

public class GameplayViewController : ViewController<GameplayView>
{
    private readonly PongNetworkManager _networkManager;
    
    public GameplayViewController(GameplayView view, PongNetworkManager networkManager) : base(view)
    {
        _networkManager = networkManager;
        _networkManager.Controller = this;
    }

    public void Setup()
    {
        
    }

    public void Join(string serverIp)
    {
        _networkManager.client.Connect(serverIp, NetworkManager.singleton.networkPort);
    }

    public void Host()
    {
        _networkManager.StartHost();
        _networkManager.client.Connect(_networkManager.networkAddress, NetworkManager.singleton.networkPort);
    }

    public void AddPlayer(PlayerController player) => View.AddPlayer(player);
}