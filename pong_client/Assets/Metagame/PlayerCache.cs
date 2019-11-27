using System.Collections.Generic;
using UnityEngine;

public interface IPlayerCacheReadOnly
{
    string PlayerName { get; }
    int Score { get; }
    int Wins { get; }
    int Losses { get; }
    bool HasId { get; }

    void RegisterObserver(IPlayerCacheObserver observer);
    void UnregisterObserver(IPlayerCacheObserver observer);
}

public interface IPlayerCacheObserver
{
    void PlayerCacheChanged();
}

public static class PlayerPrefsConst
{
    public static readonly string PlayerId = "playerId";
    public static readonly string PlayerName = "playerName";
}

public class PlayerCache : IPlayerCacheReadOnly
{
    private string _playerId;

    private string _playerName;
    private int _wins;
    private int _losses;

    private List<IPlayerCacheObserver> _observers;

    public PlayerCache()
    {
        _playerId = PlayerPrefs.GetString(PlayerPrefsConst.PlayerId, "");
        _playerName = PlayerPrefs.GetString(PlayerPrefsConst.PlayerName, "Guest");
        _observers = new List<IPlayerCacheObserver>();
    }

    public void UpdateScore(int wins, int losses)
    {
        _wins = wins;
        _losses = losses;
    }

    public void ChangeName(string name)
    {
        _playerName = name;
    }

    public string PlayerName
    {
        get { return _playerName; }
        set
        {
            _playerName = value;
            PlayerPrefs.SetString(PlayerPrefsConst.PlayerName, _playerName);
            PlayerPrefs.Save();
            NotifyObservers();
        }
    }

    public string PlayerId
    {
        get { return _playerId; }
        set
        {
            _playerId = value;
            PlayerPrefs.SetString(PlayerPrefsConst.PlayerId, _playerId);
            PlayerPrefs.Save();
        }
    }

    public int Wins
    {
        get => _wins;
        set => _wins = value;
    }

    public int Losses
    {
        get => _losses;
        set => _losses = value;
    }

    public int Score => Wins - Losses;
    public bool HasId => !string.IsNullOrEmpty(_playerId);

    public void RegisterObserver(IPlayerCacheObserver observer) => _observers.Add(observer);
    public void UnregisterObserver(IPlayerCacheObserver observer) => _observers.Remove(observer);
    public void NotifyObservers() => _observers.ForEach(o => o.PlayerCacheChanged());
}
