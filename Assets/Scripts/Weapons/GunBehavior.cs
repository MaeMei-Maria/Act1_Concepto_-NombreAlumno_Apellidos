using System;
using UnityEngine;

public class GunBehavior : Weapon
{
    [SerializeField] private float damageDistance = 500f;
    [SerializeField] private float damageAmount = 20f;
    
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        playerAmmoSystem = GetComponentInParent<PlayerAmmoSystem>();
    }

    public override void OnUse()
    {
        if(playerAmmoSystem.currentAmmoGun <= 0) return; //Evitamos volver a disparar si no hay balas.
        
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hitInfo, damageDistance))
        {
            if (hitInfo.transform.TryGetComponent(out EnemyBones enemyBone))
            {
                enemyBone.TakeDamage(damageAmount);
            }
        }
        
        playerAmmoSystem.DecreaseAmmoGun(1);
    }
}
