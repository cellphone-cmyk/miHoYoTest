using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimatorBool : StateMachineBehaviour
{

    public string[] Exit_targetBool = new string[1];
    public bool[] Exit_status = new bool[1];
    public string[] Enter_targetBool = new string[100];
    public bool[] Enter_status = new bool[100];

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for (int i = 0; i<Enter_targetBool.Length;i++)
        {
            animator.SetBool(Enter_targetBool[i], Enter_status[i]);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for (int i = 0; i < Exit_targetBool.Length; i++)
        {
            animator.SetBool(Exit_targetBool[i], Exit_status[i]);
        }
    }

    

}
