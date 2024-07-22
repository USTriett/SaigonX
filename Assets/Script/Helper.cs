using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper
{
    public static Vector3 ConvertScreenToWorld(Camera camera, Vector3 screenPos)
    {
        screenPos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(screenPos);
    }
}
