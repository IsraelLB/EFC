using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LanzarRayo();
    }

    // Update is called once per frame
    public void LanzarRayo()  
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position + Vector3.up*3-Vector3.forward,Vector3.down);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "hoja")
            {
                Destroy(hit.transform.gameObject);  //para que no haya obstaculos en el camino
            }
        }
    }
}
