using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackState : BaseState<Player>
{
    public override void Init(Player owner)
    {
        base.Init(owner);
        owner.PlayerInput.actions["Attack"].started += OnAttack;     
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        //Debug.Log($"{elapsedTime} : {(owner.stat as IAttackStat).AttackRate}");
        if(elapsedTime >= (owner.stat as IAttackStat).AttackRate)
        {
            owner.Controller.ChangeState(nameof(PlayerIdleState));
        }
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (AttackAble())
        {
            owner.Controller.ChangeState(nameof(PlayerAttackState));
        }
    }

    private bool AttackAble()
    {
        switch (owner.Controller.CurrentState.GetType().Name)
        {
            case nameof(PlayerMoveState):
            case nameof(PlayerIdleState):
                return true;
            default:
                return false;
        }
    }
}
