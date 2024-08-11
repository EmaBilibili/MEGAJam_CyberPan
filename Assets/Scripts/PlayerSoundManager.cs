using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip jumpSound;
    public AudioClip airJumpSound;
    public AudioClip deathSound;
    public AudioClip walkSound;
    public AudioClip dashSound;

    [Header("Audio Sources")]
    public AudioSource mainAudioSource; // Este será el audio source principal del jugador.
    public AudioSource walkAudioSource; // Este será un audio source dedicado para el sonido de caminar.

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isWalking;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Configurar el AudioSource de caminar para que se reproduzca en loop
        if (walkAudioSource != null)
        {
            walkAudioSource.clip = walkSound;
            walkAudioSource.loop = true;
        }
    }

    void Update()
    {
        // Detectar cuando el jugador está caminando
        if (isGrounded && Mathf.Abs(rb.velocity.x) > 0.1f)
        {
            if (!isWalking)
            {
                isWalking = true;
                PlayWalkSound();
            }
        }
        else
        {
            if (isWalking)
            {
                isWalking = false;
                StopWalkSound();
            }
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
            mainAudioSource.PlayOneShot(clip);
        }
    }

    private void PlayWalkSound()
    {
        if (walkAudioSource != null && !walkAudioSource.isPlaying)
        {
            walkAudioSource.Play();
        }
    }

    private void StopWalkSound()
    {
        if (walkAudioSource != null && walkAudioSource.isPlaying)
        {
            walkAudioSource.Pause(); // Puedes usar Pause si planeas reanudar o Stop si prefieres que comience de nuevo.
        }
    }

    // Método para detectar si está en el suelo (esto debería conectarse al sistema de detección del suelo en el script de movimiento)
    public void SetGroundedState(bool grounded)
    {
        isGrounded = grounded;

        if (!grounded)
        {
            isWalking = false;
            StopWalkSound();
        }
    }
}
