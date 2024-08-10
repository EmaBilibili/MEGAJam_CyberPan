using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_UXScript : MonoBehaviour
{
    private bool isPaused = false;  // Estado de pausa del juego
    public GameObject pausePanel;   // Panel que se muestra cuando el juego está en pausa
    
    public void PlayLore()
    {
        SceneManager.LoadScene("LoreScene");
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    public void QuitMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    // Función que se llama cuando el botón "Pausar" es presionado
    public void PauseGame()
    {
        Time.timeScale = 0f;  // Detiene el tiempo del juego
        isPaused = true;
        pausePanel.SetActive(true);  // Mostrar el panel de pausa
    }

    // Función que se llama cuando el botón "Reanudar" es presionado
    public void ResumeGame()
    {
        Time.timeScale = 1f;  // Restaura el tiempo del juego
        isPaused = false;
        pausePanel.SetActive(false);  // Ocultar el panel de pausa
    }
}