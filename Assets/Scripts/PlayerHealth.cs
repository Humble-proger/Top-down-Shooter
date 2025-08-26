using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    public float _currentHealth;

    public event Action<float> ChangeHealth;
    public event Action Death;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage) {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Death?.Invoke();
            _currentHealth = 0f;
        }
        ChangeHealth?.Invoke(_currentHealth);
    }
}