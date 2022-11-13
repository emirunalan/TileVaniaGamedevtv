using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 30f;
    [SerializeField] float jumpSpeed = 5f;
    Vector2 moveInput;
    Rigidbody2D rb2d;
    SpriteRenderer spRenderer;
    Animator anim;

    
    bool jumpPressed = false;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Run();
        FlipSprite();
        Debug.Log(jumpPressed);
    }

    private void FlipSprite()
    {
        
        if(moveInput.x > 0)
        {
            spRenderer.flipX = false;
        }
        if(moveInput.x < 0)
        {
            spRenderer.flipX = true;
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if(value.isPressed)
        {
            // do stuff
            rb2d.velocity += new Vector2 (0f, jumpSpeed);
        }
    }


    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed * Time.deltaTime, rb2d.velocity.y);
        rb2d.velocity += playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(rb2d.velocity.x) > .01;
        anim.SetBool("isRunning", playerHasHorizontalSpeed);

    }
}
