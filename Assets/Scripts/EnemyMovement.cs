using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D rb2d;
    BoxCollider2D turnCollider;
    Transform xform;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        turnCollider = GetComponent<BoxCollider2D>();
        xform = GetComponent<Transform>();
         
    }

    void Update()
    {
        rb2d.velocity = new Vector2(moveSpeed, 0f);
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        moveSpeed *= -1;
        FlipEnemyFacing();
    }

    private void FlipEnemyFacing()
    {
        xform.localScale = new Vector3(-xform.localScale.x,xform.localScale.y,xform.localScale.z);
    }
}
