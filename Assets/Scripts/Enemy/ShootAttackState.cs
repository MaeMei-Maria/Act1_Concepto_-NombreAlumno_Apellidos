using DG.Tweening;
using FSM.Enemy;
using UnityEngine;
using UnityEngine.Pool;

public class ShootAttackState : States<EnemyController>
{
    private static readonly int Attacking = Animator.StringToHash("attacking");

    [Header("Attack Settings")]
    [SerializeField] private float attackDistance = 10f;
    [SerializeField] private float smoothGaze = 2f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject gunGameObject;
    [SerializeField] private EnemyBullet bulletPrefab;

    private ObjectPool<EnemyBullet> bulletPool;

    public override void OnEnter()
    {
        gunGameObject.SetActive(true);
        _controller.Agent.isStopped = true;
        _controller.Animator.SetBool(Attacking, true);

        bulletPool ??= new ObjectPool<EnemyBullet>(OnCreateBullet, OnGetBullet, OnReleaseBullet);
    }

    public override void OnUpdate()
    {
        transform.DOLookAt(_controller.Target.transform.position, smoothGaze, AxisConstraint.Y);
    }
    
    private EnemyBullet OnCreateBullet()
    {
        EnemyBullet bullet = Instantiate(bulletPrefab);
        bullet.BulletPool = bulletPool;
        return bullet;
    }

    private void OnGetBullet(EnemyBullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnReleaseBullet(EnemyBullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }
    
    private void OnShootAttack()
    {
        var bullet = bulletPool.Get();
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;
        bullet.Shoot(firePoint.forward);
    }

    private void ShootAttackFinished() //Se llama mediante un evento de animación y comprueba la distancia al finalizar el ataque
    {
        // Comprobar si el jugador está fuera del rango
        float distance = Vector3.Distance(_controller.transform.position, _controller.Target.position);
        if (distance > attackDistance)
        {
            gunGameObject.SetActive(false);
            _controller.SetState(_controller.ChaseState);
        }
    }
    
    public override void OnExit()
    {
        _controller.Animator.SetBool(Attacking, false);
        gunGameObject.SetActive(false);
    }
}
