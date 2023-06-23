using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Buttons_sc : MonoBehaviour
{
    [SerializeField]  AudioMixer audioMixer;
    MouseLock_sc mouseLock;


    void Start()
    {
        mouseLock=FindObjectOfType<MouseLock_sc>();
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
        if(mouseLock!=null) mouseLock.enabled=false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }
    public void Restart(string Restart)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(Restart);

    }
    public void Unpause()
    {
        if (mouseLock != null) mouseLock.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }
    public void Exit()
    {
        Application.Quit();
    }
}
