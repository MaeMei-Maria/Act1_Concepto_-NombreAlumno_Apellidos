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

        [SerializeField] private EventManagerSO _eventManagerSO;
        public PatrolState PatrolState {get; set;}
        public Transform Target {get; set;}
        public Animator Animator {get; set;}
        public float MaximumSpeed {get; set;}
        public ChaseState ChaseState {get; set;}
        public AttackState MeleeAttackState {get; set;}
        public ShootAttackState ShootAttackState {get; set;}

        public NavMeshAgent Agent {get; set;}
        private float currentHealth;
        private bool dead;
        
        // Este enum te permite elegir el tipo de ataque desde el Inspector
        public enum AttackType { Melee, Ranged }
        [Header("Attack Type")]
        [SerializeField] private AttackType attackType = AttackType.Melee;
        
        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
            Animator = GetComponent<Animator>();
            _ragdollSystem =  GetComponent<RagdollSystem>();
            
            PatrolState = GetComponent<PatrolState>();
            ChaseState = GetComponent<ChaseState>();
            MeleeAttackState = GetComponent<AttackState>();
            ShootAttackState = GetComponent<ShootAttackState>();

            //Le indicamos a los estados quién es el controlador.
            // Inicializamos los estados
            if (PatrolState != null) PatrolState.InitController(this);
            if (ChaseState != null) ChaseState.InitController(this);
            if (MeleeAttackState != null) MeleeAttackState.InitController(this);
            if (ShootAttackState != null) ShootAttackState.InitController(this);
            
            currentHealth = maxHealth;
            SetState(PatrolState);
        }

        public void ChangeToAttackState()
        {
            if (attackType == AttackType.Melee && MeleeAttackState != null)
            {
                SetState(MeleeAttackState);
            }
            else if (attackType == AttackType.Ranged && ShootAttackState != null)
            {
                SetState(ShootAttackState);
            }
        }
        
        public void OnDamage(float damageAmount)
        {
            currentHealth -= damageAmount;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Death();
            }
            
            _eventManagerSO.EnemyNotifiesDamaged(currentHealth, maxHealth);
        }

        private void Death()
        {
            if(dead) return;
            
            //Se deshabilitan los componentes y estados
            PatrolState.enabled = false;
            ChaseState.enabled = false;
            MeleeAttackState.enabled = false;
            ShootAttackState.enabled = false;
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

