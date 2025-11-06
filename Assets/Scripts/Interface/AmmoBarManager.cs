using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoBarManager : MonoBehaviour
{
    [SerializeField] private EventManagerSO _eventManagerSo;
    [SerializeField] private Image ammoGunBar;
    [SerializeField] private Image ammoGrenadeBar;
    [SerializeField] private TextMeshProUGUI ammoGrenadeText;
    [SerializeField] private TextMeshProUGUI ammoGunText;

    private void OnEnable()
    {
        // Escucha los eventos de munición de arma
        _eventManagerSo.OnPlayerUseAmmoGun += UpdateGunAmmoBar;
        _eventManagerSo.OnAmmoGunGetted += UpdateGunAmmoBar;
        
        _eventManagerSo.OnPlayerUseGrenade += UpdateClusterAmmoBar;
        _eventManagerSo.OnAmmoGrenadeGetted += UpdateClusterAmmoBar;
    }

    private void UpdateClusterAmmoBar(float currentAmmo, float maxAmmo)
    {
        ammoGrenadeBar.fillAmount = currentAmmo / maxAmmo;
        ammoGrenadeText.text = currentAmmo.ToString("00");
    }

    private void UpdateGunAmmoBar(float currentAmmo, float maxAmmo)
    {
        ammoGunBar.fillAmount = currentAmmo / maxAmmo;
        ammoGunText.text = currentAmmo.ToString("00");
    }

    private void OnDisable()
    {
        _eventManagerSo.OnPlayerUseAmmoGun -= UpdateGunAmmoBar;
        _eventManagerSo.OnAmmoGunGetted -= UpdateGunAmmoBar;
        
        _eventManagerSo.OnPlayerUseGrenade -= UpdateClusterAmmoBar;
        _eventManagerSo.OnAmmoGrenadeGetted -= UpdateClusterAmmoBar;
    }
}

