using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    // serialize field means that you can set it in the editor
    [SerializeField] private float _currentHealth;

    [SerializeField] private float _maxHealth;

    public UnityEvent onDeath;

    public UnityEvent onDamage;

    public UnityEvent onHealthChange;

    // this will allow us to set a period of invincibility to not take damage every frame 
    public bool isInvincible { get; set; }

    public float RemainingHealthPercentage
    {
        get
        {
            return _currentHealth / _maxHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        if (_currentHealth == 0)
        {
            return;
        }

        if (isInvincible)
        {
            return;
        }

        _currentHealth -= damage;

        onHealthChange.Invoke();

        if (_currentHealth  < 0)
        {
            _currentHealth = 0;
        }

        // enemies will still attack and move towards player, might change this to destroy
        if (_currentHealth == 0)
        {
            // disable movement, shooting, and collider for player
            onDeath.Invoke();
            Destroy(gameObject);
        }

        else
        {
            onDamage.Invoke();
        }
    }

    public void AddHealth(float amount)
    {
        if (_currentHealth == _maxHealth)
        {
            return;
        }

        _currentHealth += amount;

        onHealthChange.Invoke();

        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth; 
        }
    }
}
