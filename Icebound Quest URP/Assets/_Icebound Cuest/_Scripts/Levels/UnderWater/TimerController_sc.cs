using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerController_sc : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] GameObject SucessMenu;
    [SerializeField] TextMeshProUGUI counter;
    [SerializeField] int num;
    void Start()
    {
        
    }

    void Update()
    {
        time-=Time.deltaTime;
        counter.text=time.ToString("0.0");

        if(time <= 0)
        {
            if (num > PlayerPrefs.GetInt("idLevel")) PlayerPrefs.SetInt("idLevel", num);
            SucessMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
