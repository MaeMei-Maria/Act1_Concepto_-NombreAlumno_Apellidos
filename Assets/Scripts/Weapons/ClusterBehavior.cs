using System;
using UnityEngine;
using UnityEngine.Pool;

public class ClusterBehavior : Weapon
{
    [SerializeField] private Grenade grenadePrefab;
    [SerializeField] private Transform spawnPoint;
    
    private ObjectPool<Grenade> grenadePool;

    private void Awake()
    {
        grenadePool = new ObjectPool<Grenade>(OnCreateGrenade, OnGetGrenade, OnReleaseGrenade);
    }

    private Grenade OnCreateGrenade() //Genera nuevas granadas cuando no hay disponibles.
    {
        Grenade grenadeCopy = Instantiate(grenadePrefab, spawnPoint.position, Quaternion.identity);
        grenadeCopy.GrenadePool = grenadePool;
        return grenadeCopy;
    }

    private void OnGetGrenade(Grenade newGrenade) //Para activar alguna de las granadas disponibles.
    {
        newGrenade.GetComponent<Rigidbody>().linearVelocity = Vector3.zero; //Se detiene la fuerza por la que es propulsada.
        newGrenade.transform.position = spawnPoint.position;
        newGrenade.transform.rotation = spawnPoint.rotation;
        newGrenade.gameObject.SetActive(true);
    }
    
    private void OnReleaseGrenade(Grenade grenadeToRelease) //Para desactivar las granadas
    {
        grenadeToRelease.gameObject.SetActive(false);
    }

    public override void OnUse()
    {
        grenadePool.Get();
    }
}
