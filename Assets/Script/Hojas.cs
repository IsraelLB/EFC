using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hojas : MonoBehaviour
{
    public GameObject nenufar;
    // Start is called before the first frame update
    void Start()
    {
        LanzarRayo();
    }


    public void LanzarRayo()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position + Vector3.up*3-Vector3.forward,Vector3.down);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "Agua")  //para que haya siempre un camino de hojas
            {
                Instantiate(nenufar, transform.position - Vector3.forward , transform.rotation);
            }else if (hit.collider.gameObject.tag == "obstaculo"){
                Destroy(hit.transform.gameObject);  //para que no haya obstaculos en el camino
            }
        }
    }
}
