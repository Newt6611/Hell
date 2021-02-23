using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : IPlayerState
{
    private Player player;
    private bool canJump;

    public JumpState(Player player)
    {
        this.player = player;
        canJump = true;
    }

    public override void OnEntry()
    {
        if(canJump)
        {
            //player.SetAnimationTrigger("jump");
            Jump();
            canJump = false;
        }
    }

    public override void OnUpdate() { }
    
    public override void OnFixedUpdate()
    {
        if(Physics2D.OverlapCircle(player.JumpPosition.position, 0.3f, player.WalkableLayer) && !canJump)
        {
            canJump = true;

            if(player.MovementX == 0)
                player.SetState(player.GetStateCache()["idle"]);
            else if(player.IsRun)
                player.SetState(player.GetStateCache()["run"]);
            else if(!player.IsRun)
                player.SetState(player.GetStateCache()["walk"]);
        }

        player.Movement();
    }

    public override void OnExit() { }

    private void Jump() 
    {
        player.rb.velocity = Vector2.up * player.JumpForce;
    }
}
