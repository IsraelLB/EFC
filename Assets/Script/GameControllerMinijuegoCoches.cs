using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerMinijuegoCoches : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject carToExit;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == carToExit)
        {
            Debug.Log("Puzzle Completo!");
        }
    }
}
