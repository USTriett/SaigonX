using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CircularLoading : MonoBehaviour
{
    // Update is called once per frame
    private void OnEnable()
    {
        Loading();
    }

    private void Loading()
    {
        transform
            .DORotate(new Vector3(0, 0, 360), 1f, RotateMode.LocalAxisAdd)
            .SetEase(Ease.InOutQuint)
            .SetLoops(-1);
    }
}
