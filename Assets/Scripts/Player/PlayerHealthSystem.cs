using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] private EventManagerSO eventManager;
    [SerializeField] private FirstPersonController playerController;
    [SerializeField] private float maxHealth = 100f;

    private float currentHealth;

    private void Awake()
    {
        playerController = GetComponent<FirstPersonController>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        eventManager.PlayerNotifiesDamaged(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            playerController.OnDeath();
        }
    }

    public void Heal(float healAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
        eventManager.InteractableNotifiesHealling(currentHealth, maxHealth);
    }
}