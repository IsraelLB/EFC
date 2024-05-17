using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // La cámara que el objeto seguirá
    private Vector3 initialOffset; // Desplazamiento inicial con respecto a la cámara

    void Start()
    {
        if (target != null)
        {
            // Calcular y almacenar el desplazamiento inicial
            initialOffset = transform.position - target.position;
        }
        else
        {
            Debug.LogError("Target not assigned for FollowCameraRelative script.");
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Mantener el desplazamiento inicial mientras se mueve con la cámara
            transform.position = target.position + initialOffset;
        }
    }
}