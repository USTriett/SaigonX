using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class InputManager : SingletonPersitent<InputManager>
{
    // Start is called before the first frame update
    private PlayerInput _playerInput;

    private Action<Vector3, Vector2, float> _onStartTouch;

    private Action<Vector3, Vector2, float> _onEndTouch;
    private Action<float> _onSwipe;
    private Camera _mainCamera;

    private Vector2 _swipeDirection = Vector2.zero;

    public void AddStartTouchAction(Action<Vector3, Vector2, float> action)
    {
        _onStartTouch += action;
    }

    public void RemoveStartTouchAction(Action<Vector3, Vector2, float> action)
    {
        _onStartTouch -= action;
    }

    public void AddEndTouchAction(Action<Vector3, Vector2, float> action)
    {
        _onEndTouch += action;
    }

    public void RemoveEndTouchAction(Action<Vector3, Vector2, float> action)
    {
        _onEndTouch -= action;
    }

    private void Start()
    {
        _mainCamera = Camera.main;
        _playerInput.Touch.Touch.started += ctx => StartTouchPrimary(ctx);
        _playerInput.Touch.Touch.canceled += ctx => EndTouchPrimary(ctx);
    }

    private void EndTouchPrimary(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        Vector2 touchPosition = _playerInput.Touch.Position.ReadValue<Vector2>();
        Debug.Log("end: " + touchPosition);
        _onEndTouch?.Invoke(touchPosition, touchPosition, (float)ctx.time);
    }

    private void StartTouchPrimary(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        Vector2 touchPosition = _playerInput.Touch.Position.ReadValue<Vector2>();

        Debug.Log("start: " + touchPosition);

        _onStartTouch?.Invoke(touchPosition, touchPosition, (float)ctx.time);
    }

    public void AddSwipeListener(Action<float> action)
    {
        _onSwipe += action;
    }

    public void RemoveSwipeListener(Action<float> action)
    {
        _onSwipe -= action;
    }

    void OnEnable()
    {
        _playerInput = new PlayerInput();

        _playerInput.Enable();
        // TouchSimulation.Enable();
    }

    void OnDisable()
    {
        _playerInput.Disable();
        TouchSimulation.Disable();
    }
}
