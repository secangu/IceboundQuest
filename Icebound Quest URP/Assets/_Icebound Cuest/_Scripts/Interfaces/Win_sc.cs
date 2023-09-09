using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win_sc : MonoBehaviour
{
    [SerializeField] GameObject win;
    [SerializeField] int num;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (num > PlayerPrefs.GetInt("idLevel")) PlayerPrefs.SetInt("idLevel", num);
            win.SetActive(true);
            other.gameObject.SetActive(false);
        }
    }
}
