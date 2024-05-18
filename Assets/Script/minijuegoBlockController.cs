using UnityEngine;

public class minijuegoBlockController : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Implementa la lógica para mover el bloque aquí
        }
    }
}
