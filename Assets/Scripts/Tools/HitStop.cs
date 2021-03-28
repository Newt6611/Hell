using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    private bool waiting = false;

    private void Start() 
    {
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
}
