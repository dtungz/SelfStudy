using System.Collections.Generic;
using UnityEngine;

public abstract class EventChannelSO<T> : ScriptableObject
{
    private readonly List<EventListener<T>> _eventRaised = new  List<EventListener<T>>();

    public void EventRaised(T value)
    {
        foreach (var listener in _eventRaised)
        {
            listener?.OnRaised(value);
        }
    }

    public void AddListener(EventListener<T> listener)
    {
        _eventRaised.Add(listener);
    }

    public void RemoveListener(EventListener<T> listener)
    {
        _eventRaised.Remove(listener);
    }
}
