using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : BaseCreature
{
    public PlayerController Controller => controller;
    private PlayerController controller;

    public PlayerInput PlayerInput => playerInput;
    private PlayerInput playerInput;

    public Inventory Inventory => inventory;
    private Inventory inventory;

    public Transform cameraContainer;

    private void Update()
    {
        controller.OnUpdate(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        controller.OnFixedUpdate();
    }

    public override void Init()
    {
        base.Init();
        stat = GetComponent<PlayerStat>();
        stat.Init();
        playerInput = GetComponent<PlayerInput>();
        inventory = new Inventory();
        RegisterState();
    }

    public override void RegisterState()
    {
        controller = new PlayerController(this, new PlayerIdleState());
        controller.RegisterState(new PlayerAttackState());
        controller.RegisterState(new PlayerMoveState());
        controller.RegisterState(new PlayerDeathState());
    }
}
