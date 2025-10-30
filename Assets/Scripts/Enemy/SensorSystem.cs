using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SensorSystem : MonoBehaviour
{
    [SerializeField] private float sensorDistance = 9f;
    [SerializeField] private float sensorAngle = 65f;
    [SerializeField] private LayerMask obstaclesMask;
    
    public event Action OnPlayerDetected, OnPlayerLost;
    
    private SphereCollider sensorCollider;

    private void Awake()
    {
        sensorCollider = GetComponent<SphereCollider>();
        sensorCollider.radius = sensorDistance;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckDetection(other);
        }
    }

    private void CheckDetection(Collider other)
    {
        Vector3 directionToTarget = other.transform.position - transform.position;
        
        //Si hay un obstáculo en medio no es valida la detección.
        if(Physics.Raycast(transform.position, directionToTarget, sensorDistance, obstaclesMask)) return;
        
        //Revisa el ángulo de visión
        if (Vector3.Angle(directionToTarget, transform.forward) < sensorAngle / 2f)
        {
            Debug.Log("Player en rango");
            OnPlayerDetected?.Invoke();
        }
        else  OnPlayerLost?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerLost?.Invoke();
        }
    }
}
