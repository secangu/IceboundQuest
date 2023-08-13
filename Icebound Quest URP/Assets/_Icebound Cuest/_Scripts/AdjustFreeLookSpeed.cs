using UnityEngine;
using Cinemachine;

public class ControlFreeLookSpeed : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook freeLookCamera;
    [SerializeField] private float rotationSpeedX = 300.0f;
    [SerializeField] private float rotationSpeedY = 2.0f;
    [SerializeField] private float zoomSpeed = 1.0f;

    private void Update()
    {
        if (freeLookCamera != null)
        {
            // Ajustar la velocidad de rotación
            freeLookCamera.m_XAxis.m_MaxSpeed = rotationSpeedX;
            freeLookCamera.m_YAxis.m_MaxSpeed = rotationSpeedY;

            // Ajustar la velocidad de zoom
            //foreach (var orbit in freeLookCamera.m_Orbits)
            //{
            //    orbit.m_HeightSpeed = zoomSpeed;
            //}
        }
    }
}
