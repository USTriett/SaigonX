using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private BulletSpawner _bulletSpawner;

    void Start()
    {
        _bulletSpawner = GetComponentInParent<BulletSpawner>();
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("hit " + other.gameObject.name);

        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyStat enemyStat = other.gameObject.GetComponent<EnemyStat>();
            enemyStat?.LoseHealth(1);
            _bulletSpawner.Enqueue(gameObject);
        }
    }
}
