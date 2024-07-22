using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBus : SingletonPattern<EventBus>
{
    private static List<BaseController> _controllers;

    public static void Execute(string eventName)
    {
        var eventSO = FindEventSO(eventName);
        if (eventSO != null)
        {
            eventSO.Raise();
        }
    }

    private static EventSO FindEventSO(string eventName)
    {
        return null;
    }

    public void RegisterController(EventSO _event, BaseController controller)
    {
        _controllers.Add(controller);
        _event.Register(controller.HandleEvent);
    }

    public void UnregisterController(EventSO _event, BaseController controller)
    {
        _event.Unregister(controller.HandleEvent);
    }
}
