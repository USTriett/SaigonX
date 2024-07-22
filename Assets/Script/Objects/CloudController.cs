using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    [SerializeField]
    private float _duration;

    private void Start()
    {
        transform
            .DOLocalRotate(new Vector3(0, 360, 0), _duration, RotateMode.LocalAxisAdd)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);
    }
}
