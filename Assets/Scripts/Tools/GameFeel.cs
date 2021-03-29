using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameFeel : MonoBehaviour
{
    private bool waitingStop = false;
    private bool waitingShake = false;

    private CinemachineBrain brain;
    private CinemachineVirtualCamera currentCam;

    private void Awake()
    {
        brain = Camera.main.GetComponent<CinemachineBrain>();
    }

    public void Stop(float time) 
    {
        if(waitingStop)
            return;
        Time.timeScale = 0.0f;
        StartCoroutine(WaitStop(time));
    }

    private IEnumerator WaitStop(float time) 
    {
        waitingStop = true;
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1.0f;
        waitingStop = false;
    }

    public void ShakeCamera(float intensity, float time)
    {
        if(waitingShake)
            return;

        CinemachineBasicMultiChannelPerlin perlin = currentCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = intensity;
        StartCoroutine(WaitShake(time, perlin));
    }

    private IEnumerator WaitShake(float time, CinemachineBasicMultiChannelPerlin perlin) 
    {
        waitingShake = true;
        yield return new WaitForSecondsRealtime(time);
        perlin.m_AmplitudeGain = 0.0f;
        waitingShake = false;
    }



    private void OnEnable()
    {
        brain.m_CameraActivatedEvent.AddListener(OnVCameraChange);
    }

    private void OnDisable()
    {
        brain.m_CameraActivatedEvent.RemoveListener(OnVCameraChange);
    }




    private void OnVCameraChange(ICinemachineCamera camera, ICinemachineCamera c) 
    {
        currentCam = camera as CinemachineVirtualCamera;
    }
}
