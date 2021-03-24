using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IPlayerState
{
    private Player player;
    private string state_name;

    public AttackState(Player player, string name)
    {
        this.player = player;
        state_name = name;
    }

    public override void OnEntry()
    {
        player.PlayAnimation(AniamtionName.attack);
        player.trail.enabled = true;
    }

    public override void OnUpdate()
    {
    }

    public override void OnFixedUpdate()
    {
        player.GroundDetection();
    }

    public override void OnExit()
    {
        player.CanJump = true;
        player.CanAttack = true;
        player.trail.enabled = false;
    }

    public override void PrintName()
    {
        Debug.Log(state_name);
    }
}
