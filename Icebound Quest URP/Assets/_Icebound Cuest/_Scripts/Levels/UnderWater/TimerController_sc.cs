using TMPro;
using UnityEngine;

public class TimerController_sc : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] GameObject SucessMenu;
    [SerializeField] TextMeshProUGUI counter;
    [SerializeField] int num;
    AudioController_sc audioController;
    bool pause;
    void Start()
    {
        audioController = FindObjectOfType<AudioController_sc>();
    }

    void Update()
    {
        time -= Time.deltaTime;
        counter.text = time.ToString("0.0");
        if (time <= 0.5 && !pause)
        {
            audioController.StopSounds();
            pause = true;
        }
        else if (time <= 0)
        {
            if (num > PlayerPrefs.GetInt("idLevel")) PlayerPrefs.SetInt("idLevel", num);
            SucessMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
