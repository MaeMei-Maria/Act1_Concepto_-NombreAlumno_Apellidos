using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : States
{
    [SerializeField] private Transform[] patrolPath;
    [SerializeField] private SensorSystem sensor;
    [SerializeField] private float waitTime = 2f;

    private List<Vector3> patrolPoints = new();
    
    private NavMeshAgent agent;
    private int currentPatrolIndex;

    public override void InitController(FSMController controller)
    {
        base.InitController(controller);
        
        //Se inicializan los puntos de patrullaje.
        foreach (Transform points in patrolPath)
        {
            patrolPoints.Add(points.position);
        }
        
        agent = GetComponent<NavMeshAgent>();
    }

    public override void OnEnter()
    {
        StartCoroutine(PatrolAndWait());
    }

    private IEnumerator PatrolAndWait()
    {
        while (true)
        {
            agent.SetDestination(patrolPoints[currentPatrolIndex]);
            yield return new WaitUntil(() => !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance);
            yield return new WaitForSeconds(waitTime);
            
            CalculateNewPoint();
        }
    }

    private void CalculateNewPoint()
    {
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPath.Length;
    }

    public override void OnUpdate()
    {
        throw new System.NotImplementedException();
    }

    public override void OnExit()
    {
        throw new System.NotImplementedException();
    }
}
