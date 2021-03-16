using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IPlayerState
{
    private Player player;

    public AttackState(Player player)
    {
        this.player = player;
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
}
