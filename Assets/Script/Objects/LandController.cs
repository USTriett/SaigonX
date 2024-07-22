using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LandController : MonoBehaviour
{
    // Update is called once per frame

    private bool _onPerforming;

    public void LandRotate(Vector2 direction)
    {
        if (_onPerforming || Mathf.Abs(direction.x) <= 0.01)
        {
            return;
        }

        transform
            .DOLocalRotate(
                new Vector3(0, -90 * direction.x / Mathf.Abs(direction.x), 0),
                0.5f,
                RotateMode.LocalAxisAdd
            )
            .SetEase(Ease.InCubic)
            .OnComplete(() => _onPerforming = false);
        _onPerforming = true;
    }
}
