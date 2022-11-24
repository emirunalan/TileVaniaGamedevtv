using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 5f;
    Rigidbody2D rb2d;
    PlayerMovement player;
    float direction;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        direction = player.xDirection;
        
    }

    void Update()
    {
        rb2d.velocity = new Vector2(bulletSpeed * direction,0);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        Destroy(gameObject);
    }
}
