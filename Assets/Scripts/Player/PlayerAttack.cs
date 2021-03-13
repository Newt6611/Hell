using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Player player;

    void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if(player == null)
            Debug.Log("Can't Find Player Tag !");
    }

    public void Attack()
    {
        if(!player.CanAttack)
            player.GroundDetection();
        player.SetState(player.GetStateCache()["idle"]);
    }
}
