using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public bool isInteracting;
    public bool isDead;
    public bool ultimate_skill;
    public bool LshiftInput;

    public bool comboFlag;
    public bool SpecialComboFlag;
    public bool lockOnFlag;
    public bool ultimateSkillFlag;
    public bool dodgeFlag;

    PlayerControls inputActions;
    CameraHandler cameraHandler;
    PlayerManager1 playerManager;
    PlayerAttacker1 playerAttacker;
    PlayerAvoider playerAvoider;

    Vector2 movementInput;
    Vector2 cameraInput;

    private void Awake()
    {
        //需要获取的组件
        cameraHandler = GetComponentInChildren<CameraHandler>();
        playerManager = GetComponent<PlayerManager1>();
        playerAttacker = GetComponent<PlayerAttacker1>();
        playerAvoider = GetComponent<PlayerAvoider>();
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
            inputActions.PlayerActions.ResPawn.performed += i => isDead = true;
            inputActions.PlayerActions.UltimateSkill.performed += i => ultimate_skill = true;
            inputActions.PlayerActions.Dodge.performed += i => LshiftInput = true;
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
        HandleAttackInput(delta);
        HandleLockOnInput();
        HandleDeath();
        HandleDodgeInput(delta);
    }

    private void MoveInput(float delta)
    {
        horizontal = movementInput.x;
        vertical = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        mouseX = cameraInput.x;
        mouseY = cameraInput.y;
    }

    public void HandleDodgeInput(float delta)
    {
        if (LshiftInput)
        {
            if (playerManager.isInteracting)
                return;
            dodgeFlag = true;
            playerAvoider.HandleAvoid();
            dodgeFlag = false;
        }
    }

    private void HandleAttackInput(float delta)
    {
        inputActions.PlayerActions.RB.performed += i => rb_Input = true;
        inputActions.PlayerActions.RT.performed += i => rt_Input = true;
        inputActions.PlayerActions.UltimateSkill.performed += i => ultimate_skill = true;

        if (rb_Input)
        {
            if (playerManager.canDoCombo)
            {
                comboFlag = true;
                playerAttacker.HandleWeaponCombo();
                comboFlag = false;
            }
            else
            {
                if (playerManager.isInteracting)
                    return;

                if (playerManager.canDoCombo)
                    return;
                playerAttacker.HandleLightAttack();
            }
        }

        if (rt_Input)
        {
            if (playerManager.canDoSpecialCombo)
            {
                comboFlag = true;
                playerAttacker.HandleSpecialCombo();
                comboFlag = false;
            }
            if (playerManager.isInteracting)
                return;

            if (playerManager.canDoSpecialCombo)
                return;
            playerAttacker.HandleHeavyAttack();
        }

        if (ultimate_skill)
        {
            if (playerManager.isInteracting)
                return;
            playerAttacker.HandleUltimateAttack(delta);
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

    public void HandleDeath()
    {
        if (isDead)
        {
            playerManager.isDead = true;
        }
    }

}
