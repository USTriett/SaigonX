using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;

public class TankController : MonoBehaviour
{
    private TankInputHandler _tankInputHandler;
    private bool _isTouch = false;
    private Vector3 _startPos;
    private Vector3 _startScreenPos;
    private Rigidbody _rigidbody;
    private Vector3 _predictionDir;
    private bool _isMove;

    [SerializeField]
    private float _jumpForce = 1f;

    [SerializeField]
    private GameObject _direction;

    [SerializeField]
    private GameObject _mesh;

    [SerializeField]
    private ARRaycastManager _rayCastManager;
    private Animator _animator;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        if (!TryGetComponent<TankInputHandler>(out _tankInputHandler))
        {
            _tankInputHandler = gameObject.AddComponent<TankInputHandler>();
        }
        _tankInputHandler.AddOnStartTouchListener(CheckTouchTank);
        _tankInputHandler.AddOnStopTouchListener(ShootTank);
        _direction = FindGameObjectInChildren("Direction");
        _mesh = FindGameObjectInChildren("Mesh");
    }

    private GameObject FindGameObjectInChildren(string v)
    {
        int num = gameObject.transform.childCount;
        for (int i = 0; i < num; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            if (child.name == v)
            {
                return child;
            }
        }
        return null;
    }

    private void OnDestroy()
    {
        _tankInputHandler.RemoveOnStartTouchListener(CheckTouchTank);
        _tankInputHandler.RemoveOnStopTouchListener(ShootTank);
    }

    private void ShootTank(InputAction.CallbackContext context)
    {
        if (_isMove)
        {
            return;
        }
        if (_isTouch)
        {
            _direction.transform.DOScaleZ(0, 0f);
            Vector3 cancledPos = Helper.ConvertScreenToWorld(Camera.main, _tankInputHandler.curPos);
            Vector3 dir = _startScreenPos - _tankInputHandler.curPos;
            float distance =
                Mathf.Min(Vector3.Distance(_startPos, _tankInputHandler.curPos), 2.5f) / 2.5f;
            dir.Normalize();
            dir.z = dir.y;
            dir.y = 0;
            Debug.Log("Distance: " + distance);
            _rigidbody.AddForce(_jumpForce * distance * dir, ForceMode.Impulse);

            _isMove = true;
            _animator.SetTrigger("Move");
            StartCoroutine(MovingHandle());
            _isTouch = false;
        }
    }

    private IEnumerator MovingHandle()
    {
        while (_rigidbody.velocity.sqrMagnitude != 0)
        {
            Debug.Log("Moving");
            yield return null;
        }
        _animator.SetBool("Stop", true);
        _isMove = false;
    }

    private void CheckTouchTank(InputAction.CallbackContext context)
    {
        if (_isTouch || _isMove)
            return;
        if (
            Physics.Raycast(
                Camera.main.ScreenPointToRay(_tankInputHandler.curPos),
                out RaycastHit hit
            )
        )
        {
            if (hit.collider.gameObject.CompareTag("Tank"))
            {
                _isTouch = true;
                _startPos = hit.point;
                _startScreenPos = _tankInputHandler.curPos;
            }
            else
            {
                _isTouch = false;
            }
        }
    }

    private void Update()
    {
        if (_isTouch)
        {
            Vector3 touchPos = Helper.ConvertScreenToWorld(Camera.main, _tankInputHandler.curPos);
            float distance =
                Mathf.Min(Vector3.Distance(_startPos, _tankInputHandler.curPos), 2.5f) / 2.5f;
            _predictionDir = _startScreenPos - _tankInputHandler.curPos;
            _predictionDir.Normalize();
            _predictionDir.z = _predictionDir.y;
            _predictionDir.y = 0;

            _direction.transform.DOScaleZ(distance, 0f);
            Vector3 dir = new Vector3(
                0,
                Vector3.SignedAngle(Vector3.forward, _predictionDir, Vector3.up),
                0
            );
            _direction.transform.DOLocalRotate(dir, 0f);
            _mesh.transform.DOLocalRotate(dir, 0f);
        }
    }

    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;

    //     if (_isTouch)
    //     {
    //         Vector3 dir = Helper.ConvertScreenToWorld(Camera.main, _tankInputHandler.curPos);
    //         dir.y = _startPos.y;
    //         Gizmos.DrawLine(_startPos, dir - _startPos);
    //     }
    // }
}
