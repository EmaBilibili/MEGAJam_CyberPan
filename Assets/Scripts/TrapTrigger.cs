using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    public GameObject trap; // Referencia a la trampa que se activará

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateTrap();
        }
    }

    void ActivateTrap()
    {
        // Aquí activas la trampa, puede ser activando una animación, un script, etc.
        trap.SetActive(true);
        trap.GetComponent<FallingBlock>().Activate();
    }
}