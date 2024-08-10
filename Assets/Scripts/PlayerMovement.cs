using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float airJumpForce = 7f; // Fuerza de salto cuando el personaje está en el aire
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float groundCheckRadius = 0.2f;

    [Header("References")]
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private PlayerSoundManager soundManager;

    private bool isGrounded;
    private bool hasAirJumped;
    private bool isDashing;
    private float dashTime;
    private Vector2 moveInput;
    
    void Start()
    {
        soundManager = GetComponent<PlayerSoundManager>();
    }

    void Update()
    {
        // Handle input
        moveInput.x = Input.GetAxisRaw("Horizontal");
        
        // Jump
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                soundManager.PlayJumpSound(); // Reproducir sonido de salto
                hasAirJumped = false;
            }
            else if (!hasAirJumped)
            {
                rb.velocity = new Vector2(rb.velocity.x, airJumpForce);
                soundManager.PlayJumpSound(true); // Reproducir sonido de salto en el aire
                hasAirJumped = true;
            }
        }
        

        // Dash
        if (Input.GetButtonDown("Fire3") && !isDashing)
        {
            StartDash();
            soundManager.PlayDashSound(); // Reproducir sonido de dash
        }
    }

    void FixedUpdate()
    {
        // Check if grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isDashing)
        {
            rb.velocity = new Vector2(moveInput.x * dashSpeed, rb.velocity.y);
            dashTime -= Time.fixedDeltaTime;
            if (dashTime <= 0)
            {
                isDashing = false;
            }
        }
        else
        {
            // Apply movement
            rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        }
        soundManager.SetGroundedState(isGrounded); // Actualizar estado del suelo para el sonido
    }

    void StartDash()
    {
        isDashing = true;
        dashTime = dashDuration;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
