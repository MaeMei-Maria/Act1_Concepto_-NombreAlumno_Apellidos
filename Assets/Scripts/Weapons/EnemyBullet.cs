using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyBullet : MonoBehaviour
{
    public ObjectPool<EnemyBullet> BulletPool {get; set;}

    [Header("Movement")] 
    [SerializeField] private float bulletSpeed = 100f;
    [SerializeField] private float bulletLifeTime = 3f;
    
    [Header("Damage")]
    [SerializeField] private float bulletDamage = 20f;
    
    private Rigidbody rigidbodyBullet;
    private bool isActive;

    private void Awake()
    {
        rigidbodyBullet = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        isActive = true;
        StartCoroutine(ReleaseAfterTime());
    }
    
    public void Shoot(Vector3 direction)
    {
        rigidbodyBullet.linearVelocity = Vector3.zero;
        rigidbodyBullet.angularVelocity = Vector3.zero;

        rigidbodyBullet.AddForce(direction.normalized * bulletSpeed, ForceMode.VelocityChange);
    }

    private IEnumerator ReleaseAfterTime()
    {
        yield return new WaitForSeconds(bulletLifeTime);

        if (isActive)
        {
            BulletPool.Release(this);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        isActive = false;

        if (other.transform.TryGetComponent(out PlayerHealthSystem playerHealthSystem))
        {
            playerHealthSystem.TakeDamage(bulletDamage);
        }
        
        BulletPool.Release(this);
    }
}
