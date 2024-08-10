using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    public float fallDelay = 1f;
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
    }
}