using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class HitStop : MonoBehaviour
{
    private bool waiting = false;

    private CinemachineBrain brain;

    private void Awake()
    {
        brain = Camera.main.GetComponent<CinemachineBrain>();
    }

    private void OnEnable()
    {
        brain.m_CameraActivatedEvent.AddListener(OnVCameraChange);
    }

    private void OnDisable()
    {
        brain.m_CameraActivatedEvent.RemoveListener(OnVCameraChange);
    }

    public void Stop(float time) 
    {
        if(waiting)
            return;
        Time.timeScale = 0.0f;
        StartCoroutine(Wait(time));
    }

    IEnumerator Wait(float time) 
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1.0f;
        waiting = false;
    }

    private void OnVCameraChange(ICinemachineCamera camera, ICinemachineCamera c) 
    {
        Debug.Log(camera.Name);
    }
}
