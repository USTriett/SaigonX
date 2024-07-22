using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingletonPattern<T>: MonoBehaviour where T:Component
{
    private static T _instance;

    public static T Instance{
        get{
            if(_instance == null){
                GameObject obj = new GameObject
                {
                    name = typeof(T).Name,
                    hideFlags = HideFlags.HideAndDontSave
                };
                _instance = obj.AddComponent<T>();
            }
           
            return _instance;
        }
    }

    private void OnDestroy() {
        if(_instance != this)
        {
            _instance = null;
        }
    }
}

public class SingletonPersitent<T>: MonoBehaviour where T:Component
{
    private static T _instance;
    private float _initTime;

    public static T Instance{
        get{
            if(_instance == null){
                GameObject obj = new GameObject
                {
                    name = typeof(T).Name,
                    hideFlags = HideFlags.HideAndDontSave
                };
                _instance = obj.AddComponent<T>();
            }
           
            return _instance;
        }
    }

    private void OnDestroy() {
        if(_instance != this)
        {
            _instance = null;
        }
    }

    protected virtual void Awake() {
        if(!Application.isPlaying) return;
        _initTime = Time.time;
        DontDestroyOnLoad(gameObject);
        T[] oldInstances = FindObjectsByType<T>(FindObjectsSortMode.None);
        foreach(T old in oldInstances){
            if(old.GetComponent<SingletonPersitent<T>>()._initTime < _initTime){
                Destroy(old.gameObject);
            }
        }
        if(_instance == null){
            _instance = this as T;
        }
        
    }

    
}
