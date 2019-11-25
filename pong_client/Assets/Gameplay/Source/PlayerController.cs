using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] Transform _playerTransform;
    private Vector2 _targetPosition;
    private float _movementSpeed = 1f;

    public void Setup()
    {
        _targetPosition = _playerTransform.position;
    }

    void Update()
    {
        if (!isLocalPlayer) return;
        
        if (Input.GetMouseButtonDown((int)MouseButton.LeftMouse))
        {
            _targetPosition = Input.mousePosition;
        }

        if (Input.touchCount > 0)
        {
            _targetPosition = Input.touches.Last().position;
        }

        _playerTransform.position += Vector3.left * 
            (_targetPosition.x - _playerTransform.position.x) * _movementSpeed * Time.deltaTime;
    }
}
