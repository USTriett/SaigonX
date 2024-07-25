using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GateSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _gatePrefab;

    private static Queue<GameObject> _gatePool;

    [SerializeField]
    public static int _numGates = 3;

    public static int NumDisabledGate = 0;

    // Start is called before the first frame update
    private void Start()
    {
        InitPool();
        SpawnGates();
    }

    private void SpawnGates()
    {
        NumDisabledGate = 0;
        for (int i = 0; i < _numGates; i++)
        {
            Spawn();
        }
    }

    private void InitPool()
    {
        _gatePool = new Queue<GameObject>();
        for (int i = 0; i < _numGates; i++)
        {
            GameObject gate = Instantiate(_gatePrefab, transform);
            gate.SetActive(false);
            _gatePool.Enqueue(gate);
        }
    }

    public GameObject Spawn()
    {
        int xPos = Random.Range(-10, 10);
        int zPos = Random.Range(-10, 10);
        GameObject gate = _gatePool.Dequeue();
        gate.transform.localPosition = new Vector3(xPos, 0, zPos);
        gate.SetActive(true);
        return gate;
    }

    public static void Collapse(GameObject gate)
    {
        gate.SetActive(false);
        _gatePool.Enqueue(gate);
    }

    // Update is called once per frame
    void Update()
    {
        if (NumDisabledGate == _numGates)
        {
            SpawnGates();
        }
    }
}
