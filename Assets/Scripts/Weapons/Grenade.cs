using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Grenade : MonoBehaviour
{
    public ObjectPool<Grenade> GrenadePool {get; set;}

    [SerializeField] private float impulseForce = 30f;
    [SerializeField] private float timeToRelease = 5f;
    [SerializeField] private float damageRadius = 4f;
    [SerializeField] private float damageGenerated = 25f;

    private void OnEnable()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * impulseForce, ForceMode.Impulse);
        StartCoroutine(WaitAndRelease());
    }

    private IEnumerator WaitAndRelease()
    {
        yield return new WaitForSeconds(timeToRelease);
        GrenadePool.Release(this);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        /*if (other.CompareTag("Obstacle"))
        {
           
        }*/
    }
}
