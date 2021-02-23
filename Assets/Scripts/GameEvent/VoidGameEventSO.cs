using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName="VoidGameEvent", menuName="Game Event/Void")]
public class VoidGameEventSO : ScriptableObject
{
    public UnityAction eventRaised;

    public void Raise()
    {
        eventRaised?.Invoke();
    }
}
