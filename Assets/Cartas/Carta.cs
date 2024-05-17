using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Carta : MonoBehaviour, IPointerClickHandler
{
    public bool bocaArriba = false;
    public Sprite imagenBocaAbajo;
    private Sprite _imagenBocaArriba;
    public AudioClip sonidoVoltear;

    private Image imageComponent;
    private GameManager gameManager;

    private bool seleccionada = false;

    public Sprite ImagenBocaArriba => _imagenBocaArriba;

    private void Start()
    {
        imageComponent = GetComponent<Image>();
        gameManager = FindObjectOfType<GameManager>();
        VoltearBocaAbajo();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!seleccionada && gameManager.puedeSeleccionar)
        {
            Debug.Log("Carta clicada: " + gameObject.name); 
            Seleccionar();
        }
    }

    public void Seleccionar()
    {
        seleccionada = true;
        Voltear();
        gameManager.RegistrarSeleccion(this);
    }

    public void Desseleccionar()
    {
        seleccionada = false;
        VoltearBocaAbajo();
    }

    public void Voltear()
    {
        bocaArriba = !bocaArriba;
        if (bocaArriba)
        {
            imageComponent.sprite = _imagenBocaArriba;
        }
        else
        {
            imageComponent.sprite = imagenBocaAbajo;
        }
        if (sonidoVoltear != null)
        {
        AudioSource.PlayClipAtPoint(sonidoVoltear, Camera.main.transform.position);
        }
    }

    public void VoltearBocaAbajo()
    {
        bocaArriba = false;
        imageComponent.sprite = imagenBocaAbajo;
    }

    public void SetImagenBocaArriba(Sprite nuevaImagen)
    {
        _imagenBocaArriba = nuevaImagen;
    }
}