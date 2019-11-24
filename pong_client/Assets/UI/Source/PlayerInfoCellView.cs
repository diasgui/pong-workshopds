using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoCellView : MonoBehaviour
{
    [SerializeField] Text _playerNameLabel;
    [SerializeField] Text _playerWinsLabel;
    [SerializeField] Text _playerLossesLabel;
    
    public void UpdatePlayerInfo(string playerName, int wins, int losses)
    {
        _playerNameLabel.text = playerName;
        _playerWinsLabel.text = $"W: {wins}";
        _playerLossesLabel.text = $"L: {losses}";
    }
}
