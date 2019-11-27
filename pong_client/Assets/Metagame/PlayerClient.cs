using System;
using System.Collections.Generic;
using SimpleJSON;

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
            _playerCache.Wins = response["wins"];
            _playerCache.Loses = response["losses"];
            _playerCache.PlayerName = response["name"];
        }, () =>
        {
            _playerCache.PlayerName = oldName;
        });
    }

    public void LeaderBoard(Action<List<PlayerInfo>> success, Action fail)
    {
        _client.RequestGet("leaderboard", (response) =>
        {
            List<PlayerInfo> ranking = new List<PlayerInfo>();
            foreach (JSONObject player in response.AsArray)
            {
                ranking.Add(new PlayerInfo(
                    player["name"], 
                    player["wins"], 
                    player["losses"])
                );
            }
            success?.Invoke(ranking);
        });
        // TODO: Add fail popup
    }

    public void FindMatch()
    {
        var parameters = new Dictionary<string, string>();
        parameters["url"] = "http://192.168.0.14:7777";
        
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

    public void MatchEnded(string winnerId, string loserId)
    {
        var parameters = new Dictionary<string, string>();
        parameters["winner_id"] = winnerId;
        parameters["loser_id"] = loserId;
        
        _client.RequestAuth("match_end", parameters, (data) =>
        {
            
        });
    }
}
