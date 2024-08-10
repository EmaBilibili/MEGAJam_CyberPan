using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip jumpSound;
    public AudioClip airJumpSound;
    public AudioClip deathSound;
    public AudioClip walkSound;
    public AudioClip dashSound;

    [Header("Audio Source")]
    public AudioSource audioSource; // Este será el audio source principal del jugador.

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isWalking;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Detectar cuando el jugador está caminando
        if (isGrounded && Mathf.Abs(rb.velocity.x) > 0.1f)
        {
            if (!isWalking)
            {
                isWalking = true;
                PlaySound(walkSound);
            }
        }
        else
        {
            isWalking = false;
        }
    }

    public void PlayJumpSound(bool isAirJump = false)
    {
        if (isAirJump)
        {
            PlaySound(airJumpSound);
        }
        else
        {
            PlaySound(jumpSound);
        }
    }

    public void PlayDeathSound()
    {
        PlaySound(deathSound);
    }

    public void PlayDashSound()
    {
        PlaySound(dashSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    // Método para detectar si está en el suelo (esto debería conectarse al sistema de detección del suelo en el script de movimiento)
    public void SetGroundedState(bool grounded)
    {
        isGrounded = grounded;

        if (!grounded)
        {
            isWalking = false;
        }
    }
}