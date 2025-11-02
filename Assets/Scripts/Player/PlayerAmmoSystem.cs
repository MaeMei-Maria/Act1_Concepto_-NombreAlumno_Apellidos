using UnityEngine;

public class PlayerAmmoSystem : MonoBehaviour
{
    [SerializeField] private EventManagerSO _eventManagerSo;
    [SerializeField] private FirstPersonController _playerController;
    [SerializeField] private float maxAmmoGun = 50f;
    [SerializeField] private float maxAmmCluster = 5f;

    private float currentAmmoGun;
    private float currentAmmCluster;

    private void Awake()
    {
        _playerController = GetComponent<FirstPersonController>();
        currentAmmoGun = maxAmmoGun;
        currentAmmCluster = maxAmmCluster;
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
        _eventManagerSo.PlayerUseGrenadeAmmo(currentAmmCluster, currentAmmCluster);

        if (!(currentAmmoGun <= 0)) return;
        currentAmmoGun = 0;
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
