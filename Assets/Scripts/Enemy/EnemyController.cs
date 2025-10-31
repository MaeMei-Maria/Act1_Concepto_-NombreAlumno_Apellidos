using System;
using UnityEngine;
using UnityEngine.AI;

namespace FSM.Enemy
{
    public class EnemyController : FSMController<EnemyController>
    {        
        [field: SerializeField] public SensorSystem Sensor {get; set;}
        public PatrolState PatrolState {get; set;}
        public Transform Target {get; set;}
        public Animator Animator {get; set;}
        public float MaximumSpeed {get; set;}
        public ChaseState ChaseState {get; set;}
        public AttackState AttackState {get; set;}
        public NavMeshAgent Agent {get; set;}

        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
            Animator = GetComponent<Animator>();

            PatrolState = GetComponent<PatrolState>();
            ChaseState = GetComponent<ChaseState>();
            AttackState = GetComponent<AttackState>();

            //Le indicamos a los estados quién es el controlador.
            PatrolState.InitController(this);
            ChaseState.InitController(this);
            AttackState.InitController(this);
            
            SetState(PatrolState);
        }
    }
}

