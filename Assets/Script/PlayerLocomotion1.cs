﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion1 : MonoBehaviour
{
    PlayerManager1 playerManager;
    public Transform cameraObject;
    InputController inputHandler;
    public Vector3 moveDirection;

    [HideInInspector]
    public Transform myTransform;
    [HideInInspector]
    public AnimatorHandler animatorHandler;

    public new Rigidbody rigidbody;
    public GameObject normalCamera;

    [Header("Movement Stats")]
    [SerializeField]
    float movementSpeed = 5;
    [SerializeField]
    float rotationSpeed = 10;

    void Start()
    {
        playerManager = GetComponent<PlayerManager1>();
        rigidbody = GetComponent<Rigidbody>();
        inputHandler = GetComponent<InputController>();
        animatorHandler = GetComponentInChildren<AnimatorHandler>();
        cameraObject = Camera.main.transform;
        myTransform = transform;
        animatorHandler.Initialize();

        // playerManager.isGrounded = true;
        //ignoreForGroundCheck = ~(1 << 8 | 1 << 11);

    }

    #region Movement
    Vector3 normalVector;

    private void HandleRotation(float delta)
    {
        Vector3 targetDir = Vector3.zero;
        float moveOverride = inputHandler.moveAmount;

        targetDir = cameraObject.forward * inputHandler.vertical;
        targetDir += cameraObject.right * inputHandler.horizontal;

        targetDir.Normalize();
        targetDir.y = 0;

        if (targetDir == Vector3.zero)
            targetDir = myTransform.forward;

        float rs = rotationSpeed;

        Quaternion tr = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);

        myTransform.rotation = targetRotation;
    }

    public void HandleMovement(float delta)
    {

        //如果动画处于交互状态，需要缓存数据然后统一输出
        if (playerManager.isInteracting || playerManager.nextAction != 0)
        {
            return;
        }


        moveDirection = cameraObject.forward * inputHandler.vertical;
        moveDirection += cameraObject.right * inputHandler.horizontal;
        moveDirection.Normalize();
        moveDirection.y = 0;
            
        float speed = movementSpeed;


        if (inputHandler.moveAmount > 0.5)
        {
            moveDirection *= speed;
        }
        else
        // 如果输入的向量小于一定值，则归0，代表不移动
        {
            inputHandler.moveAmount = 0; 
        }

        if (!playerManager.forbidMovement)
        {
            Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
            rigidbody.velocity = projectedVelocity;
            animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0);
            if (animatorHandler.canRotate)
            {
                HandleRotation(delta);
            }
        }
        else
        {
            Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, Vector3.zero);
            rigidbody.velocity = projectedVelocity;
            animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0);
        }



    }

    //无用
    public void HandleRollingAndSprinting(float delta)
    {
        if (animatorHandler.anim.GetBool("isInteracting"))
            return;
        if (inputHandler.dodgeFlag)
        {
            moveDirection = cameraObject.forward * inputHandler.vertical;
            moveDirection += cameraObject.right * inputHandler.horizontal;
            
            if(inputHandler.moveAmount > 0)
            {
                animatorHandler.PlayTargetAnimation("Rolling",true);
                moveDirection.y = 0;
            }
        }
    }
    #endregion
}
