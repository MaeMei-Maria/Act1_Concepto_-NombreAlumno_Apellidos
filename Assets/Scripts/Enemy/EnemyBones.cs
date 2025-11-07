using System;
using FSM.Enemy;
using UnityEngine;

public class EnemyBones : MonoBehaviour
{
    [SerializeField] private EnemyHealthSystem enemyHealthSystem;
    [SerializeField] private float damageMultiplier;

    private void Awake()
    {
        enemyHealthSystem = GetComponentInParent<EnemyHealthSystem>();
    }

    public void TakeDamage(float damage)
    {
        damage += damageMultiplier;
        enemyHealthSystem.TakeDamage(damage);
    }
}
