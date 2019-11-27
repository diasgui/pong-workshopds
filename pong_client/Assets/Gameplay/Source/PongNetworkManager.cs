
using UnityEngine;
using UnityEngine.Networking;

public class PongNetworkManager : NetworkManager
{
    private GameplayViewController _controller;

    public GameplayViewController Controller
    {
        set => _controller = value;
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId) {
        var player = Instantiate(playerPrefab);
        NetworkServer.AddPlayerForConnection(conn, player.gameObject, playerControllerId);
        Debug.Log("Client connected!");
        _controller.AddPlayer(player.GetComponent<PlayerController>());
    }
    
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        Debug.Log("Connected successfully to server.");
    }
}
