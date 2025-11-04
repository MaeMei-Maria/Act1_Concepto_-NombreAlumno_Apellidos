using System;
using UnityEngine;
using UnityEngine.AI;

namespace FSM.Enemy
{
    public class EnemyController : FSMController<EnemyController>
    {        
        [field: SerializeField] public SensorSystem Sensor {get; set;}
        [SerializeField] private RagdollSystem _ragdollSystem;

        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private float timeToDestroy = 4.5f;

        public PatrolState PatrolState {get; set;}
        public Transform Target {get; set;}
        public Animator Animator {get; set;}
        public float MaximumSpeed {get; set;}
        public ChaseState ChaseState {get; set;}
        public AttackState AttackState {get; set;}
        public NavMeshAgent Agent {get; set;}
        private float currentHealth;
        private bool dead;
        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
            Animator = GetComponent<Animator>();
            _ragdollSystem =  GetComponent<RagdollSystem>();
            
            PatrolState = GetComponent<PatrolState>();
            ChaseState = GetComponent<ChaseState>();
            AttackState = GetComponent<AttackState>();

            //Le indicamos a los estados quién es el controlador.
            PatrolState.InitController(this);
            ChaseState.InitController(this);
            AttackState.InitController(this);
            
            currentHealth = maxHealth;
            SetState(PatrolState);
        }

        public void OnDamage(float damageAmount)
        {
            currentHealth -= damageAmount;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Death();
            }
        }

        private void Death()
        {
            if(dead) return;
            
            //Se deshabilitan los componentes y estados
            PatrolState.enabled = false;
            ChaseState.enabled = false;
            AttackState.enabled = false;
            Animator.enabled = false;
            Agent.enabled = false;
            this.enabled = false;
            
            //Cambia el estado de los huesos
            _ragdollSystem.UpdateBonesState(false);
            
            Destroy(gameObject, timeToDestroy);
            
            dead = true;
        }
    }
}

