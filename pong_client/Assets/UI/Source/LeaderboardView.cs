using System;
using UnityEngine;

public class LeaderboardView : View
{
    [SerializeField] Transform _cellsHolder;
    [SerializeField] PlayerInfoCellView _cellPrefab;
    Action _backCallback;
    
    
    public void Setup(Action backCallback)
    {
        _backCallback = backCallback;
    }

    public void AddPlayerCell(PlayerInfo player)
    {
        var cell = Instantiate(_cellPrefab, _cellsHolder);
        cell.UpdatePlayerInfo(player.Name, player.Wins, player.Losses);
    }

    public void OnBackClicked()
    {
        _backCallback?.Invoke();
    }
}
