using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class devCoches : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coche"))
        {
            other.transform.Translate(0, 0, -13);
        }
    }
}

