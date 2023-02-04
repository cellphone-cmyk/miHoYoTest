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
    }

    public void HandleWeaponCombo(bool isInteracting)
    {
        if (inputHandler.comboFlag)
        {
            if (isInteracting)
            {
                if (inputHandler.rb_Input)
                {
                    nextAction = 2; //2为攻击
                    playerManager.nextAction = nextAction;
                    return;
                }else if (inputHandler.rt_Input)
                {
                    nextAction = 3; //3为重攻击
                    playerManager.nextAction = nextAction;
                    Debug.Log("3");
                    return;
                }

            }
            playerManager.RotateToEnemy();
            animatorHandler.anim.SetBool("canDoCombo", false);
            animatorHandler.anim.SetBool("canDoSpecialCombo", false);
            //animatorHandler.anim.SetInteger("Combo", lastCombo++);

            //判断是否符合当前连击节点
            if (animatorHandler.anim.GetBool("canLightAttack") && inputHandler.rb_Input)
            {
                animatorHandler.anim.SetBool("LightAttack",true);

            }else if (animatorHandler.anim.GetBool("canHeavyAttack") && inputHandler.rt_Input)
            {
                Debug.Log("22");
                animatorHandler.anim.SetBool("HeavyAttack", true);
            }
            //else if (animatorHandler.anim.GetBool("canLPLightAttack") && inputHandler.rt_Input)
            //{
            //    animatorHandler.anim.SetBool("LPLightAttack", true);

            //}else if (animatorHandler.anim.GetBool("canLPHeavyAttack") && inputHandler.rt_Input)
            //{
            //    animatorHandler.anim.SetBool("LPHeavyAttack", true);
            //}
            //不在连击序列
            else if (inputHandler.rb_Input)
            {
                HandleLightAttack(isInteracting);
            }else if (inputHandler.rt_Input)
            {
                HandleHeavyAttack(isInteracting);
            }

            //播放对应动画
            //animatorHandler.PlayTargetAnimation("combo"+lastCombo.ToString(), true);
            //animatorHandler.PlayTargetAnimation($"combo{lastCombo}", true);
            //Debug.Log("combo" + lastCombo.ToString());

            //调整镜头抖动、顿帧参数
            //if (lastCombo  == 2)
            //{
            //    stressReceiver.MaximumTranslationShake = new Vector3(0, 3, 0);
            //    traumaInducer.MaximumStress = 0.15f;
            //    traumaInducer.duration = 4;
            //}
            //else if(lastCombo == 3)
            //{
            //    stressReceiver.MaximumTranslationShake = new Vector3(0, 2, 0);
            //    traumaInducer.MaximumStress = 0.15f;
            //    traumaInducer.duration = 2;
            //}

        }

        //if (lastCombo > 3)
        //{
        //    lastCombo = 0;
        //}
    }

    public void HandleSpecialCombo()
    {
        if (inputHandler.comboFlag)
        {
            playerManager.RotateToEnemy();
            animatorHandler.anim.SetBool("canDoCombo", false);
            animatorHandler.anim.SetBool("canDoSpecialCombo", false);
            animatorHandler.anim.SetInteger("Combo", lastCombo);

            //调整镜头抖动、顿帧参数
            if (lastCombo == 2)
            {
                stressReceiver.MaximumTranslationShake = new Vector3(1, 1, 0);
                traumaInducer.MaximumStress = 0.15f;
                traumaInducer.duration = 3;
            }
            else if (lastCombo == 3)
            {
                stressReceiver.MaximumTranslationShake = new Vector3(1, 1, 0);
                traumaInducer.MaximumStress = 0.15f;
                traumaInducer.duration = 3;
            }
            //播放对应动画
            animatorHandler.PlayTargetAnimation("SpecialAttack" + lastCombo.ToString(), true);
            //Debug.Log("combo" + lastCombo.ToString());

            lastCombo++;
        }

        if (lastCombo > 3)
        {
            lastCombo = 0;
        }
    }

    public void HandleLightAttack(bool isInteracting)
    {
        //animatorHandler.anim.SetInteger("Combo",1);
        if (isInteracting)
        {
            nextAction = 2; //2为攻击
            playerManager.nextAction = nextAction;
            return;
        }

        playerManager.RotateToEnemy();
        animatorHandler.PlayTargetAnimation("GhostSamurai_APose_Attack02_1_ALL_Root", true);
        //stressReceiver.MaximumTranslationShake = new Vector3(2,0,0);
        //traumaInducer.MaximumStress = 0.15f;
        //traumaInducer.duration = 3;
        //lastCombo = 1;
    }

    public void HandleHeavyAttack(bool isInteracting)
    {
        if (isInteracting)
        {
            nextAction = 3;
            playerManager.nextAction = nextAction;
            return;
        }

        playerManager.RotateToEnemy();
        //animatorHandler.anim.SetInteger("Combo", 1);
        animatorHandler.PlayTargetAnimation("GhostSamurai_APose_Attack01_1_ALL_Root", true);
        //stressReceiver.MaximumTranslationShake = new Vector3(0, 2, 0);
        //traumaInducer.MaximumStress = 0.15f;
        //lastCombo = 0;        
    }

    public void HandleUltimateAttack(float delta)
    {
        playerManager.AttackRange = 10;
        playerManager.SearchNearEnemy();
        playerManager.RotateToEnemy();
        animatorHandler.PlayTargetAnimation("taunt", true);
        //animatorHandler.anim.SetBool("isUltimate",true);
        playerManager.AttackRange = 2;
    }

}
