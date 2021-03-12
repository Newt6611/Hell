using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : IPlayerState
{
    private Player player;

    public WalkState(Player player) 
    {
        this.player = player;
    }

    public override void OnEntry()
    {
        player.SetSpeed(player.WalkSpeed);
        player.PlayAnimation(AniamtionName.walk);
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
        player.SetAnimationBool("isWalk", false);
    }
}
