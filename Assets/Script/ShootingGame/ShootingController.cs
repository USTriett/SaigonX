using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class ShootingController : MonoBehaviour
{
    [SerializeField]
    private float _maxDistance;
    private ShootingControl _shootingControl;

    [SerializeField]
    [Range(1, 2)]
    private float _smooth;

    // ShootingGameInputManager inputManager;
    void Awake()
    {
        _shootingControl = new ShootingControl();
    }

    private void Start()
    {
        // _inputManager = InputManager.Instance;
    }

    void OnEnable()
    {
        _shootingControl.Enable();
        _shootingControl.Dragging.Look.performed += OnDragging;
    }

    void OnDisable()
    {
        _shootingControl.Dragging.Look.performed -= OnDragging;
        _shootingControl.Disable();
    }

    // Update is called once per frame


    public void OnDragging(CallbackContext callbackContext)
    {
        Vector3 screenPosition = callbackContext.ReadValue<Vector2>();
        screenPosition.y = screenPosition.x;
        screenPosition.x = 0;
        transform.DORotate(transform.rotation.eulerAngles + screenPosition * _smooth, 0);
    }
}
