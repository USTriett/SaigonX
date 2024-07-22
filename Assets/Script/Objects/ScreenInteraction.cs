using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class ScreenInteraction : MonoBehaviour
{
    [SerializeField]
    private LandController _landController;

    private InputManager _inputManager;
    private GameObject _objectDectectHolder;

    private float _startTouchTime;

    private Vector2 _startPositionTouch;

    // Start is called before the first frame update

    void OnEnable()
    {
        // TouchSimulation.Enable();
        _inputManager = InputManager.Instance;
        // Debug.Log(_inputManager.gameObject.name);
        _inputManager.AddStartTouchAction(DetectGameObject);
        _inputManager.AddEndTouchAction(DetectSwipe);
        // _inputManager.AddEndTouchAction(Swipe);
    }

    private void DetectSwipe(Vector3 worldPos, Vector2 screenPos, float time)
    {
        Debug.Log(screenPos - _startPositionTouch);
        _landController.LandRotate(screenPos - _startPositionTouch);
    }

    private void Swipe(float swipeDirection)
    {
        Debug.Log("Do:" + swipeDirection);

        _landController.LandRotate(new Vector2(0, swipeDirection));
    }

    void OnDisable()
    {
        _inputManager.RemoveStartTouchAction(DetectGameObject);
        _inputManager.RemoveEndTouchAction(DetectSwipe);
    }

    private void DetectGameObject(Vector3 location, Vector2 screenPos, float time)
    {
        // RaycastHit hit;
        // if (Physics.Raycast(location, _camera.transform.forward, out hit, Mathf.Infinity))
        // {
        //     _objectDectectHolder = hit.transform.gameObject;
        // }
        // Debug.Log(screenPos);

        _startTouchTime = time;
        // Debug.Log("StartTime: " + time);

        _startPositionTouch = screenPos;
    }

    // Update is called once per frame
    void Update() { }
}
