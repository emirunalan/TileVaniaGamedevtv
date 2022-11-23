using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 30f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    Vector2 moveInput;
    Rigidbody2D rb2d;
    SpriteRenderer spRenderer;
    Animator anim;
    CapsuleCollider2D capsCollider;
    float gravityScaleAtStart;

    
    bool jumpPressed = false;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        capsCollider = GetComponent<CapsuleCollider2D>();
        gravityScaleAtStart = rb2d.gravityScale;
    }

    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
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
        if(!capsCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;
        
        if(value.isPressed)
        {
            // do stuff
            rb2d.velocity += new Vector2 (0f, jumpSpeed);
            //rb2d.AddForce(new Vector2(0f,jumpSpeed));
        }
    }


    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed * Time.deltaTime, rb2d.velocity.y);
        rb2d.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(rb2d.velocity.x) > .1;
        anim.SetBool("isRunning", playerHasHorizontalSpeed);

    }

    void ClimbLadder()
    {
        if(!capsCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rb2d.gravityScale = gravityScaleAtStart;
            anim.SetBool("isClimbing", false);
            return;
        }
        Vector2 climbVelocity = new Vector2(rb2d.velocity.x, moveInput.y * climbSpeed * Time.deltaTime);
        rb2d.velocity = climbVelocity;
        rb2d.gravityScale = 0;
        bool playerHasVerticalSpeed = Mathf.Abs(rb2d.velocity.y) > .1;
        anim.SetBool("isClimbing", playerHasVerticalSpeed);
    }
}
