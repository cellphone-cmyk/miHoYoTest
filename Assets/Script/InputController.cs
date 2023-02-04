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

    public void TickInput(float delta,bool isInteracting) //isInteracting：在此变量false之前，禁止其他播放其他动画，但期间如果forbidInput不为True可以缓存命令
    {
        if (playerManager.nextAction != 0 && playerManager.cancel)
        {
            switch (playerManager.nextAction)
            {
                case 1:
                    LshiftInput = true;
                    HandleDodgeInput(delta, isInteracting);
                    break;
                case 2:
                    rb_Input = true;
                    HandleAttackInput(delta,isInteracting);
                    break;
                case 3:
                    rt_Input = true;
                    HandleAttackInput(delta,isInteracting);
                    Debug.Log("heavy attack interrupt");
                    break;
            }
            playerManager.nextAction = 0;
        }
        else
        {
            MoveInput(delta, isInteracting);
            HandleAttackInput(delta, isInteracting);
            HandleLockOnInput();
            HandleDeath();
            HandleDodgeInput(delta, isInteracting);
        }

    }

    private void MoveInput(float delta, bool isInteracting)
    {
        horizontal = movementInput.x;
        vertical = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        mouseX = cameraInput.x;
        mouseY = cameraInput.y;
    }

    public void HandleDodgeInput(float delta, bool isInteracting)
    {
        if (LshiftInput)
        {
            if (playerManager.isInteracting)
            {
                playerAvoider.HandleAvoid(isInteracting);
                return;
            }               
            dodgeFlag = true;
            playerAvoider.HandleAvoid(isInteracting);
            dodgeFlag = false;
        }
    }

    private void HandleAttackInput(float delta, bool isInteracting)
    {
        //输入白名单：handle期间接受输入的指令
        //if (!playerManager.forbidInput)
        //{
        //    inputActions.PlayerActions.RB.performed += i => rb_Input = true;
        //    inputActions.PlayerActions.RT.performed += i => rt_Input = true;
        //    inputActions.PlayerActions.UltimateSkill.performed += i => ultimate_skill = true;
        //}


        if (rb_Input)
        {
            if (playerManager.canDoCombo)
            {
                comboFlag = true;
                playerAttacker.HandleWeaponCombo(isInteracting);
                comboFlag = false;
            }
            else
            {
                playerAttacker.HandleLightAttack(isInteracting);
            }
        }

        if (rt_Input)
        {
            if (playerManager.canDoCombo)
            {
                comboFlag = true;
                playerAttacker.HandleWeaponCombo(isInteracting);
                comboFlag = false;
            }

            if (playerManager.canDoSpecialCombo)
                return;
            playerAttacker.HandleHeavyAttack(isInteracting);
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
