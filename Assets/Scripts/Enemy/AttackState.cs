using FSM.Enemy;
using UnityEngine;

public class AttackState : States<EnemyController>
{
    private static readonly int Attacking = Animator.StringToHash("attacking");

    [SerializeField] private float attackDistance = 2.5f;
    public override void OnEnter()
    {
        
    }

    public override void OnUpdate()
    {
        _controller.Agent.isStopped = true;
        _controller.Animator.SetBool(Attacking, true);
        
        //Rotamos al enemigo para que vea hacia el player cuando lo esté atacando.
        Vector3 directionToTarget = (_controller.Target.position - transform.position).normalized;
        directionToTarget.y = 0; //Evitamos que se vuelque.
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = lookRotation;
    }

    private void AttackFinished() //Se llama mediante un evento de animación y comprueba la distancia al finalizar el ataque
    {
        if (Vector3.Distance(transform.position, _controller.Target.position) > attackDistance)
        {
            _controller.Animator.SetBool(Attacking, false);
            _controller.Agent.isStopped = false;
            _controller.SetState(_controller.ChaseState);
        }
    }
    
    public override void OnExit()
    {
        
    }
}
