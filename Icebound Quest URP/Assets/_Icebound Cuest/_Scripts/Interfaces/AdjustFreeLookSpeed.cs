using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class ControlFreeLookSpeed : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook freeLookCamera;
    [SerializeField] Slider _horizontalSlider;
    [SerializeField] Slider _verticalSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("RotationSpeedX") == false) PlayerPrefs.SetFloat("RotationSpeedX", 300);
        if (PlayerPrefs.HasKey("RotationSpeedY") == false) PlayerPrefs.SetFloat("RotationSpeedY", 2);
        // Cargar los valores guardados
        float savedRotationSpeedX = PlayerPrefs.GetFloat("RotationSpeedX", _horizontalSlider.value);
        float savedRotationSpeedY = PlayerPrefs.GetFloat("RotationSpeedY", _verticalSlider.value);

        // Configurar los sliders con los valores
        _horizontalSlider.value = savedRotationSpeedX;
        _verticalSlider.value = savedRotationSpeedY;
    }

    private void Update()
    {
        if (freeLookCamera != null && _horizontalSlider != null && _verticalSlider != null)
        {
            // Ajustar la velocidad de rotación
            freeLookCamera.m_XAxis.m_MaxSpeed = _horizontalSlider.value;
            freeLookCamera.m_YAxis.m_MaxSpeed = _verticalSlider.value;

            // Guardar los valores en PlayerPrefs
            PlayerPrefs.SetFloat("RotationSpeedX", _horizontalSlider.value);
            PlayerPrefs.SetFloat("RotationSpeedY", _verticalSlider.value);
        }
    }
}
