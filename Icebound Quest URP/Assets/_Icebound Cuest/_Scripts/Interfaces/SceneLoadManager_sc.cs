using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadManager_sc : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] GameObject loadPanel;
    InterfaceController_sc interfaceController;

    void Start()
    {
        interfaceController=GetComponent<InterfaceController_sc>();
    }
    public void SceneLoad(int scene)
    {
        Time.timeScale = 1;
        loadPanel.SetActive(true);
        StartCoroutine(LoadAsync(scene));
        if(interfaceController!=null)interfaceController.enabled = false;
    }

    IEnumerator LoadAsync(int scene)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene);
        asyncOperation.allowSceneActivation = false; // Evitar que la escena se active autom�ticamente

        float targetProgress = 0.9f; // El progreso m�ximo que deseas alcanzar
        float currentProgress = 0.0f; // El progreso actual

        while (currentProgress < targetProgress)
        {
            // Incrementa el progreso de manera lenta
            currentProgress += 0.005f; // Ajusta  para cambiar la velocidad

            // Aseg�rate de que el progreso no supere el objetivo
            currentProgress = Mathf.Clamp01(currentProgress);

            // Actualiza el valor del slider y espera un tiempo
            slider.value = currentProgress / targetProgress;
            yield return new WaitForSeconds(0.01f); // Ajusta el tiempo de espera seg�n tu preferencia
        }

        asyncOperation.allowSceneActivation = true; // Permite que la escena se active cuando est� lista
    }
}
