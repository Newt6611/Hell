using UnityEngine;
using UnityEngine.Events;

public class VoidGameEventListener : MonoBehaviour
{
    [SerializeField] private VoidGameEventSO _channel;
    public UnityEvent OnEventRaised;

    private void OnEnable() 
    {
        _channel.eventRaised += Response;
    }

    private void OnDisable() 
    {
        _channel.eventRaised -= Response;
    }

    public void Response()
    {
        OnEventRaised?.Invoke();
    }
}
