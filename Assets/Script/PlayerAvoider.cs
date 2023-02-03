using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAvoider : MonoBehaviour
{
    AnimatorController animatorHandler;
    InputController inputHandler;
    TraumaInducer traumaInducer;
    StressReceiver stressReceiver;
    PlayerManager1 playerManager;
    PlayerLocomotion1 playerLocomotion;

    [Header("temporary data")]
    public Vector3 moveDirection;
    public float moveAmount;
    public int nextAction;

    private void Awake()
    {
        animatorHandler = GetComponentInChildren<AnimatorController>();
        inputHandler = GetComponent<InputController>();
        traumaInducer = GetComponentInChildren<TraumaInducer>();
        stressReceiver = GetComponentInChildren<StressReceiver>();
        playerManager = GetComponent<PlayerManager1>();
        playerLocomotion = GetComponent<PlayerLocomotion1>();
    }
    public void HandleAvoid(bool isInteracting)
    {
        //允许转向
        //animatorHandler.canRotate = true;
        //正常获取移动向量
        moveDirection = playerLocomotion.cameraObject.forward * inputHandler.vertical;
        moveDirection += playerLocomotion.cameraObject.right * inputHandler.horizontal;
        moveAmount = inputHandler.moveAmount;
        moveDirection.y = 0;

        //如果动画处于交互状态，需要缓存数据然后统一输出
        if (isInteracting)
        {
            nextAction = 1;
            playerManager.nextAction = nextAction;
            
            return; //如果代码重复，通过这个return来理清思路
        }

        //通过flag来统一判定输出口
        if (inputHandler.dodgeFlag)
        {
            playerLocomotion.moveDirection = moveDirection; // 翻滚方向
            
            if (moveAmount > 0)
            {
                animatorHandler.PlayTargetAnimation("Avoid_F", true);
                playerLocomotion.moveDirection.y = 0;

            }
            else
            {
                animatorHandler.PlayTargetAnimation("Avoid_B", true);
                playerLocomotion.moveDirection.y = 0;

            }

            //restAllTemp();
        }
    }

    public void restAllTemp()
    {
        moveDirection = Vector3.zero;
        moveAmount = 0;
        nextAction = 0;
    }
}
