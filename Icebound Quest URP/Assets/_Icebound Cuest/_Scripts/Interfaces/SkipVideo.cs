using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;


public class SkipVideo : MonoBehaviour
{
    [SerializeField] VideoPlayer cinematic;
    [SerializeField] int scene;
    [SerializeField] GameObject text;
    SceneLoadManager_sc sceneLoadManager;
    void Start()
    {
        sceneLoadManager=FindObjectOfType<SceneLoadManager_sc>();
        cinematic=GetComponent<VideoPlayer>();
        cinematic.loopPointReached += OnVideoEnd;
        StartCoroutine(DisabledText());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cinematic.Pause();
            sceneLoadManager.SceneLoad(scene);
            Destroy(this.gameObject);
        }
    }
    private void OnVideoEnd(VideoPlayer vp)
    {
        cinematic.Pause();
        sceneLoadManager.SceneLoad(scene);
        Destroy(this.gameObject);
    }

    IEnumerator DisabledText()
    {
        yield return new WaitForSeconds(5);
        text.SetActive(false);
    }
}
