using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector2 _movement;
    private Rigidbody2D _rb;
    private bool _isRunning = false;

    public event Action<bool> IsRunning;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    private void Update()
    {
        _movement = InputManager.Instance.Move;
        CheckingStatusMovement();
    }
    private void FixedUpdate()
    {
        Vector2 targetPosition = _rb.position + _movement * (_speed * Time.fixedDeltaTime);
        _rb.MovePosition(targetPosition);
    }

    private void CheckingStatusMovement() 
    {
        if (_movement == Vector2.zero)
        {
            if (_isRunning) {
                IsRunning?.Invoke(false);
                _isRunning = false;
            }
        }
        else if (!_isRunning){
            IsRunning?.Invoke(true);
            _isRunning = true;
        }
    }
}