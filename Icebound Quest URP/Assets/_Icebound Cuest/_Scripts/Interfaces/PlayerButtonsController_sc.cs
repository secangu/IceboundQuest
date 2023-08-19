using UnityEngine;
using UnityEngine.UI;

public class PlayerButtonsController_sc : MonoBehaviour
{
    [SerializeField] Image[] buttonSprites;
    [SerializeField] Sprite[] activebuttonSprites;
    [SerializeField] Sprite[] disabledbuttonSprites;

    private void SetButtonSprites(Sprite[] newSprites)
    {
        for (int i = 1; i <= 3; i++)
        {
            buttonSprites[i].sprite = newSprites[i];
        }
    }

    public void ActiveSprites()
    {
        SetButtonSprites(activebuttonSprites);
    }

    public void DisabledSprites()
    {
        SetButtonSprites(disabledbuttonSprites);
    }
}