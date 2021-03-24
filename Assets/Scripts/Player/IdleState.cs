using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IPlayerState
{
    private Player player;
    private string state_name;

    public IdleState(Player player, string name)
    {
        this.player = player;
        state_name = name;
    }

    public override void OnEntry()
    {
        player.SetSpeed(0);
        player.PlayAnimation(AniamtionName.idle);
        player.SetPhysicsFriction(true);
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

    public override void PrintName()
    {
        Debug.Log(state_name);
    }
}
