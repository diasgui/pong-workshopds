using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class GameplayView : View
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

    private Action _hostWin;
    private Action _guestWin;

    public void Setup(Action hostWin, Action guestWin)
    {
        Assert.IsNotNull(hostWin);
        Assert.IsNotNull(guestWin);
        _hostWin = hostWin;
        _guestWin = guestWin;
    }

    public void AddPlayer(PlayerController player)
    {
        if (_hostController == null)
        {
            _hostController = player;
            _hostController.transform.SetParent(_gameRect, false);
            _hostController.transform.position = _hostPlayerStartPosition.position;
            _hostController.Setup();
        }
        else if (_guestController == null)
        {
            _guestController = player;
            _guestController.transform.SetParent(_gameRect, false);
            _guestController.transform.position = _guestPlayerStartPosition.position;
            _guestController.Setup();
        }

        CheckStart();
    }

    public void CheckStart()
    {
        if (_ballController == null)
        {
            _ballController = Instantiate(_ballPrefab, _gameRect);
            _ballController.transform.SetParent(_gameRect, false);
            _ballController.transform.position = Vector3.zero;
            _ballController.Setup();
        }
        
        if (!_gameStarted && _hostController != null && _guestController != null)
        {
            _gameStarted = true;
            StartRound();
        }
    }

    void StartRound()
    {
        if (_hostScore >= 5)
        {
            _hostWin();
        }
        else if (_guestScore >= 5)
        {
            _guestWin();
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
        if (!_gameStarted) return;
        
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
