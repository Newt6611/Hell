using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : IPlayerState
{
    private Player player;

    public JumpState(Player player)
    {
        this.player = player;
    }

    public override void OnEntry()
    {
        player.PlayAnimation(AniamtionName.jump);
        Jump();
        player.CanJump = false;
    }

    public override void OnUpdate() 
    {
        
    }
    
    public override void OnFixedUpdate()
    {
        player.Movement();
        player.GroundDetection();
    }

    public override void OnExit() 
    {
    }

    public void Jump()
    {
        player.rb.velocity = new Vector2(player.rb.velocity.x, Vector2.up.y * player.JumpForce);
    }
}
