using System;
using UnityEngine;

public class LeaderboardView : View
{
    [SerializeField] Transform _cellsHolder;
    [SerializeField] PlayerInfoCellView _cellPrefab;
    Action _backCallback;
    
    
    public void Setup()
    {
        
    }

    public void OnBackClicked()
    {
        _backCallback?.Invoke();
    }
}
