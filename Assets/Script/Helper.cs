using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Helper : MonoBehaviour
{
    public static Vector3 ConvertScreenToWorld(Camera camera, Vector3 screenPos)
    {
        screenPos.z = camera.transform.position.y;
        return camera.ScreenToWorldPoint(screenPos);
    }

    public static void Reload(string name)
    {
        SceneManager.LoadScene(name);
    }
}
