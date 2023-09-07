using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneLoadManager_sc : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] GameObject loadPanel;
    public void SceneLoad(int scene)
    {
        loadPanel.SetActive(true);
        StartCoroutine(LoadAsync(scene));
    }
    IEnumerator LoadAsync(int scene)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene);
        while (!asyncOperation.isDone)
        {
            slider.value = asyncOperation.progress / 0.9f;
            yield return null;
        }
    }
}
