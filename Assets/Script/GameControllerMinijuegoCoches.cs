using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerMinijuegoCoches : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject carToExit;
    public Camera mainCamera;
    public Camera minigameCamera;
    public List<GameObject> objectsToReset = new List<GameObject>();

    private Dictionary<GameObject, TransformData> initialTransforms = new Dictionary<GameObject, TransformData>();

    void Start()
    {
        // Guardar la posición y rotación inicial de los objetos
        foreach (GameObject obj in objectsToReset)
        {
            initialTransforms[obj] = new TransformData(obj.transform);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == carToExit)
        {
            Debug.Log("Puzzle Completo!");
            
            minigameCamera.gameObject.SetActive(false);
            mainCamera.gameObject.SetActive(true);

            Movimiento movimiento = FindObjectOfType<Movimiento>();
                    if (movimiento != null)
                    {
                        movimiento.ReactivarChef();
                    }
            StartCoroutine(WaitAndResetPositions());
        }
    }
    IEnumerator WaitAndResetPositions()
    {
        yield return new WaitForSeconds(1);
        ResetPositions();
    }

    public void ResetPositions()
    {
        foreach (GameObject obj in objectsToReset)
        {
            if (initialTransforms.ContainsKey(obj))
            {
                TransformData initialTransform = initialTransforms[obj];
                obj.transform.position = initialTransform.position;
                obj.transform.rotation = initialTransform.rotation;
            }
            else
            {
                Debug.LogWarning("El objeto " + obj.name + " no tiene una transformada inicial guardada.");
            }
        }
    }

    // Clase auxiliar para almacenar la transformada inicial de un objeto
    private class TransformData
    {
        public Vector3 position;
        public Quaternion rotation;

        public TransformData(Transform transform)
        {
            position = transform.position;
            rotation = transform.rotation;
        }
    }
}
