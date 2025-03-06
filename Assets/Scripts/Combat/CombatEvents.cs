using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEvents : MonoBehaviour
{
    public delegate void EnemyEventHandler(EnemyStats enemyStats);
    public static event EnemyEventHandler OnEnemyDeath;

    public static void EnemyDied(EnemyStats enemyStats)
    {
        OnEnemyDeath?.Invoke(enemyStats);
    }
}
