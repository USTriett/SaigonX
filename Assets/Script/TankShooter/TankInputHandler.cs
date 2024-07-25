using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankInputHandler : MonoBehaviour
{
    private TankInput _input;

    private void OnEnable()
    {
        _input = new TankInput();
        _input.Enable();
        _input.HoldScreen.End.performed += ctx =>
        {
            curPos = ctx.ReadValue<Vector2>();
            // Debug.Log(curPos);
        };
    }

    public bool IsTouch = false;
    public Vector3 curPos;

    private void Start() { }

    public void AddOnStopTouchListener(Action<InputAction.CallbackContext> action)
    {
        _input.HoldScreen.Start.canceled += ctx => action.Invoke(ctx);
    }

    public void AddOnStartTouchListener(Action<InputAction.CallbackContext> action)
    {
        _input.HoldScreen.Start.started += ctx => action.Invoke(ctx);
    }

    public void RemoveOnStartTouchListener(Action<InputAction.CallbackContext> action)
    {
        // _input.HoldScreen.Start.performed -= action;
    }

    public void RemoveOnStopTouchListener(Action<InputAction.CallbackContext> action)
    {
        // _input.HoldScreen.End.canceled -= action;
    }
}
