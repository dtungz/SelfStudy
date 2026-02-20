using System.Collections.Generic;
using UnityEngine;

public abstract class EventChannelSO<T1,T2> : ScriptableObject
{
    private readonly List<EventListener<T1,T2>> _eventRaised = new List<EventListener<T1,T2>>();

    public void Raise(T1 value1, T2 value2)
    {
        foreach (var listener in _eventRaised)
        {
            listener?.Raise(value1, value2);
        }
    }

    public void AddListener(EventListener<T1, T2> listener)
    {
        _eventRaised.Add(listener);
    }

    public void RemoveListener(EventListener<T1, T2> listener)
    {
        _eventRaised.Remove(listener);
    }
}
