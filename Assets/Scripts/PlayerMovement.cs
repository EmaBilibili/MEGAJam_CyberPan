using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float airJumpForce = 7f;
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float groundCheckRadius = 0.2f;

    [Header("References")]
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Collider2D playerCollider; // Referencia al Collider del jugador

    private bool isGrounded;
    private bool hasAirJumped;
    private bool isDashing;
    private bool isDroppingThrough;
    private float dashTime;
    private Vector2 moveInput;
    
    private PlayerSoundManager soundManager;

    public Animator animator; // Referencia al componente Animator

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        soundManager = GetComponent<PlayerSoundManager>();
        animator = GetComponent<Animator>(); // Obtener la referencia del Animator
    }

    void Update()
{
    moveInput.x = Input.GetAxisRaw("Horizontal");

    // Check if grounded
    isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

    if (isGrounded)
    {
        hasAirJumped = false; // Reset double jump if grounded
    }

    // Set animator parameters
    animator.SetFloat("Speed", Mathf.Abs(moveInput.x)); // Actualizar velocidad
    animator.SetBool("IsGrounded", isGrounded);

    // Jump
    if (Input.GetButtonDown("Jump"))
    {
        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.S)) // Detectar si está presionando "S" + "Espacio"
            {
                StartCoroutine(DropThroughPlatform());
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                soundManager.PlayJumpSound(); // Reproducir sonido de salto
                animator.SetBool("IsJumping", true);
            }
        }
        else if (!hasAirJumped)
        {
            rb.velocity = new Vector2(rb.velocity.x, airJumpForce);
            soundManager.PlayJumpSound(); // Reproducir sonido de salto
            hasAirJumped = true;
            animator.SetBool("IsJumping", true);
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
    if (isDashing)
    {
        rb.velocity = new Vector2(moveInput.x * dashSpeed, rb.velocity.y);
        dashTime -= Time.fixedDeltaTime;
        if (dashTime <= 0)
        {
            isDashing = false;
        }
    }
    else if (!isDroppingThrough) // Evitar movimiento mientras se cae a través de la plataforma
    {
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
    }
    soundManager.SetGroundedState(isGrounded); // Actualizar estado del suelo para el sonido

    // Reset jumping animation when grounded
    if (isGrounded && rb.velocity.y <= 0)
    {
        animator.SetBool("IsJumping", false);
    }
}

    void StartDash()
    {
        isDashing = true;
        dashTime = dashDuration;
    }

    IEnumerator DropThroughPlatform()
    {
        isDroppingThrough = true;
        playerCollider.enabled = false; // Desactivar el collider para atravesar la plataforma
        yield return new WaitForSeconds(0.5f); // Esperar un breve momento
        playerCollider.enabled = true; // Reactivar el collider
        isDroppingThrough = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
