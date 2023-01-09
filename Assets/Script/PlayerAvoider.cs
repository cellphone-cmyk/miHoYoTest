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
    private void Awake()
    {
        animatorHandler = GetComponentInChildren<AnimatorController>();
        inputHandler = GetComponent<InputController>();
        traumaInducer = GetComponentInChildren<TraumaInducer>();
        stressReceiver = GetComponentInChildren<StressReceiver>();
        playerManager = GetComponent<PlayerManager1>();
        playerLocomotion = GetComponent<PlayerLocomotion1>();
    }
    public void HandleAvoid()
    {
        if (inputHandler.dodgeFlag)
        {
            if (animatorHandler.anim.GetBool("isInteracting"))
                return;

            playerLocomotion.moveDirection = playerLocomotion.cameraObject.forward * inputHandler.vertical;
            playerLocomotion.moveDirection += playerLocomotion.cameraObject.right * inputHandler.horizontal; // ·­¹ö·½Ïò

            if (inputHandler.moveAmount > 0)
            {
                animatorHandler.PlayTargetAnimation("Avoid_F", true);
                playerLocomotion.moveDirection.y = 0;

            }
            else
            {
                animatorHandler.PlayTargetAnimation("Avoid_B", true);
                playerLocomotion.moveDirection.y = 0;

            }
            
        }
    }
}
