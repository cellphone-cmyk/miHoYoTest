using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SG;
public class PlayerManager1 : MonoBehaviour
{
    InputController inputHandler;
    Animator anim;
    PlayerLocomotion1 playerLocomotion;
    CameraHandler cameraHandler;


    [Header("Player Flags")]
    public bool isInteracting;
    public bool canDoCombo;

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

        inputHandler.TickInput(delta);
        playerLocomotion.HandleMovement(delta);

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

        if (cameraHandler != null)
        {
            cameraHandler.CameraRotation(inputHandler.mouseX, inputHandler.mouseY,Time.fixedDeltaTime);
        }
    }

}
