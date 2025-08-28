using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    

    public float _currentHealth;

    public event Action<float> ChangeHealth;
    public event Action Death;
    public float MaxHealth;

    private void Awake()
    {
        _currentHealth = MaxHealth;
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