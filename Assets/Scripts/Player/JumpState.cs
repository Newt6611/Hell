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
            //player.SetAnimationBool("jump", true);
            player.PlayAnimation("jump");
            Jump();
            canJump = false;
        }
    }

    public override void OnUpdate() 
    {
        
    }
    
    public override void OnFixedUpdate()
    {
        player.Movement();

        if(Physics2D.OverlapCircle(player.JumpPosition.position, 0.3f, player.WalkableLayer) && !canJump)
        {
            if(player.MovementX == 0)
                player.SetState(player.GetStateCache()["idle"]);
            else if(player.IsRun)
                player.SetState(player.GetStateCache()["run"]);
            else if(!player.IsRun)
                player.SetState(player.GetStateCache()["walk"]);
            canJump = true;
        }
    }

    public override void OnExit() 
    {
        player.SetAnimationBool("jump", false);
    }

    public void Jump()
    {
        //player.rb.AddForce(Vector2.up * player.JumpForce, ForceMode2D.Impulse);
        player.rb.velocity = new Vector2(player.rb.velocity.x, Vector2.up.y * player.JumpForce);
    }
}
