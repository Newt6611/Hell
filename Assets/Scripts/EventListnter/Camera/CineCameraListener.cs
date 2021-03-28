using UnityEngine;
using Cinemachine;
using UnityEngine.Experimental.Rendering.Universal;

public class CineCameraListener : MonoBehaviour
{
    [SerializeField] CameraShakeEventAreaSO eventArea;

    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin perlin;

    private bool shouldShake;

    private float shakeTimeTotal;
    private float shakeTime;
    private float startIntensity;

    private void Awake() 
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();    
        perlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void OnEnable() 
    {
        eventArea.shakeEvent += Shake;
    }

    private void OnDisable() 
    {
        eventArea.shakeEvent -= Shake;
    }

    private void Update() 
    {
        if(shouldShake)
        {
            if(shakeTime > 0)
            {
                shakeTime -= Time.deltaTime;
                perlin.m_AmplitudeGain = Mathf.Lerp(startIntensity, 0f, 1 - (shakeTime / shakeTimeTotal));
            }
            else
            {
                perlin.m_AmplitudeGain = 0;
                shouldShake = false;        
            }
        }
    }

    private void Shake(float intensity, float time)
    {
        perlin.m_AmplitudeGain = intensity;

        shakeTime = time;
        shakeTimeTotal = time;
        startIntensity = intensity;
        shouldShake = true;
    }
}
