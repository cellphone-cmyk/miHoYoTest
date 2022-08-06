using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker1 : MonoBehaviour
{
    AnimatorController animatorHandler;
    InputController inputHandler;
    TraumaInducer traumaInducer;
    StressReceiver stressReceiver;
    PlayerManager1 playerManager;
    public int lastCombo;

    private void Awake()
    {
        animatorHandler = GetComponentInChildren<AnimatorController>();
        inputHandler = GetComponent<InputController>();
        traumaInducer = GetComponentInChildren<TraumaInducer>();
        stressReceiver = GetComponentInChildren<StressReceiver>();
        playerManager = GetComponent<PlayerManager1>();
    }

    public void HandleWeaponCombo()
    {
        if (inputHandler.comboFlag)
        {
            playerManager.RotateToEnemy();
            animatorHandler.anim.SetBool("canDoCombo", false);
            animatorHandler.anim.SetInteger("Combo", lastCombo++);

            //调整镜头抖动、顿帧参数
            if (lastCombo  == 2)
            {
                stressReceiver.MaximumTranslationShake = new Vector3(0, 3, 0);
                traumaInducer.MaximumStress = 0.15f;
                traumaInducer.duration = 4;
            }else if(lastCombo == 3)
            {
                stressReceiver.MaximumTranslationShake = new Vector3(0, 2, 0);
                traumaInducer.MaximumStress = 0.15f;
                traumaInducer.duration = 2;
            }
            //播放对应动画
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
        playerManager.RotateToEnemy();
        animatorHandler.PlayTargetAnimation("combo1", true);
        stressReceiver.MaximumTranslationShake = new Vector3(2,0,0);
        traumaInducer.MaximumStress = 0.15f;
        traumaInducer.duration = 3;
        lastCombo = 1 ;
    }

    public void HandleHeavyAttack()
    {
        playerManager.RotateToEnemy();
        //animatorHandler.anim.SetInteger("Combo", 1);
        animatorHandler.PlayTargetAnimation("heavyAttack", true);
        stressReceiver.MaximumTranslationShake = new Vector3(0, 8, 0);
        traumaInducer.MaximumStress = 0.2f;
        lastCombo = 1;        
    }

}
