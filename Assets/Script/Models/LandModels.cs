using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class LandModels : MonoBehaviour
{
    public static async Task<List<DTOLand>> FetchAllLands(string uri)
    {
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(uri);
        await Task.Yield();
        unityWebRequest.SendWebRequest();
        if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(unityWebRequest.error);
            return new List<DTOLand>();
        }
        else
        {
            return ParseJson<List<DTOLand>>(unityWebRequest.downloadHandler.text);
        }
    }

    private static T ParseJson<T>(string text)
    {
        try
        {
            var result = JsonUtility.FromJson<T>(text);
            return result;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        return default;
    }
}
