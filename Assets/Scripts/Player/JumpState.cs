using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : IPlayerState
{
    private Player player;
    private string state_name;

    public JumpState(Player player, string name)
    {
        this.player = player;
        state_name = name;
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
        //player.GroundDetection();
    }

    public override void OnExit() 
    {
    }

    public override void PrintName()
    {
        Debug.Log(state_name);
    }
}
