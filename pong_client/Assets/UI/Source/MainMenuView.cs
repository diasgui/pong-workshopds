using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : View
{
    [Header("TopBar")] 
    [SerializeField] Text _playerNameLabel;
    [SerializeField] Text _playerWinsLabel;
    [SerializeField] Text _playerLossesLabel;
    
    [Header("Buttons")]
    [SerializeField] Transform _buttonsHolder;
    [SerializeField] MenuButtonView _buttonPrefab;

    public void UpdatePlayerInfo(string name, int wins, int losses)
    {
        _playerNameLabel.text = name;
        _playerWinsLabel.text = $"W: {wins}";
        _playerLossesLabel.text = $"L: {losses}";
    }

    public void AddButton(string buttonText, Action buttonCallback)
    {
        Instantiate(_buttonPrefab, _buttonsHolder)
            .Setup(buttonText, buttonCallback);
    }
    
}
