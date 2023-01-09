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

        inputHandler.TickInput(delta); //ªÒ»° ‰»Î
        playerLocomotion.HandleMovement(delta);

        SearchNearEnemy();
        if (isDead && (anim.GetBool("isDie")==false)) anim.SetBool("isDie", true);

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
