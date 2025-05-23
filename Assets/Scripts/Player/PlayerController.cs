using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController<Player>
{
    private Interaction interaction;
    private float maxInteractionDistance = 15;

    public Vector3 inputDir;

    private Vector2 mousePos;

    private float cameraRotateAngle;

    private float maxCameraRotate = 90;
    private float minCameraRotate = -50;

    private float rotateSpeed = 10;

    private float jumpPower = 5;

    public PlayerController(Player owner, BaseState<Player> initState) : base(owner, initState) 
    {
        interaction = new Interaction(maxInteractionDistance);
        this.owner.PlayerInput.actions["Look"].performed += OnLook;
        this.owner.PlayerInput.actions["Jump"].started += OnJump;
        this.owner.PlayerInput.actions["Interaction"].started += OnInteraction;
    }

    public override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        interaction.GetInteraction();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mousePos = context.ReadValue<Vector2>();
        LookRotat();
    }

    public override void LookRotat()
    {
        base.LookRotat();

        owner.transform.rotation *= Quaternion.Euler(0, mousePos.x * rotateSpeed * Time.deltaTime, 0);

        cameraRotateAngle += mousePos.y * rotateSpeed * Time.deltaTime;
        cameraRotateAngle = Mathf.Clamp(cameraRotateAngle, minCameraRotate, maxCameraRotate);
        owner.cameraContainer.localRotation = Quaternion.Euler(-cameraRotateAngle, 0, 0);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Vector3 jumpVelocity = owner.Rb.velocity;
        jumpVelocity.y = 0;
        owner.Rb.velocity = jumpVelocity;
        owner.Rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        interaction.OnInteraction();
    }
}
