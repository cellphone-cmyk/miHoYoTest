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
    public string nextAnim;


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
        //anim.applyRootMotion = isInteracting;
        //canRotate = false;
        anim.SetBool("isInteracting", isInteracting);
        anim.SetBool("forbidInput",true);
        anim.SetBool("forbidMovement",true);
        anim.CrossFade(targetAnim, 0.2f);
        //anim.Play(targetAnim);
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

    public void BufferProtector(float delta)
    {
        // 缓存保护：在命令/对应动画执行中，在动画开始 -> 缓存保护时间段，同样的命令不缓存，以免玩家快速连续点击的时候马上又缓存了一个命令，造成不受控制的感觉
        anim.SetBool("forbidInput", false);
    }

    public void EarlyCancel(float delta)
    {
        anim.SetBool("isInteracting",false);
        anim.SetBool("cancel",true);
        //提前取消：一些比较轻的攻击，玩家的期待是在前摇阶段可以被闪避等动作取消，Moba游戏攻击/技能前摇靠移动/停止指令取消，可以用来骗招，动画开始 -> 提前取消的时间轴标记了这一段时间
    }

    public void EarlistExit()
    {
        //最早退出：当动画播放到最早退出时间，可以开始跳转到已经缓存了的新动画
        //在缓存保护->最早退出时间段，玩家的输入命令可以缓存。
        //缓存保护->最早退出时间段，原则上可以晚于动画结束时间，以创造类似“内置冷却”的效果
        anim.SetBool("isInteracting", false);
        anim.SetBool("cancel", true);
        anim.SetBool("forbidMovement", false);
        //playerLocomotion.moveDirection.y = 0;
    }

    public void LatestExit()
    {       
        //最迟退出：同样用来标记一段辅助输入的时间，在最早退出->最迟退出的时间追加指令，连技不会中断
        //canDoFlag = false;
        anim.SetBool("canDoCombo", false);

    }

    public void RestCommand() {
        //命令重置：标记连技中断的时间，一般此时动画会开始向待机动作跳转
        //命令重置->动作结束阶段，输入命令，则连技中断。如果连技X->X对应动画A->B， 在X命令重置后才追加X指令，只能触发A->A。
        //最迟退出->命令重置时间段，可以不处理，也可以开启另外一个缓存，在命令重置时间点后执行重置过的连技。
        //最迟退出->命令重置时间段也可以做成特殊跳转，实现A · A这样的等待攻击，在序列技表中也比较常见。
        //如果一节等待不够还可以加多节，常见于蓄力到不同阶段退出的指令技。
        //命令重置时间原则上可以位于动画结束之后，可以实现多段翻滚的内置计数，攻击之后移动一段时间再攻击可以接上之前的连击这样的效果
    }
}
