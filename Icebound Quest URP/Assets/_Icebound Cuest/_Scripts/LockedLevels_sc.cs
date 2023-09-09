using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedLevels_sc : MonoBehaviour
{
    [SerializeField] int num;
    [SerializeField] GameObject platform;
    void Start()
    {
        if (PlayerPrefs.HasKey("idLevel") == false) PlayerPrefs.SetInt("idLevel", 1);

        if(PlayerPrefs.GetInt("idLevel")>=num) platform.SetActive(false);else platform.SetActive(true);
    }
}
