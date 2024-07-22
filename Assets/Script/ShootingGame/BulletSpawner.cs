using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private float _timeSpawnding;

    [SerializeField]
    private int _numBullet = 10;

    private Queue<GameObject> _bulletPool;
    private float _time;

    private void Start()
    {
        _bulletPool = new Queue<GameObject>();
        for (int i = 0; i < _numBullet; i++)
        {
            Debug.Log(i);
            _bulletPool.Enqueue(Instantiate(_bulletPrefab, transform));
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.time - _time > _timeSpawnding)
        {
            _time = Time.time;
            Shooting();
        }
    }

    private void Shooting()
    {
        if (_bulletPool.Count == 0)
        {
            return;
        }
        GameObject bullet = _bulletPool.Dequeue();
        bullet.SetActive(true);
        bullet.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
        bullet
            .transform.DOMove(transform.forward * 10, 1)
            .OnComplete(() =>
            {
                _bulletPool.Enqueue(bullet);
                bullet.SetActive(false);
            });
    }

    public void Enqueue(GameObject bullet)
    {
        bullet.SetActive(false);

        _bulletPool.Enqueue(bullet);
    }
}
