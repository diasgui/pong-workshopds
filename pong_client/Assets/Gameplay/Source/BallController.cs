using UnityEngine;
using UnityEngine.Networking;

public class BallController : NetworkBehaviour
{
    private Vector2 _direction;
    private float _speed;
    private float _speedInitial = 1f;
    private float _speedMax = 100f;

    public void Setup()
    {
        _direction = Random.insideUnitCircle;
        _speed = 0f;
    }
    
    public void ReleaseBall()
    {
        _speed = _speedInitial;
    }

    public void FreezeBall()
    {
        _speed = 0;
    }

    public void BounceHorizontal()
    {
        _direction *= new Vector2(-1f, 1f);
    }
    
    public void BounceVertical()
    {
        _direction *= new Vector2(1f, -1f);
    }

    private void Update()
    {
        transform.position += (Vector3)(_direction * _speed * Time.deltaTime);
    }
}
