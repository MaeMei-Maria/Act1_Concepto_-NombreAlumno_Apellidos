using System;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    private PlayerHealthSystem playerHealthSystem;
    private PlayerAmmoSystem playerAmmoSystem;

    public event Action<float> OnAmmoGunBoxCollected;
    public event Action<float> OnAmmoGrenadeBoxCollected;
    public event Action<float> OnHealed;

    private void Awake()
    {
        playerAmmoSystem = GetComponentInChildren<PlayerAmmoSystem>();
        playerHealthSystem = GetComponentInChildren<PlayerHealthSystem>();
    }

    private void OnEnable()
    {
        OnAmmoGunBoxCollected += playerAmmoSystem.AddGunAmmo;
        OnAmmoGrenadeBoxCollected += playerAmmoSystem.AddClusterAmmo;
        OnHealed += playerHealthSystem.Heal;
    }

    private void OnDisable()
    {
        OnAmmoGunBoxCollected -= playerAmmoSystem.AddGunAmmo;
        OnAmmoGrenadeBoxCollected -= playerAmmoSystem.AddClusterAmmo;
        OnHealed -= playerHealthSystem.Heal;
    }

    public void NotifyAmmoGunCollected(float amount)
    {
        OnAmmoGunBoxCollected?.Invoke(amount);
    }

    public void NotifyAmmoGrenadeCollected(float amount)
    {
        OnAmmoGrenadeBoxCollected?.Invoke(amount);
    }

    public void NotifyHealed(float amount)
    {
        OnHealed?.Invoke(amount);
    }
}