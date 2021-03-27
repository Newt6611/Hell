using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionBox : MonoBehaviour
{
    [SerializeField] Transform nextSpot;

    [SerializeField] private PlayerSceneTransitionSO fadeInOut;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            fadeInOut.RaiseEvent(nextSpot.position);
        }    
    }
}
