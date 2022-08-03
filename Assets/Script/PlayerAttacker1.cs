using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker1 : MonoBehaviour
{
    AnimatorController animatorHandler;
    InputController inputHandler;
    public int lastCombo;

    private void Awake()
    {
        animatorHandler = GetComponentInChildren<AnimatorController>();
        inputHandler = GetComponent<InputController>();
    }

    public void HandleWeaponCombo()
    {
        if (inputHandler.comboFlag)
        {
            animatorHandler.anim.SetBool("canDoCombo", false);
            animatorHandler.anim.SetInteger("Combo", lastCombo++);
            //animatorHandler.anim.SetBool("isInteracting",true);
            animatorHandler.PlayTargetAnimation("combo"+lastCombo.ToString(), true);
            //Debug.Log("combo" + lastCombo.ToString());
        }

        if(lastCombo > 3)
        {
            lastCombo = 0;
        }
    }

    public void HandleLightAttack()
    {
        //animatorHandler.anim.SetInteger("Combo",1);
        animatorHandler.PlayTargetAnimation("combo1", true);
        lastCombo = 1 ;
    }

    public void HandleHeavyAttack()
    {
        //animatorHandler.anim.SetInteger("Combo", 1);
        animatorHandler.PlayTargetAnimation("combo1", true);
        lastCombo = 1;        
    }

}
