using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    public float jump_height = 5f;
    private bool isGrounded = false;

    public Transform groundCheck; // assign this in the inspector
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check if on ground using OverlapCircle
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        float move_Horizontal = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(move_Horizontal * speed, rb2d.velocity.y);

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
        {
            rb2d.AddForce(new Vector2(0, jump_height), ForceMode2D.Impulse);
        }
    }

    void OnDrawGizmosSelected()
    {
        // This just draws a wire circle in the editor so you can visualize the ground check area
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }


    // void FixedUpdate()
    // {
    //     Vector2 Movement = new Vector2(0f,0f);

    //     float move_Horizontal = Input.GetAxis("Horizontal");
    //     if (move_Horizontal > 0)
    //     {
    //         {
    //             rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
    //         }

    //     }
    //     if (move_Horizontal < 0)
    //     {
    //         rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
    //     }
    //     if (Input.GetKeyDown(KeyCode.W) || (Input.GetKeyDown(KeyCode.UpArrow)))
    //     {
    //         if(in_air == false)
    //         {
    //             rb2d.AddForce(new Vector2(0, jump_height), ForceMode2D.Impulse);
    //         }
    //     }
    // }

}


//this comment is to help find changed code