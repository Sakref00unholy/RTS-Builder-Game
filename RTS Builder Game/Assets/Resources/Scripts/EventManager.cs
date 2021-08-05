using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    private Dictionary<string, UnityEvent> _events;
    private static EventManager _eventManager;
    private Dictionary<string, CustomEvent> _typedEvent;

    public static EventManager instance
    {
        get
        {
            if (!_eventManager)
            {
                _eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!_eventManager)
                    Debug.LogError("there needs to be one active eventmanager script on a gameobject in your scene.");
                else
                    _eventManager.Init();
               
            }
            return _eventManager;
        }
    }

    void Init()
    {
        if (_events == null)
        {
            _events = new Dictionary<string, UnityEvent>();
            _typedEvent = new Dictionary<string, CustomEvent>();
        }
    }
    
    public static void AddListener(string eventName, UnityAction listener)
    {
        UnityEvent evt = null;
        if (instance._events.TryGetValue(eventName, out evt))
        {
            evt.AddListener(listener);
        }
        else
        {
            evt = new UnityEvent();
            evt.AddListener(listener);
            instance._events.Add(eventName, evt);
        }
    }

    public static void RemoveListener(string eventName, UnityAction listener)
    {
        if (_eventManager = null) return;
        UnityEvent evt = null;
        if (instance._events.TryGetValue(eventName, out evt))
            evt.RemoveListener(listener);
            
    }

    public static void TriggerEvent (string eventName)
    {
        UnityEvent evt = null;
        if (instance._events.TryGetValue(eventName, out evt))
            evt.Invoke();
    }
    public static void AddTypedListener(string eventName, UnityAction<CustomEventData> listener)
    {
        CustomEvent evt = null;
        if (instance._typedEvent.TryGetValue(eventName, out evt))
        {
            evt.AddListener(listener);
        }
        else
        {
            evt = new CustomEvent();
            evt.AddListener(listener);
            instance._typedEvent.Add(eventName, evt);
        }
    }
    public static void RemoveTypedListener(string eventName, UnityAction<CustomEventData> listener)
    {
        if (_eventManager == null) return;
        CustomEvent evt = null;
        if (instance._typedEvent.TryGetValue(eventName, out evt))
            evt.RemoveListener(listener);
    }

    public static void TriggerTypedEvent(string eventName, CustomEventData data)
    {
        CustomEvent evt = null;
        if (instance._typedEvent.TryGetValue(eventName, out evt))
            evt.Invoke(data);
    }
}
