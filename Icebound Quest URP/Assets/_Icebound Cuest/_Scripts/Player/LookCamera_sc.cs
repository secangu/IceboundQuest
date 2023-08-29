using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCamera_sc : MonoBehaviour
{
    private Transform mainCameraTransform;

    private void Start()
    {
        mainCameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        Vector3 cameraDirection = Camera.main.transform.forward;

        // Rota el objeto para que mire hacia la direcci�n de la c�mara
        transform.LookAt(transform.position + cameraDirection);
    }
}
