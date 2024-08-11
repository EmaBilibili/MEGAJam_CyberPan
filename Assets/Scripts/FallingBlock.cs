using UnityEngine;
using UnityEngine.SceneManagement; // Importar la librería para la gestión de escenas

public class FallingBlock : MonoBehaviour
{
    public float fallDelay = 1f;
    public float duration = 5f; // Tiempo de duración antes de que el bloque se destruya
    public string sceneToLoad = "YourSceneName"; // Nombre de la escena a cargar
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }

    public void Activate()
    {
        Invoke("DropBlock", fallDelay);
    }

    void DropBlock()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        Invoke("DestroyBlock", duration); // Destruir el bloque después de la duración
    }

    void DestroyBlock()
    {
        Destroy(gameObject);
    }

    // Método que se ejecuta cuando el bloque colisiona con otro objeto
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si el objeto que tocó el bloque tiene el tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Cargar la escena especificada
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    // Alternativamente, si quieres usar un Trigger en lugar de una colisión física:
    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto que entró en el trigger tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Cargar la escena especificada
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}