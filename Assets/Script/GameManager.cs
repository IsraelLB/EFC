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
    private AudioSource audioSource;

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

     private void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
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
            audioSource.PlayOneShot(sonidoCorrecto);
            paresEncontrados++;
            
            if (paresEncontrados == 8)
            {
                yield return new WaitForSeconds(1f);
                SceneManager.UnloadSceneAsync("MinijuegoMemoria").completed += (op) =>
                {
                    // Reactivar el objeto Chef al finalizar el minijuego
                    Movimiento movimiento = FindObjectOfType<Movimiento>();
                    if (movimiento != null)
                    {
                        movimiento.ReactivarChef();
                    }
                };
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
