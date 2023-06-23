using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Buttons : MonoBehaviour
{
    [SerializeField]  AudioMixer audioMixer;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void GameScene(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }
    public void ChangeVolume(float volume)
    {
        audioMixer.SetFloat("Volume",volume);
    }
    public void ChangeVolumeSFX(float volumeSfx)
    {
        audioMixer.SetFloat("Effects", volumeSfx);
    }
    public void Select(AudioSource audioSource)
    {
        audioSource.Play();
    }
    public void Pause()
    {
        Time.timeScale = 0f;
    }
    public void Restart(string Restart)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(Restart);

    }
    public void Unpause()
    {
        Time.timeScale = 1f;
    }
    public void Exit()
    {
        Application.Quit();
    }
}
