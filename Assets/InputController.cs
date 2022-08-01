using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SG;

public class InputController : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float moveAmount;
    public float mouseX;
    public float mouseY;

    public bool b_Input;
    public bool y_Input;
    public bool rb_Input;
    public bool rt_Input;
    public bool lockOnInput;

    public bool comboFlag;
    public bool lockOnFlag;

    PlayerControls inputActions;
    CameraHandler cameraHandler;

    Vector2 movementInput;
    Vector2 cameraInput;

    private void Awake()
    {
        //需要获取的组件
        cameraHandler = GetComponentInChildren<CameraHandler>();
    }

    public void OnEnable()
    {
        if(inputActions == null)
        {
            inputActions = new PlayerControls();
            inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
            inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            inputActions.PlayerActions.RB.performed += i => rb_Input = true;
            inputActions.PlayerActions.RT.performed += i => rt_Input = true;
            inputActions.PlayerActions.LockOn.performed += i => lockOnInput = true;
        }

        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void TickInput(float delta)
    {
        MoveInput(delta);
        //HandleAttackInput(delta);
        HandleLockOnInput();
    }

    private void MoveInput(float delta)
    {
        horizontal = movementInput.x;
        vertical = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        mouseX = cameraInput.x;
        mouseY = cameraInput.y;
    }

    private void HandleAttackInput(float delta)
    {
        inputActions.PlayerActions.RB.performed += i => rb_Input = true;
        inputActions.PlayerActions.RT.performed += i => rt_Input = true;

        if (rb_Input)
        {
            //if (playerManager.canDoCombo)
            //{
            //    comboFlag = true;
            //    playerAttacker.HandleWeaponCombo(playerInventory.rightWeapon);
            //    comboFlag = false;
            //}
            //else
            //{
            //    if (playerManager.isInteracting)
            //        return;

            //    if (playerManager.canDoCombo)
            //        return;

            //    playerAttacker.HandleLightAttack(playerInventory.rightWeapon);
            //}
        }

        if (rt_Input)
        {
            //playerAttacker.HandleHeavyAttack(playerInventory.rightWeapon);
        }
    }

    private void HandleLockOnInput()
    {
        if (lockOnInput && lockOnFlag == false)
        {
            lockOnInput = false;
            lockOnFlag = true;
            cameraHandler.isAiming = true;
        }
        else if (lockOnInput && lockOnFlag)
        {
            lockOnInput = false;
            lockOnFlag = false;
            cameraHandler.isAiming = false;
        }
    }


}
