using UnityEngine;
using UnityEngine.SceneManagement;  // Necesario para cargar escenas

public class ChangeSceneOnTrigger : MonoBehaviour
{
    // Nombre de la escena que deseas cargar
    public string sceneToLoad;

    // Este método se llama cuando otro objeto entra en el trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificamos si el objeto que colisiona tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Cargamos la escena especificada
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}