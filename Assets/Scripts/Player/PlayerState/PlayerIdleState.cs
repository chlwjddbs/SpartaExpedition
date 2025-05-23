using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : BaseState<Player>
{
    public override void Init(Player owner)
    {
        base.Init(owner);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Vector3 input = Vector3.zero;
        input.y = owner.Rb.velocity.y;
        owner.Rb.velocity = input;
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        
        if(owner.Controller.inputDir != Vector3.zero)
        {
            owner.Controller.ChangeState(nameof(PlayerMoveState));
        }
    }
}
