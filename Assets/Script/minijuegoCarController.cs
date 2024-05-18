using UnityEngine;

public class minijuegoCarController : MonoBehaviour
{
    public float speed = 5f; // Velocidad del coche

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);

        GetComponent<Rigidbody>().velocity = movement * speed;
    }
}