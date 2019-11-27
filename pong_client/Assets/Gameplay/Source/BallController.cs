using UnityEngine;
using UnityEngine.Networking;

public class BallController : NetworkBehaviour
{
    private Vector2 _direction;
    private float _speed;
    private float _speedInitial = 10f;
    private float _speedMax = 100f;
    private float _speedStep = 5f;

    private bool _collided;

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
        _speed = Mathf.Min(_speed + _speedStep, _speedMax);
    }
    
    public void BounceVertical()
    {
        _direction *= new Vector2(1f, -1f);
        _speed = Mathf.Min(_speed + _speedStep, _speedMax);
    }

    private void Update()
    {
        transform.position += (Vector3)(_direction * _speed * Time.deltaTime);
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_collided)
        {
            _collided = true;
            BounceVertical();
        }
    }
    
    void OnCollisionExit2D(Collision2D collision)
    {
        _collided = false;
    }
}
