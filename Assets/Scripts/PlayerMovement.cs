using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public Transform wallCheck;
    public float groundCheckRadius = 0.2f;
    public float wallCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public float attackRange = 0.5f;
    public int attackDamage = 20;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    [Header("Health")]
    public int maxHealth = 100;
    private int currentHealth;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isOnWall;
    private bool facingRight = true;
    private HealthBar healthBar;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

        healthBar = FindObjectOfType<HealthBar>();
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isOnWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, wallLayer);

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                // Regular jump
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else if (isOnWall && !isGrounded)
            {
                // Wall jump
                float wallJumpDirection = transform.localScale.x > 0 ? -1 : 1;
                rb.velocity = new Vector2(wallJumpDirection * moveSpeed, jumpForce);

                // Optional: flip the character after wall jump
            }
        }
        if (Input.GetKeyDown(KeyCode.E)) // Or whatever key you want
        {
             Attack();
        }

    }

    

    void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Flip the player if the direction changed
        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }
    }
    

    void Flip()
{
    facingRight = !facingRight;
    Vector3 scaler = transform.localScale;
    scaler.x *= -1;
    transform.localScale = scaler;
}

    void LateUpdate()
    {
        // Lock Z position
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }

    void Attack()
    {
        

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>()?.TakeDamage(attackDamage);
        }
    }




    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
        else if (attackPoint != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }

    // Call this method to apply damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took damage. Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene("Death");

    
}
}
