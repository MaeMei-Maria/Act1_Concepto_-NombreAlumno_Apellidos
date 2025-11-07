using System;
using System.Collections;
using System.Collections.Generic;
using FSM.Enemy;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : States<EnemyController>
{
    [SerializeField] private Transform patrolPath;
    [SerializeField] private float patrolSpeed = 3.5f;

    [SerializeField] private float waitTime = 1f;

    private List<Vector3> patrolPoints = new ();
    
    private int currentPatrolIndex;
    
    private static readonly int Speed = Animator.StringToHash("speed");

    public override void InitController(EnemyController controller)
    {
        base.InitController(controller);
        
        //Se inicializan los puntos de patrullaje.
        foreach (Transform points in patrolPath)
        {
            patrolPoints.Add(points.position);
        }
    }

    private void OnEnable()
    {
        _controller.Sensor.OnPlayerDetected += StopPatrol;
    }

    private void StopPatrol(Transform newTarget)
    {
        StopAllCoroutines();
        _controller.Agent.isStopped = true;
        _controller.SetState(_controller.ChaseState);
    }

    public override void OnEnter()
    {
        _controller.Agent.stoppingDistance = 0f;
        _controller.Agent.speed = patrolSpeed;
        _controller.Agent.isStopped = false;
        StartCoroutine(PatrolAndWait());
    }

    private IEnumerator PatrolAndWait()
    {
        while (true)
        {
            //Nos aseguramos de que el agente esté activo o sobre el navmesh para evitar errores.
            if(_controller.Agent == null || !_controller.Agent.isOnNavMesh) yield break; 
            
            _controller.Agent.SetDestination(patrolPoints[currentPatrolIndex]);
            yield return new WaitUntil(() => !_controller.Agent.pathPending && _controller.Agent.remainingDistance <= _controller.Agent.stoppingDistance);
            yield return new WaitForSeconds(waitTime);
            
            CalculateNewPoint();
        }
    }

    private void CalculateNewPoint()
    {
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
    }

    public override void OnUpdate()
    {
        _controller.Animator.SetFloat(Speed, _controller.Agent.velocity.magnitude/_controller.MaximumSpeed);
    }

    public override void OnExit()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        StopAllCoroutines(); // asegura que ninguna corrutina siga viva

        _controller.Sensor.OnPlayerDetected -= StopPatrol;
    }
}
