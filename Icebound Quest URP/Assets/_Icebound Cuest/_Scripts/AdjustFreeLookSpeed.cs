using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class ControlFreeLookSpeed : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook freeLookCamera;
    [SerializeField] Slider rotationSpeedXSlider;
    [SerializeField] Slider rotationSpeedYSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("RotationSpeedX") == false) PlayerPrefs.SetFloat("RotationSpeedX", 200);
        if (PlayerPrefs.HasKey("RotationSpeedY") == false) PlayerPrefs.SetFloat("RotationSpeedY", 2);
        // Cargar los valores guardados
        float savedRotationSpeedX = PlayerPrefs.GetFloat("RotationSpeedX", rotationSpeedXSlider.value);
        float savedRotationSpeedY = PlayerPrefs.GetFloat("RotationSpeedY", rotationSpeedYSlider.value);

        // Configurar los sliders con los valores
        rotationSpeedXSlider.value = savedRotationSpeedX;
        rotationSpeedYSlider.value = savedRotationSpeedY;
    }

    private void Update()
    {
        if (freeLookCamera != null && rotationSpeedXSlider != null && rotationSpeedYSlider != null)
        {
            // Ajustar la velocidad de rotación
            freeLookCamera.m_XAxis.m_MaxSpeed = rotationSpeedXSlider.value;
            freeLookCamera.m_YAxis.m_MaxSpeed = rotationSpeedYSlider.value;

            // Guardar los valores en PlayerPrefs
            PlayerPrefs.SetFloat("RotationSpeedX", rotationSpeedXSlider.value);
            PlayerPrefs.SetFloat("RotationSpeedY", rotationSpeedYSlider.value);
        }
    }
}
