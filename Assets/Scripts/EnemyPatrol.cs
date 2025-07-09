using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Settings")]
    public float moveSpeed = 2f;
    public Transform groundCheck;
    public float groundCheckDistance = 1f;
    public LayerMask groundLayer;
    public LayerMask wallLayer;

    private Rigidbody2D rb;
    private bool movingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        // Move in the current direction
        rb.velocity = new Vector2((movingRight ? 1 : -1) * moveSpeed, rb.velocity.y);

        // Ground check ahead
        Vector2 groundCheckPos = new Vector2(groundCheck.position.x, groundCheck.position.y);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheckPos, Vector2.down, groundCheckDistance, groundLayer);

        // Wall check ahead
        Vector2 wallCheckDirection = movingRight ? Vector2.right : Vector2.left;
        RaycastHit2D wallInfo = Physics2D.Raycast(groundCheckPos, wallCheckDirection, 0.1f, wallLayer);

        // Flip if no ground ahead or wall detected
        if (!groundInfo || wallInfo)
        {
            Flip();
        }
    }

    void Flip()
    {
        movingRight = !movingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);

            Gizmos.color = Color.yellow;
            Vector3 wallDir = movingRight ? Vector3.right : Vector3.left;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + wallDir * 0.1f);
        }
    }
}
