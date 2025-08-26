using System;
using UnityEngine;

public class PlayerShooting : MonoBehaviour 
{
    [SerializeField] private Transform _pointShoot;
    [SerializeField] private float _delayShoot;
    [SerializeField] private float _rotationSmootness = 10f;

    private float _nextFireTime = 0f;
    private Camera _camera;

    public event Action Shoot;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private bool CanShoot() {
        return Time.time > _nextFireTime;
    }

    private void Aim() {
        Vector3 mousePosition = _camera.ScreenToWorldPoint(InputManager.Instance.MousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            _rotationSmootness * Time.deltaTime);
    }
    private void Fire() 
    {
        _nextFireTime = Time.time + _delayShoot;
        Bullet bullet = BulletPool.Instance.GetItem();
        bullet.transform.SetPositionAndRotation(_pointShoot.position, transform.rotation);
        Shoot?.Invoke();
    }

    private void Update()
    {
        Aim();
        if (InputManager.Instance.GetShootAction().IsPressed() && CanShoot()) {
            Fire();
        }
    }
}