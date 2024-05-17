using System.Collections.Generic;
using UnityEngine;

public class MinijuegoMemoria : MonoBehaviour
{
    public GameObject cartaPrefab;
    public Sprite[] imagenesCartas;
    public int numRows = 4;
    public int numColumns = 4;

    private List<GameObject> cartas;

    void Start()
    {
        cartas = new List<GameObject>();
        DistribuirCartasAleatoriamente();
    }

    void DistribuirCartasAleatoriamente()
    {
        RectTransform canvasRect = GetComponent<RectTransform>();

        float boardWidth = (numColumns - 1) * 100f;
        float boardHeight = (numRows - 1) * 100f;

        float initialX = -boardWidth / 2f;
        float initialY = boardHeight / 2f;

        List<Sprite> cartasMezcladas = new List<Sprite>(imagenesCartas);
        cartasMezcladas.AddRange(imagenesCartas);
        Shuffle(cartasMezcladas);

        int index = 0;
        for (int fila = 0; fila < numRows; fila++)
        {
            for (int columna = 0; columna < numColumns; columna++)
            {
                GameObject nuevaCarta = Instantiate(cartaPrefab, transform);
                nuevaCarta.GetComponent<Carta>().SetImagenBocaArriba(cartasMezcladas[index]);
                
                float cartaX = initialX + columna * 100f;
                float cartaY = initialY - fila * 100f;

                RectTransform rt = nuevaCarta.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(cartaX, cartaY);

                cartas.Add(nuevaCarta);
                index++;
            }
        }
    }

    void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
