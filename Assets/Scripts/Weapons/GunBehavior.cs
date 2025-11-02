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
    }

    public override void OnUse()
    {
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hitInfo, damageDistance))
        {
            if (hitInfo.transform.TryGetComponent(out EnemyBones enemyBone))
            {
                enemyBone.TakeDamage(damageAmount);
            }
        }
    }
}
