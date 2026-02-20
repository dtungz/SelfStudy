using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class EventListener<T1,T2> : MonoBehaviour
{
    [SerializeField] private EventChannelSO<T1, T2> eventRaised;
    [SerializeField] private UnityEvent unityEvent;

    public void Raise(T1 value1, T2 value2)
    {
        eventRaised?.Raise(value1, value2);
    }

    private void OnEnable()
    {
        eventRaised.AddListener(this);
    }

    private void OnDisable()
    {
        eventRaised.RemoveListener(this);
    }
}
