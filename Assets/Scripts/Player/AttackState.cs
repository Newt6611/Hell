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
        player.SetAnimationTrigger("attack");
    }

    public override void OnUpdate()
    {
        if(!player.ani.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            player.SetState(player.stateCache["idle"]);
        }
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnExit()
    {
    }
}
