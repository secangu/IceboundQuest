using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;


public class SkipVideo : MonoBehaviour
{
    [SerializeField] VideoPlayer cinematic;
    void Start()
    {
        cinematic=GetComponent<VideoPlayer>();
        cinematic.loopPointReached += OnVideoEnd;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.L)) SceneManager.LoadScene("Fight");
    }
    private void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene("Fight");
    }

}
