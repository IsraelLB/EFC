using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class MiniJuegoPuzle : MonoBehaviour
{
    public Sprite[] Niveles;

    public Camera mainCamera;
    public Camera minigamePuzzle;
      private Vector3 PosicionCorrecta;
    public GameObject MenuGanar;
    public GameObject PiezaSeleccionada;
    int capa = 1;    
    public int PiezasEncajadas = 0;

    void Start()
    {
        for (int i = 0;i < 36; i++)
        {
            GameObject.Find("Pieza (" + i + ")").transform.Find("Puzzle").GetComponent<SpriteRenderer>().sprite = Niveles[PlayerPrefs.GetInt("Nivel")];
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.transform.CompareTag("Puzzle"))
            {
                if (!hit.transform.GetComponent<pieza>().Encajada)
                {
                    PiezaSeleccionada = hit.transform.gameObject;
                    PiezaSeleccionada.GetComponent<pieza>().Seleccionada = true;
                    PiezaSeleccionada.GetComponent<SortingGroup>().sortingOrder = capa;
                    capa++;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (PiezaSeleccionada != null)
            {
                PiezaSeleccionada.GetComponent<pieza>().Seleccionada = false;
                PiezaSeleccionada = null;
            }
        }
        if (PiezaSeleccionada != null)
        {
            Vector3 raton = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PiezaSeleccionada.transform.position = new Vector3(raton.x,raton.y,0);
        }             
        if (PiezasEncajadas == 36)
        {
            minigamePuzzle.gameObject.SetActive(false);
            mainCamera.gameObject.SetActive(true);
            Movimiento movimiento = FindObjectOfType<Movimiento>();
                    if (movimiento != null)
                    {
                        movimiento.ReactivarChef();
                    }
          ResetearPiezas();
            
        }
    }

void ResetearPiezas()
{
    // Restablecer las posiciones de todas las piezas al valor inicial
    for (int i = 0; i < 36; i++)
    {
        float randomX = Random.Range(554f, 561f);
        float randomY = Random.Range(273f, 279f);
        GameObject pieza = GameObject.Find("Pieza (" + i + ")");
        pieza.transform.position = new Vector3(randomX, randomY, 0);

        // Restablecer el estado de la pieza
        pieza.GetComponent<pieza>().Encajada = false;
        pieza.GetComponent<pieza>().Seleccionada = false;
    }

    // Restablecer otras variables necesarias
    PiezasEncajadas = 0;
    PiezaSeleccionada = null;
    capa = 1;
}



}