using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerButtonsController_sc : MonoBehaviour
{
    [SerializeField] Image[] buttonSprites;
    [SerializeField] Sprite[] activebuttonSprites;
    [SerializeField] Sprite[] disabledbuttonSprites;
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI text;
  
    private void SetButtonSprites(Sprite[] newSprites)
    {
        for (int i = 0; i <= 2; i++)
        {
            buttonSprites[i].sprite = newSprites[i];
        }
    }
    public void SliderValue(float value)
    {
        slider.value = value;

        if (value > 0)
        {
            text.text = value.ToString("0.0");
        }
        else
        {
            text.text = "";
        }
    }
    public void ActiveSprites()
    {
        SetButtonSprites(activebuttonSprites);
        slider.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
    }

    public void DisabledSprites()
    {
        SetButtonSprites(disabledbuttonSprites);
        slider.gameObject.SetActive(true);
        text.gameObject.SetActive(true);
    }
}