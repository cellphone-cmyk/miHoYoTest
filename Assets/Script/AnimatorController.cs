using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimatorController : MonoBehaviour
{
    PlayerManager1 playerManager;
    public Animator anim;
    PlayerLocomotion1 playerLocomotion;
    int vertical;
    int horizontal;
    public bool canRotate;


    public void Initialize()
    {
        playerManager = GetComponentInParent<PlayerManager1>();
        anim = GetComponent<Animator>();
        playerLocomotion = GetComponentInParent<PlayerLocomotion1>();
        vertical = Animator.StringToHash("Vertical");
        horizontal = Animator.StringToHash("Horizontal");
    }

    public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement)
    {
        #region Vertical
        float v = 0;

        if (verticalMovement > 0 && verticalMovement < 0.55f)
        {
            v = 0.5f;
        }
        else if (verticalMovement > 0.55f)
        {
            v = 1;
        }
        else if (verticalMovement < 0 && verticalMovement > -0.55f)
        {
            v = -0.5f;
        }
        else if (verticalMovement < -0.55f)
        {
            v = -1;
        }
        else
        {
            v = 0;
        }
        #endregion

        #region Horizontal
        float h = 0;

        if (horizontalMovement > 0 && horizontalMovement < 0.55f)
        {
            h = 0.5f;
        }
        else if (horizontalMovement > 0.55f)
        {
            h = 1;
        }
        else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
        {
            h = -0.5f;
        }
        else if (horizontalMovement < -0.55f)
        {
            h = -1;
        }
        else
        {
            h = 0;
        }
        #endregion

        anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
        anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
    }

    public void PlayTargetAnimation(string targetAnim, bool isInteracting)
    {
        anim.applyRootMotion = isInteracting;
        anim.SetBool("isInteracting", isInteracting);
        //anim.CrossFade(targetAnim, 0.2f);
        anim.Play(targetAnim);
        //AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(1);
        //Debug.Log(stateInfo.IsName("comboo2"));
    }

    //强行锁isInteracting,防止出错
    public void DisableIteracting()
    {
       anim.SetBool("isInteracting", true);
    }

    public void CanRotate()
    {
        canRotate = true;
    }

    public void StopRotation()
    {
        canRotate = false;
    }

    public void EnableCombo()
    {
        anim.SetBool("canDoCombo", true);
    }

    public void DisableCombo()
    {
        anim.SetBool("canDoCombo", false);
    }

    public void EnableSpecialCombo()
    {
        anim.SetBool("canDoSpecialCombo",true);
    }

    public void DisableSpecialCombo()
    {
        anim.SetBool("canDoSpecialCombo", false); ;
    }

    public void reloadScene()
    {
        SceneManager.LoadScene(0);
    }

    private void OnAnimatorMove()
    {
        if (playerManager.isInteracting == false)
            return;

        float delta = Time.deltaTime;
        if (delta == 0) return;
        playerLocomotion.rigidbody.drag = 0;
        Vector3 deltaPosition = anim.deltaPosition;
        deltaPosition.y = 0;
        Vector3 velocity = deltaPosition / delta;
        playerLocomotion.rigidbody.velocity = velocity;
        
        //Debug.Log(velocity);
        //anim.ApplyBuiltinRootMotion();

    }

}
