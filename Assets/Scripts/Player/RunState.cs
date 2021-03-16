using System.Collections;
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
        player.PlayAnimation(AniamtionName.run);
        player.SetSpeed(player.RunSpeed);
        player.SetPhysicsFriction(false);
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
        if(player.MovementX == 0)
        {
            player.IsRun = false;
        }
    }
}
