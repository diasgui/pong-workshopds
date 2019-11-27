using System;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;

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
            var data = response.AsObject;
            _playerCache.PlayerId = data["id"];
            _playerCache.Wins = data["wins"];
            _playerCache.Losses = data["losses"];
            
            success?.Invoke();
        });
    }

    public void AuthenticatePlayer(Action success)
    {
        var parameters = new Dictionary<string, string>();
        parameters["id"] = _playerCache.PlayerId;
        
        _client.Request("sign_in", parameters, (response) =>
        {
            var data = response.AsObject;
            _playerCache.PlayerId = data["id"];
            _playerCache.Wins = data["wins"];
            _playerCache.Losses = data["losses"];
            
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
            var data = response.AsObject;
            _playerCache.Wins = data["wins"];
            _playerCache.Losses = data["losses"];
            _playerCache.PlayerName = data["name"];
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

    public void FindMatch(Action<string> joinMatch, Action hostMatch)
    {
        var parameters = new Dictionary<string, string>();
        parameters["url"] = NetworkManager.singleton.networkAddress;
        
        _client.RequestAuth("find_match", parameters, (response) =>
        {
            var data = response.AsObject;
            string status = data["status"];
            if (status == "found")
            {
                joinMatch(data["match_url"]);
            }
            else if (status == "waiting")
            {
                hostMatch();
            }
            else
            {
                Debug.LogError($"Unknown status: {status}");
                Debug.LogError($"Unknown status: {data}");
            }
        });
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
