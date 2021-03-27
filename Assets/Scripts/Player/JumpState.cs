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
        player.IsJumping = true;

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
    }

    public override void OnExit() 
    {
        if(player.IsJumping)
        {
            player.IsJumping = false;
            
            player.EndJumpState();
        }
    }

    public override void PrintName()
    {
        Debug.Log(state_name);
    }
}
