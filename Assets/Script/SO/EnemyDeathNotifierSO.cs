using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "EnemyDeathNotifierSO",
    menuName = "ScriptableObjects/DeathNotifier",
    order = 0
)]
public class EnemyDeathNotifierSO : ScriptableObject
{
    private Action _action;

    public void Notify()
    {
        _action?.Invoke();
    }

    public void AddListener(Action action)
    {
        _action += action;
    }

    public void RemoveListener(Action action)
    {
        _action -= action;
    }
}
