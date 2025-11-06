using System;
using UnityEngine;
using UnityEngine.Pool;

public class GunBehavior : Weapon
{
    [SerializeField] private ParticlesBehavior gunParticles;
    [SerializeField] private Transform spawnPoint;
    
    [SerializeField] private float damageDistance = 500f;
    [SerializeField] private float damageAmount = 20f;

    private ObjectPool<ParticlesBehavior> gunParticlesPool;
    
    private Camera _camera;

    private void Awake()
    {
        gunParticlesPool = new ObjectPool<ParticlesBehavior>(OnCreateParticles, OnGetParticles, OnRealeaseParticles);
        _camera = Camera.main;
        playerAmmoSystem = GetComponentInParent<PlayerAmmoSystem>();
    }
    
    public override void OnUse()
    {
        Debug.Log("OnUse");
        if(playerAmmoSystem.CurrentAmmoGun <= 0) return; //Evitamos volver a disparar si no hay balas.
        
        //Disparamos partículas
        gunParticlesPool.Get();
        
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hitInfo, damageDistance))
        {
            if (hitInfo.transform.TryGetComponent(out EnemyBones enemyBone))
            {
                enemyBone.TakeDamage(damageAmount);
            }
        }
        
        playerAmmoSystem.DecreaseGunAmmo(1);
    }
    
    private ParticlesBehavior OnCreateParticles()
    {
        ParticlesBehavior gunShootCopy = Instantiate(gunParticles, spawnPoint.position, Quaternion.identity);
        gunShootCopy.ParticlesPool = gunParticlesPool;
        return gunShootCopy;
    }

    private void OnGetParticles(ParticlesBehavior newParticles)
    {
        //Alinear la posición y rotación cada vez que se usan
        newParticles.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
        
        newParticles.gameObject.SetActive(true);
        newParticles.StartParticlesSystem();
    }

    private void OnRealeaseParticles(ParticlesBehavior particles)
    {
        particles.gameObject.SetActive(false);
    }
}
