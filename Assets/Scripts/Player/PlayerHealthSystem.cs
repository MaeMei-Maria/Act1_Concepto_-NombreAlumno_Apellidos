using System;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] private FirstPersonController _playerController;
    [SerializeField] private float maxHealth = 100f;
    
    private float currentHealth;

    private void Awake()
    {
        _playerController = GetComponent<FirstPersonController>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            _playerController.OnDeath();
        }
    }
}
