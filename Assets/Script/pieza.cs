using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class pieza : MonoBehaviour
{
    private Vector3 PosicionCorrecta;
    public bool Encajada;
    public bool Seleccionada;
    private Vector3 offset;
    private Camera mainCamera;

    // Velocidad de arrastre
    public float velocidadArrastre = 10f;
    private Vector3 targetPosition;

    void Start()
    {
        PosicionCorrecta = transform.position;
        mainCamera = Camera.main;

        float randomX = Random.Range(554f, 561f);
        float randomY = Random.Range(273f, 279f);

        transform.position = new Vector3(randomX, randomY, 0);
    }

    void OnMouseDown()
    {
        if (!Encajada)
        {
            Seleccionada = true;
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            offset = transform.position - new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
            GetComponent<SortingGroup>().sortingOrder = FindObjectOfType<MiniJuegoPuzle>().IncrementarCapa();
        }
    }

    void OnMouseDrag()
    {
        if (Seleccionada)
        {
            // Obtiene la posición del ratón en el mundo
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            // Calcula la posición objetivo con la velocidad
            targetPosition = new Vector3(mousePosition.x + offset.x, mousePosition.y + offset.y, transform.position.z);
            // Aplica la posición objetivo de manera suavizada
            transform.position = Vector3.Lerp(transform.position, targetPosition, velocidadArrastre * Time.deltaTime);
        }
    }

    void OnMouseUp()
    {
        if (Seleccionada)
        {
            Seleccionada = false;

            if (Vector3.Distance(transform.position, PosicionCorrecta) < 0.5f)
            {
                transform.position = PosicionCorrecta;
                Encajada = true;
                GetComponent<SortingGroup>().sortingOrder = 0;
                FindObjectOfType<MiniJuegoPuzle>().PiezasEncajadas++;
            }
        }
    }
}
