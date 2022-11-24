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
    [SerializeField] Vector2 deathKick = new Vector2(0,10f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    Vector2 moveInput;
    public float xDirection;
    Rigidbody2D rb2d;
    SpriteRenderer spRenderer;
    Animator anim;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D feetCollider;
    float gravityScaleAtStart;
    bool isAlive = true;

    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = rb2d.gravityScale;
    }

    void Update()
    {
        if(!isAlive) return;
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    private void FlipSprite()
    {
        
        if(moveInput.x > 0)
        {
            spRenderer.flipX = false;
            xDirection = 1;
        }
        if(moveInput.x < 0)
        {
            spRenderer.flipX = true;
            xDirection = -1;
        }
    }

    void OnMove(InputValue value)
    {
        if(!isAlive) return;
        moveInput = value.Get<Vector2>();
    }



    void OnJump(InputValue value)
    {
        if(!isAlive) return;
        if(!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;
        
        if(value.isPressed)
        {
            // do stuff
            rb2d.velocity += new Vector2 (0f, jumpSpeed);
            //rb2d.AddForce(new Vector2(0f,jumpSpeed));
        }
    }

    void OnFire(InputValue value)
    {
        if(!isAlive) return;
        Instantiate(bullet,gun.position,transform.rotation);
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
        if(!feetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
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


    void Die()
    {
        if(bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies","Hazards")))
        {
            isAlive = false;
            anim.SetTrigger("Die");
            rb2d.velocity = deathKick;
        }
    }



}
