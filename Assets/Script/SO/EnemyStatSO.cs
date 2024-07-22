using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatSO", menuName = "ScriptableObjects/EnemyStat", order = 0)]
[Serializable]
public class EnemyStatSO : ScriptableObject
{
    [SerializeField]
    private int health;

    [SerializeField]
    private int speed;

    [SerializeField]
    private int attack;

    private Dictionary<string, int> stat = new Dictionary<string, int>();

    public Dictionary<string, int> GetStat()
    {
        InitStat();
        return stat.ToDictionary(entry => entry.Key, entry => entry.Value);
    }

    private void InitStat()
    {
        stat.Add("health", health);
        stat.Add("speed", speed);
        stat.Add("attack", attack);
    }
}
