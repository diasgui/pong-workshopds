using System;
using System.Collections.Generic;

public class PlayerClient
{
    readonly PlayerCache _playerCache;
    private ClientRequester _client;

    public PlayerClient(ClientRequester clientRequester, PlayerCache playerCache)
    {
        _playerCache = playerCache;
        _client = clientRequester;
    }
    

    public void CreatePlayer()
    {
        _playerCache.PlayerId = "";

        var parameters = new Dictionary<string, string>();
        parameters["name"] = _playerCache.PlayerName;
        
        _client.Request("sign_up", parameters, (data) =>
        {
            _playerCache.PlayerId = data["playerId"];
        });
    }

    public void AuthenticatePlayer()
    {
        var parameters = new Dictionary<string, string>();
        parameters["playerId"] = _playerCache.PlayerId;
        
        _client.Request("sign_in", parameters, (data) =>
        {
            _playerCache.Loses = int.Parse(data["wins"]);
            _playerCache.Wins = int.Parse(data["loses"]);
            _playerCache.PlayerName = data["name"];
        });
    }

    public void ChangeName(string name)
    {
        string oldName = name;
        _playerCache.PlayerName = name;
        
        var parameters = new Dictionary<string, string>();
        parameters["playerId"] = _playerCache.PlayerId;
        parameters["name"] = name;
        
        _client.RequestAuth("change_name", parameters, null, () =>
        {
            _playerCache.PlayerName = oldName;
        });
    }

    public void LeaderBoard(Action success, Action fail)
    {
        _client.Request("get_leaderboard", new Dictionary<string, string>(), (data) =>
        {
           // TODO: Implement 
        });
    }

    public void FindMatch()
    {
        var parameters = new Dictionary<string, string>();
        parameters["playerId"] = _playerCache.PlayerId;
        
        _client.RequestAuth("find_match", parameters, (data) =>
        {
            // If there is already a player, have fun
            // otherwise, we're fucked, and should assume server state
        });
    }

    public void CancelMatch()
    {
        // TODO: We may implement this if there is enough time
    }

    public void MatchFound()
    {
        // Observer
    }

    public void MatchEnded(int playerScore, int opponentScore)
    {
        // TODO: Call match ended
    }
}
