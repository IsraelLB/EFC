using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public AudioClip sonidoCorrecto;

    private Carta primeraCarta;
    private Carta segundaCarta;
    public bool puedeSeleccionar = true;
    private int paresEncontrados = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegistrarSeleccion(Carta cartaSeleccionada)
    {
        if (primeraCarta == null)
        {
            primeraCarta = cartaSeleccionada;
        }
        else if (segundaCarta == null)
        {
            segundaCarta = cartaSeleccionada;
            StartCoroutine(CheckMatchCoroutine());
        }
    }

    private IEnumerator CheckMatchCoroutine()
    {
        puedeSeleccionar = false;

        yield return new WaitForSeconds(1f);

        if (primeraCarta.ImagenBocaArriba == segundaCarta.ImagenBocaArriba)
        {
            primeraCarta.GetComponent<Button>().interactable = false;
            segundaCarta.GetComponent<Button>().interactable = false;
            paresEncontrados++;
            if (sonidoCorrecto != null)
            {
            AudioSource.PlayClipAtPoint(sonidoCorrecto, Camera.main.transform.position);
            }
            if (paresEncontrados == 8)
            {
                SceneManager.UnloadSceneAsync("minijuegoMemoria");
            }
        }
        else
        {
            primeraCarta.Desseleccionar();
            segundaCarta.Desseleccionar();
        }

        primeraCarta = null;
        segundaCarta = null;
        puedeSeleccionar = true;
    }
}
