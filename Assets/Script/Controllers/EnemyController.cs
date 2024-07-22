using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    private Animator _animator;
    private EnemyStat _enemyStat;

    private Action onDieAction;
    private bool _isDead = false;

    protected virtual void Start()
    {
        _animator = GetComponent<Animator>();
        _enemyStat = GetComponent<EnemyStat>();
    }

    protected void AddDieListener(Action action)
    {
        onDieAction += action;
    }

    protected void RemoveDieListener(Action action)
    {
        onDieAction -= action;
    }

    public Animator EAnimator => _animator;

    protected virtual void Move() { }

    protected virtual void Attack() { }

    protected virtual void OnShot(int damage)
    {
        _enemyStat.LoseHealth(damage);
    }

    protected virtual void Update()
    {
        if (_enemyStat.NotifyDeath() && !_isDead)
        {
            Debug.Log("Die");
            onDieAction?.Invoke();
            _isDead = true;
        }
    }
}
