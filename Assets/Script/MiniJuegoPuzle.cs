using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class MiniJuegoPuzle : MonoBehaviour
{
    public Sprite[] Niveles;
    public GameObject MenuGanar;
    private int capa = 1;    
    public int PiezasEncajadas = 0;

    void Start()
    {
        for (int i = 0; i < 36; i++)
        {
            GameObject.Find("Pieza (" + i + ")").transform.Find("Puzzle").GetComponent<SpriteRenderer>().sprite = Niveles[PlayerPrefs.GetInt("Nivel")];
        }
    }

    void Update()
    {
        if (PiezasEncajadas == 36)
        {
             SceneManager.UnloadSceneAsync("MiniJuegoPuzzle");
        }
    }

    public int IncrementarCapa()
    {
        return capa++;
    }
}
