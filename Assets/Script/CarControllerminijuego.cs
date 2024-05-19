using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerminijuego : MonoBehaviour
{
    public bool isHorizontal;
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 startPos;
    private bool dragging;
    private bool canMove;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        canMove = true; // Permitir el movimiento inicialmente
    }

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        startPos = transform.position;
        dragging = true;
    }

    void OnMouseUp()
    {
        dragging = false;
        Vector3 finalPosition = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));
        rb.MovePosition(finalPosition);
        canMove = true; // Resetear el estado de movimiento al soltar el coche
    }

    void FixedUpdate()
    {
        if (dragging && canMove)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

            if (isHorizontal)
            {
                Vector3 targetPosition = new Vector3(Mathf.Round(curPosition.x), transform.position.y, transform.position.z);
                rb.MovePosition(targetPosition);
            }
            else
            {
                Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, Mathf.Round(curPosition.z));
                rb.MovePosition(targetPosition);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != gameObject)
        {
            canMove = false; // Detener el movimiento al colisionar con otro coche
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject != gameObject)
        {
            canMove = true; // Permitir el movimiento al salir de la colisión
        }
    }
}
