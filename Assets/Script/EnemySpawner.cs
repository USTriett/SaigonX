using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Random = System.Random;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private ARPlaneManager _aRPlaneManager;

    [SerializeField]
    private ARRaycastManager _aRRaycastManager;

    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private float _spawnTime = 1f;

    [SerializeField]
    private float _minDistanceFromPlayer = 5f;

    List<ARRaycastHit> _hits = new List<ARRaycastHit>();
    private bool planeDetected = false;
    private HashSet<ARPlane> _planeHolder = new HashSet<ARPlane>();

    private Vector2 _startRayPoint = new Vector2(Screen.width / 2, Screen.height / 2.5f);

    private List<GameObject> _enemies = new();
    private float _lastSpawnTime;

    private void OnEnable() { }

    private void Update()
    {
        if (_aRRaycastManager.Raycast(_startRayPoint, _hits))
        {
            foreach (var h in _hits)
            {
                if (h.trackable is ARPlane)
                {
                    _planeHolder.Add(_aRPlaneManager.GetPlane(h.trackableId));
                    planeDetected = true;
                }
            }
        }

        if (planeDetected && CanSpawning())
        {
            _lastSpawnTime = Time.time;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector3 pos = GetRandomPlane();
        pos.z += _minDistanceFromPlayer;
        GameObject newEnemy = Instantiate(_enemyPrefab, pos, Quaternion.identity);
        _enemies.Add(newEnemy);
    }

    private Vector3 GetRandomPlane()
    {
        Random rand = new Random();
        int index = rand.Next(_planeHolder.Count);
        return _planeHolder.ElementAt(index).center;
    }

    private bool CanSpawning()
    {
        return Time.time - _lastSpawnTime >= _spawnTime;
    }
}
