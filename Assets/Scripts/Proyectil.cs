using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Proyectil : MonoBehaviour
{
    public GameObject fxExplosion; // El prefab del efecto de explosión
    public AudioClip sonidoImpacto; // El sonido de impacto
    private AudioSource audioSource; // Fuente de audio
    public float tiempoDeVida = 5f; // Tiempo antes de que el proyectil se destruya automáticamente
    public string escenaCambio; // Nombre de la escena a la que cambiar

    void Start()
    {
        // Obtener el componente AudioSource
        audioSource = GetComponent<AudioSource>();

        // Destruye el proyectil después de un tiempo si no colisiona con nada
        Destroy(gameObject, tiempoDeVida);
    }

    void OnCollisionEnter2D(Collision2D colision)
    {
        if (colision.gameObject.CompareTag("Player"))
        {
            // Destruir al jugador
            Destroy(colision.gameObject);

            // Ejecutar el efecto de explosión y el sonido de impacto
            StartCoroutine(ExplosiónYCambioDeEscena());
        }
        else if (colision.gameObject.CompareTag("Ground"))
        {
            // Ejecutar el efecto de explosión y el sonido de impacto
            Explosión();

            // Destruir el proyectil después de un pequeño retraso
            Destroy(gameObject, sonidoImpacto.length);
        }
    }

    IEnumerator ExplosiónYCambioDeEscena()
    {
        // Instanciar el efecto de explosión en la posición del proyectil
        Instantiate(fxExplosion, transform.position, Quaternion.identity);

        // Reproducir el sonido de impacto
        audioSource.PlayOneShot(sonidoImpacto);

        // Esperar hasta que el sonido y el FX hayan terminado
        yield return new WaitForSeconds(sonidoImpacto.length);

        // Cambiar a la escena especificada
        SceneManager.LoadScene(escenaCambio);
    }

    void Explosión()
    {
        // Instanciar el efecto de explosión en la posición del proyectil
        Instantiate(fxExplosion, transform.position, Quaternion.identity);

        // Reproducir el sonido de impacto
        audioSource.PlayOneShot(sonidoImpacto);
    }
}
