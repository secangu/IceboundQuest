using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Buttons_sc : MonoBehaviour
{
    [SerializeField]  AudioMixer _audioMixer;
    MouseLock_sc mouseLock;
    [SerializeField] Slider _sliderGeneralValue;
    [SerializeField] Slider _sliderMusicValue;
    [SerializeField] Slider _sliderEffectsValue;

    float _generalVolume;
    float _musicVolume;
    float _effectsVolume;

    void Start()
    {
        mouseLock=FindObjectOfType<MouseLock_sc>();
        if (PlayerPrefs.HasKey("VolumenGeneral") == false) PlayerPrefs.SetFloat("VolumenGeneral", 1.5f);
        if (PlayerPrefs.HasKey("VolumenMusica") == false) PlayerPrefs.SetFloat("VolumenMusica", 1.5f);
        if (PlayerPrefs.HasKey("VolumenEfectos") == false) PlayerPrefs.SetFloat("VolumenEfectos", 5f);

        _generalVolume = PlayerPrefs.GetFloat("VolumenGeneral");
        _musicVolume = PlayerPrefs.GetFloat("VolumenMusica");
        _effectsVolume = PlayerPrefs.GetFloat("VolumenEfectos");

        _sliderGeneralValue.value = _generalVolume;
        _sliderMusicValue.value = _musicVolume;
        _sliderEffectsValue.value = _effectsVolume;

        CambiarVolumenGeneral(_generalVolume);
        CambiarVolumenMusica(_musicVolume);
        CambiarVolumenEfectos(_effectsVolume);
    }
    public void CambiarVolumenGeneral(float volumen)
    {
        _audioMixer.SetFloat("VolumenGeneral", Mathf.Log10(volumen) * 20);

        PlayerPrefs.SetFloat("VolumenGeneral", volumen);
    }
    public void CambiarVolumenMusica(float volumen)
    {
        _audioMixer.SetFloat("VolumenMusica", Mathf.Log10(volumen) * 20);

        PlayerPrefs.SetFloat("VolumenMusica", volumen);
    }
    public void CambiarVolumenEfectos(float volumen)
    {
        _audioMixer.SetFloat("VolumenEfectos", Mathf.Log10(volumen) * 20);

        PlayerPrefs.SetFloat("VolumenEfectos", volumen);
    }
    public void GameScene(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }
    public void ChangeVolume(float volume)
    {
        _audioMixer.SetFloat("Volume",volume);
    }
    public void ChangeVolumeSFX(float volumeSfx)
    {
        _audioMixer.SetFloat("Effects", volumeSfx);
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
