using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    [SerializeField] private bool isUp;

    // for scene one stair
    [SerializeField] private GameObject stair;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            transform.parent.GetComponent<Collider2D>().enabled = isUp;
            
            if(isUp)
                stair.GetComponent<SpriteRenderer>().sortingOrder = 40;
            else
                stair.GetComponent<SpriteRenderer>().sortingOrder = -1;
        }
    }
}
