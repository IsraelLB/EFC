using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class pieza : MonoBehaviour
{
    private Vector3 PosicionCorrecta;
    public bool Encajada = false;
    public bool Seleccionada = false;
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

    void Update()
    {
        if (Vector3.Distance(transform.position, PosicionCorrecta) < 0.5f)
        {
            if (!Seleccionada)
            {
                if (Encajada == false)
                {
                    transform.position = PosicionCorrecta;
                    Encajada = true;
                    GetComponent<SortingGroup>().sortingOrder = 0;
                    Camera.main.GetComponent<MiniJuegoPuzle>().PiezasEncajadas++;
                }
            }
        }
    }




}
