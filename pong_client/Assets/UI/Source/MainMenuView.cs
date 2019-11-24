using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : View
{
    [Header("TopBar")] 
    [SerializeField] PlayerInfoCellView _playerInfo;
    
    [Header("Buttons")]
    [SerializeField] Transform _buttonsHolder;
    [SerializeField] MenuButtonView _buttonPrefab;

    public void UpdatePlayerInfo(string name, int wins, int losses)
    {
        _playerInfo.UpdatePlayerInfo(name, wins, losses);
    }

    public void AddButton(string buttonText, Action buttonCallback)
    {
        Instantiate(_buttonPrefab, _buttonsHolder)
            .Setup(buttonText, buttonCallback);
    }
    
}
