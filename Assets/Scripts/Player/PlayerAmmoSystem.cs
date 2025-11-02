using System;
using UnityEngine;

public class PlayerAmmoSystem : MonoBehaviour
{
    [SerializeField] private EventManagerSO _eventManagerSo;
    [SerializeField] private float maxAmmoGun = 50f;
    [SerializeField] private float maxAmmCluster = 5f;

    public float currentAmmoGun;
    public float currentAmmCluster;
    
    public float MaxAmmoGun { get; private set; }
    public float MaxAmmoCluster { get; private set; }

    private void Awake()
    {
        MaxAmmoCluster = maxAmmCluster;
        MaxAmmoGun = maxAmmoGun;
        
        currentAmmoGun = maxAmmoGun;
        currentAmmCluster = maxAmmCluster;
    }

    private void Start()
    {
        // Avisar a la UI cuánta munición hay al inicio
        _eventManagerSo.PlayerUseGunAmmo(currentAmmoGun, maxAmmoGun);
        _eventManagerSo.PlayerUseGrenadeAmmo(currentAmmCluster, maxAmmCluster);
    }

    public void DecreaseAmmoGun(float lostAmmo)
    {
        currentAmmoGun -= lostAmmo;
        _eventManagerSo.PlayerUseGunAmmo(currentAmmoGun, maxAmmoGun);

        if (!(currentAmmoGun <= 0)) return;
        currentAmmoGun = 0;
    }
    
    public void DecreaseAmmoCluster(float lostAmmo)
    {
        currentAmmCluster -= lostAmmo;
        _eventManagerSo.PlayerUseGrenadeAmmo(currentAmmCluster, maxAmmCluster);

        if (!(currentAmmoGun <= 0)) return;
        currentAmmCluster = 0;
    }
    
    public void GetGunAmmo(float ammoAmount)
    {
        currentAmmoGun += ammoAmount;
        
        if (currentAmmoGun >= maxAmmoGun)
        {
            currentAmmoGun = maxAmmoGun;
        }
        _eventManagerSo.AmmoGunGetted(currentAmmoGun, maxAmmoGun);
    }

    public void GetClusterAmmo(float ammoAmount)
    {
        currentAmmCluster += ammoAmount;

        if (currentAmmCluster >= maxAmmCluster)
        {
            currentAmmCluster = maxAmmCluster;
        }
        _eventManagerSo.AmmoGrenadeGetted(currentAmmCluster, maxAmmCluster);
    }
}
