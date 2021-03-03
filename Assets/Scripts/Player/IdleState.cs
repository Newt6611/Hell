using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IPlayerState
{
    private Player player;

    public IdleState(Player player)
    {
        this.player = player;
    }

    public override void OnEntry()
    {
        player.SetSpeed(0);
    }

    public override void OnUpdate()
    {
        if(player.MovementX != 0)
        {
            player.SetState(player.GetStateCache()["walk"]);
        }
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnExit()
    {
        
    }
}
