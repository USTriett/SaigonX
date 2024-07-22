using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : EnemyController
{
    private static Action DeadEvent;

    protected override void Start()
    {
        base.Start();
    }

    protected void OnEnable()
    {
        AddDieListener(OnDeath);
    }

    protected void OnDisable()
    {
        RemoveDieListener(OnDeath);
    }

    // protected override void OnShot(int damage)
    // {
    //     base.OnShot(damage);
    //     EAnimator.SetTrigger("OnShot");
    // }

    protected void OnDeath()
    {
        EAnimator.SetTrigger("Death");
    }

    protected override void Update()
    {
        base.Update();
        if (transform.position.z <= Camera.main.transform.position.z + 0.5f)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        Vector3 cameraPos = Camera.main.gameObject.transform.position;

        transform.LookAt(cameraPos);
    }

    public static void AddDeathEventListener(Action action)
    {
        DeadEvent += action;
    }

    public static void RemoveDeathEventListener(Action action)
    {
        DeadEvent -= action;
    }

    public void OnDestroyEvent()
    {
        Debug.Log("zombie die");
        GetComponent<Collider>().enabled = false;
        DeadEvent?.Invoke();
        Destroy(gameObject);
    }
}
