using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject[] _mapPrefabs;

    // private List<DTOMap> _maps;

    private GameObject[] _mapObject;

    private int _numberOfMap;

    // async void Awake()
    // {
    //     _Maps = await MapModels.FetchAllMaps("abc");
    // }

    private void Start()
    {
        // _numberOfMap = Math.Min(_mapPrefabs.Length, _maps.Count);
        _numberOfMap = _mapPrefabs.Length;
        SpawnMaps();
    }

    private void SpawnMaps()
    {
        for (int i = 0; i < _numberOfMap; i++)
        {
            _mapObject[i] = Instantiate(_mapPrefabs[i], transform, true);
            // _mapObject[i].name = _maps[i].Name;
        }
    }

    // Update is called once per frame
    void Update() { }
}
