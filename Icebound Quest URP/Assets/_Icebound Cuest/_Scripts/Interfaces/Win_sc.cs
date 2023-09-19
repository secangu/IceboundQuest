using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win_sc : MonoBehaviour
{
    [SerializeField] GameObject win;
    [SerializeField] int num;
    AudioController_sc audioController;

    private void Start()
    {
        audioController = FindObjectOfType<AudioController_sc>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (num > PlayerPrefs.GetInt("idLevel")) PlayerPrefs.SetInt("idLevel", num);
            audioController.StopSounds();
            other.gameObject.SetActive(false);
            StartCoroutine(Win());
        }
    }
    IEnumerator Win()
    {
        yield return new WaitForSeconds(0.2f);
        win.SetActive(true);
    }
}
