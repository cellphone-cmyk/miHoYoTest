using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager1 : MonoBehaviour
{
    InputController inputHandler;
    Animator anim;
    PlayerLocomotion1 playerLocomotion;
    CameraHandler cameraHandler;

    [Header("Search Distance")]
    public Transform OverlapSpherePlayer;
    public float SearchRadius;
    public float AttackRange;
    public Collider[] colliders;
    public Collider[] s_colliders;

    [Header("Player Flags")]
    public bool isInteracting;
    public bool canDoCombo;
    public bool canDoSpecialCombo;
    public bool isDead;
    public bool isUltimate;
    public bool isDodge;

    [Header("Input Record")]
    public bool forbidInput;
    public bool forbidMovement;
    public float currentInputTime = 0;
    public float lastInputTime = 0;
    public int nextAction;
    public bool cancel;
    

    private void Awake()
    {
        cameraHandler = this.GetComponentInChildren<CameraHandler>();
    }
    void Start()
    {
        inputHandler = GetComponent<InputController>();
        anim = GetComponentInChildren<Animator>();
        playerLocomotion = GetComponent<PlayerLocomotion1>();

    }

    void Update()
    {
        float delta = Time.deltaTime;
        
        isInteracting = anim.GetBool("isInteracting");
        canDoCombo = anim.GetBool("canDoCombo");
        canDoSpecialCombo = anim.GetBool("canDoSpecialCombo");
        isUltimate = anim.GetBool("isUltimate");
        forbidInput = anim.GetBool("forbidInput");
        forbidMovement = anim.GetBool("forbidMovement");
        cancel = anim.GetBool("cancel");

        if (forbidInput == false) //���汣���ڼ䲻�����κ�����
        {
                inputHandler.TickInput(delta, isInteracting);  //����isInteracting��٣������ǻ���ָ���ִ��ָ��
            if(isInteracting == false)
                playerLocomotion.HandleMovement(delta); 
        }
          
        SearchNearEnemy(); //���������ĵ���
        if (isDead && (anim.GetBool("isDie")==false)) anim.SetBool("isDie", true);

        //if (inputHandler.rb_Input == true) //�������
        //{
        //    currentInputTime = Time.time; //��ȡ��ǰ�����ʱ��
        //    if (currentInputTime - lastInputTime > 1) //��ǰ�����ʱ�� - �ϴ������ʱ�� �Ƿ� > ���汣��/��ǰȡ��/�����˳�ʱ��
        //    {
        //        Debug.Log("combo1 J is inputing"); // ִ�ж�Ӧ�������ת������ʲô�������򱣴�����
        //        lastInputTime = currentInputTime; //��ǰ������ʱ��������һ�������ʱ�䡣
        //    } 
        //    else
        //    {
        //        Debug.Log("combo2 J++ is inputing");
        //        lastInputTime = currentInputTime;
        //    }
        //}
    }

    public void SearchNearEnemy()
    {
        colliders = Physics.OverlapSphere(OverlapSpherePlayer.position,AttackRange,1<<LayerMask.NameToLayer("Enemy"));
        s_colliders = Physics.OverlapSphere(OverlapSpherePlayer.position, SearchRadius, 1 << LayerMask.NameToLayer("Enemy"));

        if (colliders.Length <= 0) return;
        if (s_colliders.Length <= 0) return;
        cameraHandler.aimTarget = s_colliders[0].gameObject.transform.Find("EnemyLockOnTransform");

    }

    public void RotateToEnemy()
    {
        if (colliders.Length > 0)
        {
            transform.LookAt(colliders[0].transform.position);
        }
    }

    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;
    }

    private Vector2 input_look;

    private void LateUpdate()
    {
        inputHandler.rb_Input = false;
        inputHandler.rt_Input = false;
        inputHandler.ultimate_skill = false;
        inputHandler.LshiftInput = false;

        if (cameraHandler != null)
        {
            cameraHandler.CameraRotation(inputHandler.mouseX, inputHandler.mouseY,Time.fixedDeltaTime);
        }
    }

}
