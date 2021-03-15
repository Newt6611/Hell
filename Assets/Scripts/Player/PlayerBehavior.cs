using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
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

    public void Jump()
    {
        player.rb.velocity = new Vector2(player.rb.velocity.x, Vector2.up.y * player.JumpForce);
    }
}
