using System;
using FSM.Enemy;
using UnityEngine;

public class EnemyBones : MonoBehaviour
{
    [SerializeField] private EnemyController _enemyController;
    [SerializeField] private float damageMultiplier;

    private void Awake()
    {
        _enemyController = GetComponentInParent<EnemyController>();
    }

    public void TakeDamage(float damage)
    {
        damage += damageMultiplier;
        _enemyController.OnDamage(damage);
    }
}
