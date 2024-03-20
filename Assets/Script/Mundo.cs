using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mundo : MonoBehaviour
{   
    public int carril = 0;
    public GameObject[] suelos;
    public int suelosDiferencia=10;
    // Start is called before the first frame update
    private void Start()
    {
        for(int i=0;i<suelosDiferencia;i++){
            CrearSuelos();
        }
    }
    public void CrearSuelos(){
        Instantiate(suelos[Random.Range(0,suelos.Length)], Vector3.forward*carril, Quaternion.identity);
        carril++;
    }
}
