using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float jump_height = 5f;
    private bool isGrounded = false;

    public Transform groundCheck; // assign this in the inspector
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    private Rigidbody2D rb2d;

    public int maxHealth = 5;

    public int currentHealth;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
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

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered with: " + other.gameObject.name);

        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Triggered with an Enemy!");
            currentHealth -= 1;
        }
    }

    

    void Die()
    {
        Debug.Log("Player Dead");
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



}


//this comment is to help find changed code