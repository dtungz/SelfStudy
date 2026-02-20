using UnityEngine;
using UnityEngine.Events;

public abstract class EventListener<T> : MonoBehaviour
{
    [SerializeField] private UnityEvent<T> eventRaised;
    [SerializeField] private EventChannelSO<T> unityEvent;
    public void OnRaised(T value)
    {
        eventRaised?.Invoke(value);
    }

    private void OnEnable()
    {
        unityEvent.AddListener(this);
    }

    private void OnDisable()
    {
        unityEvent.RemoveListener(this);
    }
}
