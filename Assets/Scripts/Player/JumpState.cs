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
        player.CanJump = false;

        if(!player.IsRun)
            player.SetSpeed(player.WalkSpeed);
        else
            player.SetSpeed(player.RunSpeed);
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
}
