using DG.Tweening;
using FSM.Enemy;
using UnityEngine;

public class AttackState : States<EnemyController>
{
    private static readonly int Attacking = Animator.StringToHash("attacking");

    [SerializeField] private float attackDistance = 2.5f;
    [SerializeField] private float smoothGaze = 2f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 1f;
    [SerializeField] private int damage = 15;
    public override void OnEnter()
    {
        
    }

    public override void OnUpdate()
    {
        _controller.Agent.isStopped = true;
        _controller.Animator.SetBool(Attacking, true);
        
        transform.DOLookAt(_controller.Target.transform.position, smoothGaze, AxisConstraint.Y);
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

    private void OnAttack()
    {
        Collider[] colliders = Physics.OverlapSphere(attackPoint.position, attackRadius);

        foreach (Collider coll in colliders)
        {
            if(coll.TryGetComponent(out PlayerHealthSystem playerHealth))
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
    
    public override void OnExit()
    {
        
    }
}
