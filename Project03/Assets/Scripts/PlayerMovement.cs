﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 25f;
    public bool hasJumpPotion = false;
    public bool hasShootPotion = false;
    public int potionModAmount = 0;

    public AudioClip jumpClip;

    private float potionTimeMax = 10f;
    private float potionTimeCur = 0f;

    float horizontalMove = 0f;

    bool jumpFlag = false;
    bool jump = false;

    public GameObject bullet;
    private Rigidbody2D rb;

    void Start()
    {
        GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if(jumpFlag)
        {
            jumpFlag = false;
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Jump"))
        {
            if(animator.GetBool("IsJumping") == false)
            {
                AudioSource.PlayClipAtPoint(jumpClip, transform.position);
                jump = true;
                animator.SetBool("IsJumping", true);
            }
        }
    }

    public void onLanding()
    {
        animator.SetBool("IsJumping", false);
        jump = false;
    }

    void FixedUpdate()
    {
        if(hasJumpPotion && potionTimeCur < potionTimeMax)
        {
            controller.m_JumpForceMod = potionModAmount;
            potionTimeCur += Time.fixedDeltaTime;
        }
        else
        {
            potionTimeCur = 0f;
            controller.m_JumpForceMod = 0;
            hasJumpPotion = false;
        }

        if (hasShootPotion && potionTimeCur < potionTimeMax)
        {
            potionTimeCur += Time.fixedDeltaTime;
            Shoot();
        }
        else
        {
            potionTimeCur = 0f;
            hasShootPotion = false;
        }


        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);


        if(jump)
        {
            jumpFlag = true;
        }
    }

    void Shoot()
    { 
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            GameObject.Instantiate(bullet, transform.position, transform.rotation);
        }

    }

}
