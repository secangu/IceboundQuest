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
    void Start()
    {
        cinematic=GetComponent<VideoPlayer>();
        cinematic.loopPointReached += OnVideoEnd;
        StartCoroutine(DisabledText());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene(scene);
    }
    private void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene("Fight");
    }

    IEnumerator DisabledText()
    {
        yield return new WaitForSeconds(5);
        text.SetActive(false);
    }
}
