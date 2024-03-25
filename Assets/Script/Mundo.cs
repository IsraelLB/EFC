using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mundo : MonoBehaviour
{   
    public int carril = 0;
    public GameObject[] suelos;
    public int suelosDiferencia=10;
    public GameObject primerSuelo; // Suelo específico para el primer suelo

    // Start is called before the first frame update
    private void Start()
    {
        CrearSuelos();
        for(int i = 1; i < suelosDiferencia; i++) {
            CrearSuelos();
        }
    }

    public void CrearSuelos() {
        if (carril == 0) {
            Instantiate(primerSuelo, Vector3.forward * carril, Quaternion.identity);
        } else {
            Instantiate(suelos[Random.Range(0, suelos.Length)], Vector3.forward * carril, Quaternion.identity);
        }
        carril++;
    }
}
