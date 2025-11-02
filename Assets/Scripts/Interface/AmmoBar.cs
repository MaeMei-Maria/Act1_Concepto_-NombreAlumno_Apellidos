using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : MonoBehaviour
{
    [SerializeField] private EventManagerSO _eventManagerSo;
    [SerializeField] private Image healthBar;
    
    private void OnEnable()
    {
        _eventManagerSo.OnPlayerDamaged += UpdateHealthBar;
        _eventManagerSo.OnPlayerHealed += UpdateHealthBar;
    }

    private void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    private void OnDisable()
    {
        _eventManagerSo.OnPlayerDamaged -= UpdateHealthBar;
        _eventManagerSo.OnPlayerHealed -= UpdateHealthBar;
    }
}
