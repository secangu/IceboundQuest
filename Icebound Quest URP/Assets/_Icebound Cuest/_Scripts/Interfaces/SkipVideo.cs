using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;


public class SkipVideo : MonoBehaviour
{
    [SerializeField] VideoPlayer cinematic;
    [SerializeField] int scene;
    void Start()
    {
        cinematic=GetComponent<VideoPlayer>();
        cinematic.loopPointReached += OnVideoEnd;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene(scene);
    }
    private void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene("Fight");
    }

}
