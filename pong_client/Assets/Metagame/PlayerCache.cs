using UnityEngine;

public interface PlayerCacheReadOnly
{
    string PlayerName { get; }
    int Score { get; }
    int Wins { get; }
    int Loses { get; }
    bool HasId { get; }
}

public static class PlayerPrefsConst
{
    public static readonly string PlayerId = "playerId";
    public static readonly string PlayerName = "playerName";
}

public class PlayerCache : PlayerCacheReadOnly
{
    string _playerId;
    
    string _playerName = "Guest";
    int _wins = 0;
    int _loses = 0;

    public PlayerCache()
    {
        _playerId = PlayerPrefs.GetString(PlayerPrefsConst.PlayerId, "");
    }

    public void UpdateScore(int wins, int loses)
    {
        _wins = wins;
        _loses = loses;
    }

    public void ChangeName(string name)
    {
        _playerName = name;
    }
    
    public string PlayerName
    {
        get
        {
            return _playerName;
        }
        set
        {
            _playerName = value;
            PlayerPrefs.SetString(PlayerPrefsConst.PlayerName, _playerName);
        }
    }
    
    public string PlayerId
    {
        get
        {
            return _playerId;
        }
        set
        {
            _playerId = value;
            PlayerPrefs.SetString(PlayerPrefsConst.PlayerId, _playerId);
        }
    }

    public int Wins
    {
        get => _wins;
        set => _wins = value;
    }

    public int Loses
    {
        get => _loses;
        set => _loses = value;
    }

    public int Score => Wins - Loses;
    public bool HasId => !string.IsNullOrEmpty(_playerId);
}
