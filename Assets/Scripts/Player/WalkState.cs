using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : IPlayerState
{
    private Player player;
    private string state_name;

    public WalkState(Player player, string name) 
    {
        this.player = player;
        state_name = name;
    }

    public override void OnEntry()
    {
        player.SetSpeed(player.WalkSpeed);
        player.PlayAnimation(AniamtionName.walk);
        player.SetPhysicsFriction(false);
    }

    public override void OnUpdate()
    {
        if(player.MovementX == 0)
            player.SetState(player.GetStateCache()["idle"]);

        if(player.IsRun)
            player.SetState(player.GetStateCache()["run"]);
    }

    public override void OnFixedUpdate()
    {
        player.Movement();
    }

    public override void OnExit()
    {
    }

    public override void PrintName()
    {
        Debug.Log(state_name);
    }
}
