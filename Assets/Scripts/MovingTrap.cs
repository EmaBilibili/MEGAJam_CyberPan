using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingTrap : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    private bool movingToB = true;
    public string sceneToLoad = "GameOverScene"; // Nombre de la escena a cargar

    void Update()
    {
        // Mover la trampa entre los puntos A y B
        if (movingToB)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);

            // Flip cuando se mueve hacia B
            if (transform.position.x < pointB.position.x)
            {
                transform.localScale = new Vector3(-.4f, .4f, .4f); // Normal
            }
            else
            {
                transform.localScale = new Vector3(.4f, .4f, .4f); // Flip
            }

            if (Vector2.Distance(transform.position, pointB.position) < 0.1f)
            {
                movingToB = false;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);

            // Flip cuando se mueve hacia A
            if (transform.position.x > pointA.position.x)
            {
                transform.localScale = new Vector3(.4f, .4f, .4f); // Flip
            }
            else
            {
                transform.localScale = new Vector3(-.4f, .4f, .4f); // Normal
            }

            if (Vector2.Distance(transform.position, pointA.position) < 0.1f)
            {
                movingToB = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Animator playerAnimator = other.GetComponent<Animator>();

            if (playerAnimator != null)
            {
                playerAnimator.SetTrigger("Die"); // Activa la animación de muerte
                StartCoroutine(WaitForDeathAnimation(playerAnimator));
            }

            // Desactivar los controles del jugador si es necesario
            // other.GetComponent<PlayerController>().enabled = false; // Ejemplo de desactivar el script de control del jugador
        }
    }

    private IEnumerator WaitForDeathAnimation(Animator playerAnimator)
    {
        // Esperar hasta que la animación de muerte termine
        while (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Death") && 
               playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }

        // Cargar la escena especificada
        SceneManager.LoadScene(sceneToLoad);
    }
}
