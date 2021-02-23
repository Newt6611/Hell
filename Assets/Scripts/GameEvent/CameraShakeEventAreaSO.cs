using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName="Camera Shake", menuName="Game Event/CameraShake")]
public class CameraShakeEventAreaSO : ScriptableObject
{
    public UnityAction<float, float> shakeEvent;

    public void Raise(float intensity, float time)
    {
        shakeEvent?.Invoke(intensity, time);
    }
}
