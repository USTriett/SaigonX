using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    [SerializeField]
    private EnemyStatSO enemyStatSO;

    private Dictionary<string, int> stat;
    private bool _isDead = false;

    private void Start()
    {
        InitStat();
    }

    private void InitStat()
    {
        stat = enemyStatSO.GetStat();
    }

    public int GetStat(string statName)
    {
        if (stat.TryGetValue(statName, out int val))
        {
            return val;
        }
        return 0;
    }

    public void LoseHealth(int damage)
    {
        stat["health"] -= damage;
        if (stat["health"] < 0)
        {
            _isDead = true;
        }
    }

    public bool NotifyDeath()
    {
        return _isDead;
    }
}
