using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class TriggerManager : MonoBehaviour
{
    public static TriggerManager Instance { get; private set; }

    private Dictionary<string, UnityEvent> events = new Dictionary<string, UnityEvent>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }


    public static void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent;
        if (Instance.events.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            Instance.events.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        if (Instance.events == null) return;
        UnityEvent thisEvent;
        if (Instance.events.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void Trigger(string eventName)
    {
        UnityEvent thisEvent;
        if (Instance.events.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}