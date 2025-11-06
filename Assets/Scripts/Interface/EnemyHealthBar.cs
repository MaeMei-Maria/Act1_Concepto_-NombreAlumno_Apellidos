using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private EventManagerSO _eventManagerSo;
    [SerializeField] private Image healthBar;

    private Camera _camera;
    private void Awake()
    {
        _camera = Camera.main;   
    }

    private void OnEnable()
    {
        _eventManagerSo.OnEnemyDamaged += UpdateHealthBar;
    }

    private void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    private void LateUpdate()
    {
        // Hacer que el transform mire hacia la cámara
        transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);
    }

    private void OnDisable()
    {
        _eventManagerSo.OnEnemyDamaged -= UpdateHealthBar;
    }
}
