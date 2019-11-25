using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Game Entities")]
    [SerializeField] BallController _ballPrefab;
    [SerializeField] RectTransform _gameRect;
    [SerializeField] Transform _hostPlayerStartPosition;
    [SerializeField] Transform _guestPlayerStartPosition;

    [Header("UI")]
    [SerializeField] Text _scoreLabel;

    bool _gameStarted = false;
    BallController _ballController;
    PlayerController _hostController;
    PlayerController _guestController;

    private int _hostScore;
    private int _guestScore;

    public void AddPlayer(PlayerController player)
    {
        if (_hostController == null)
        {
            _hostController = player;
            _hostController.transform.position = _hostPlayerStartPosition.position;
            _hostController.Setup();
        }
        else if (_guestController == null)
        {
            _guestController = player;
            _guestController.transform.position = _guestPlayerStartPosition.position;
            _guestController.Setup();
        }

        CheckStart();
    }

    void CheckStart()
    {
        if (_ballController == null)
        {
            _ballController = Instantiate(_ballPrefab, _gameRect);
            _ballController.transform.position = Vector3.zero;
            _ballController.Setup();
        }
        
        if (!_gameStarted && _hostController != null && _guestController != null)
        {
            StartRound();
        }
    }

    void StartRound()
    {
        if (_hostScore >= 5)
        {
            // TODO: Host wins
        }
        else if (_guestScore >= 5)
        {
            // TODO: Guest wins
        }
        else
        {
            _hostController.transform.position = _hostPlayerStartPosition.position;
            _guestController.transform.position = _guestPlayerStartPosition.position;
            _ballController.ReleaseBall();
        }
    }

    void Update()
    {
        Vector2 ballPosition = _ballController.transform.position;
        if (ballPosition.x > _gameRect.rect.width || ballPosition.x < 0)
        {
            _ballController.BounceHorizontal();
        }

        if (ballPosition.y > _gameRect.rect.height)
        {
            _ballController.FreezeBall();
            _ballController.transform.position = Vector3.zero;
            _guestScore += 1;
            StartRound();
        }
        else if (ballPosition.y < 0)
        {
            _ballController.FreezeBall();
            _ballController.transform.position = Vector3.zero;
            _hostScore += 1;
            StartRound();
        }

        _scoreLabel.text = $"{_hostScore - _guestScore}";
    }
}
