using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Grenade : MonoBehaviour
{
    public ObjectPool<Grenade> GrenadePool {get; set;}

    [Header("Move/Time")]
    [SerializeField] private float impulseForce = 30f;
    [SerializeField] private float timeToRelease = 5f;
    
    [Header("Damage")]
    [SerializeField] private float damageRadius = 4f;
    [SerializeField] private float damageGenerated = 25f;
    [SerializeField] private float explosionForce = 200f;
    [SerializeField] private LayerMask damageLayerMask;
    [SerializeField] private bool obstaclesInMiddle = true; //Si hay un obstáculo delante de, por ejemplo, un enemigo, no le hace daño.

    private Rigidbody _rigidbody;
    private bool hasExploded = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Launch(Vector3 direction)
    {
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.AddForce(direction.normalized * impulseForce, ForceMode.Impulse);
        StartCoroutine(WaitAndRelease());
    }

    private IEnumerator WaitAndRelease()
    {
        yield return new WaitForSeconds(timeToRelease);
        Explode();
        GrenadePool.Release(this);
    }

    private void Explode()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, damageRadius);
        foreach (Collider hit in hits)
        {
            // buscamos un EnemyBones cercano
            EnemyBones enemy = hit.GetComponentInParent<EnemyBones>();
            if (enemy != null)
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                float damage = Mathf.Clamp01(1 - (distance / damageRadius)) * damageGenerated;
                enemy.TakeDamage(damage);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}
