using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveState : BaseState<Player>
{
    public override void Init(Player owner)
    {
        base.Init(owner);
        owner.PlayerInput.actions["Move"].performed += OnMove;
        owner.PlayerInput.actions["Move"].canceled += OnMoveStop;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        owner.Controller.inputDir = context.ReadValue<Vector2>().normalized;
    }

    public void OnMoveStop(InputAction.CallbackContext context)
    {
        owner.Controller.inputDir = Vector3.zero;
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        if (owner.Controller.inputDir == Vector3.zero)
        {
            owner.Controller.ChangeState(nameof(PlayerIdleState));
        }
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        if(owner.stat is IMoveStat moveStat)
        {
            Vector3 input = ((owner.transform.forward * owner.Controller.inputDir.y) + (owner.transform.right * owner.Controller.inputDir.x)) * moveStat.GetTotalMoveSpeed();
            input.y = owner.Rb.velocity.y; //현재 객체의 벨로시티를 가져오지 않으면 공중에서 이동하는 형태가 된다!
            owner.Rb.velocity = input;
        }   
    }
}
