using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioController_sc : MonoBehaviour
{
    [SerializeField] AudioMixer _audioMixer;
    [SerializeField] Slider _masterSlider;
    [SerializeField] Slider _melodySlider;
    [SerializeField] Slider effectsSlider;

    float _masterVolume;
    float _melodyVolume;
    float _effectsVolume;
    private void Start()
    {
        if (PlayerPrefs.HasKey("MasterVolume") == false) PlayerPrefs.SetFloat("MasterVolume", 1.5f);
        if (PlayerPrefs.HasKey("MelodyVolume") == false) PlayerPrefs.SetFloat("MelodyVolume", 1.5f);
        if (PlayerPrefs.HasKey("EffectsVolume") == false) PlayerPrefs.SetFloat("EffectsVolume", 5f);

        _masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        _melodyVolume = PlayerPrefs.GetFloat("MelodyVolume");
        _effectsVolume = PlayerPrefs.GetFloat("EffectsVolume");

        _masterSlider.value = _masterVolume;
        _melodySlider.value = _melodyVolume;
        effectsSlider.value = _effectsVolume;

        ChangeMasterVolume(_masterVolume);
        ChangeMelodyVolume(_melodyVolume);
        ChangeEffectsVolume(_effectsVolume);
    }
    public void ChangeMasterVolume(float volume)
    {
        _audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);

        PlayerPrefs.SetFloat("MasterVolume", volume);
    }
    public void ChangeMelodyVolume(float volume)
    {
        _audioMixer.SetFloat("MelodyVolume", Mathf.Log10(volume) * 20);

        PlayerPrefs.SetFloat("MelodyVolume", volume);
    }
    public void ChangeEffectsVolume(float volume)
    {
        _audioMixer.SetFloat("EffectsVolume", Mathf.Log10(volume) * 20);

        PlayerPrefs.SetFloat("EffectsVolume", volume);
    }
}