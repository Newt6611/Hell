﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IPlayerState
{
    private Player player;

    public RunState(Player player) 
    {
        this.player = player;
    }

    public override void OnEntry()
    {
        //player.SetAnimationBool("isRun", true);
        player.PlayAnimation("run");
        player.SetSpeed(player.RunSpeed);
    }

    public override void OnUpdate()
    {
        if(player.MovementX == 0)
            player.SetState(player.GetStateCache()["idle"]);
        
        if(!player.IsRun && player.MovementX != 0)
            player.SetState(player.GetStateCache()["walk"]);
    }

    public override void OnFixedUpdate()
    {
        player.Movement();
    }

    public override void OnExit()
    {
        player.SetAnimationBool("isRun", false);
        if(player.MovementX == 0)
        {
            player.IsRun = false;
        }
    }
}
