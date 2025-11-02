using System;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] private EventManagerSO _eventManagerSo;
    [SerializeField] private FirstPersonController _playerController;
    [SerializeField] private float maxHealth = 100f;
    
    private float currentHealth;

    private void Awake()
    {
        _playerController = GetComponent<FirstPersonController>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        _eventManagerSo.PlayerNotifiesDamaged(currentHealth, maxHealth);

        if (!(currentHealth <= 0)) return;
        currentHealth = 0;
        _playerController.OnDeath();
    }
    
    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        _eventManagerSo.InteractableNotifiesHealling(currentHealth, maxHealth);
    }
}
