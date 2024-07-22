using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventSO", menuName = "ScriptableObjects/EventSO", order = 0)]
public class EventSO : ScriptableObject
{
    private Action _action;

    public void Raise()
    {
        _action?.Invoke();
    }

    public void Register(Action action)
    {
        _action += action;
    }

    public void Unregister(Action action)
    {
        _action -= action;
    }
}
