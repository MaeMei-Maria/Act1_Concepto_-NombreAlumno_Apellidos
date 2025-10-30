using System;
using UnityEngine;
//using FSM.Plantillas;

namespace FSM.Enemy
{
    public class EnemyController : FSMController
    {
        public PatrolState PatrolState {get; set;}
        public ChaseState ChaseState {get; set;}
        public AttackState AttackState {get; set;}

        private void Awake()
        {
            PatrolState = GetComponent<PatrolState>();
            ChaseState = GetComponent<ChaseState>();
            AttackState = GetComponent<AttackState>();
        
            //Le indicamos a los estados quién es el controlador.
            PatrolState.InitController(this);
            ChaseState.InitController(this);
            AttackState.InitController(this);

        }
    }
}

