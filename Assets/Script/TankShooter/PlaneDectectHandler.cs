using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaneDectectHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _game;

    // Start is called before the first frame update
    private ARPlane _holder;
    private ARPlaneManager _manager;

    void Start()
    {
        _manager = GetComponent<ARPlaneManager>();
        _manager.planesChanged += OnPlanesChanged;
    }

    private void OnDestroy()
    {
        _manager.planesChanged -= OnPlanesChanged;
    }

    private void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        if (_holder == null && args.added.Count > 0)
        {
            // Đặt gameObject tại vị trí của plane đầu tiên được phát hiện
            _holder = args.added[0];
            Pose pose = new Pose(_holder.transform.position, _holder.transform.rotation);
            _game.transform.position = pose.position;
            _game.transform.rotation = pose.rotation;
            _game.SetActive(true);
            _manager.enabled = false;
        }
    }
}
