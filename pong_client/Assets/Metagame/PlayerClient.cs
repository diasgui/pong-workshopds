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
    
    public void CreatePlayer(Action success)
    {
        _playerCache.PlayerId = "";

        var parameters = new Dictionary<string, string>();
        parameters["name"] = _playerCache.PlayerName;
        
        _client.Request("sign_up", parameters, (response) =>
        {
            _playerCache.PlayerId = response["id"];
            _playerCache.Wins = response["wins"];
            _playerCache.Loses = response["losses"];
//            _playerCache.PlayerName = response["name"];
            
            success?.Invoke();
        });
    }

    public void AuthenticatePlayer(Action success)
    {
        var parameters = new Dictionary<string, string>();
        parameters["id"] = _playerCache.PlayerId;
        
        _client.Request("sign_in", parameters, (response) =>
        {
            _playerCache.PlayerId = response["id"];
            _playerCache.Wins = response["wins"];
            _playerCache.Loses = response["losses"];
//            _playerCache.PlayerName = response["name"];
            
            success?.Invoke();
        });
    }

    public void ChangeName(string name)
    {
        string oldName = name;
        _playerCache.PlayerName = name;
        
        var parameters = new Dictionary<string, string>();
        parameters["name"] = name;
        
        _client.RequestAuth("change_name", parameters, (response) =>
        {
            _playerCache.PlayerId = response["id"];
            _playerCache.Wins = response["wins"];
            _playerCache.Loses = response["losses"];
            _playerCache.PlayerName = response["name"];
        }, () =>
        {
            _playerCache.PlayerName = oldName;
        });
    }

    public void LeaderBoard(Action success, Action fail)
    {
        _client.RequestGet("leaderboard", (response) =>
        {
           // TODO: Implement 
        });
    }

    public void FindMatch()
    {
        var parameters = new Dictionary<string, string>();
        
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
