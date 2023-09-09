using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthHear_sc : MonoBehaviour
{
    [SerializeField] Sprite fullHearth, halfHearth, emptyHearth;
    Image heartImage;

    private void Awake()
    {
        heartImage = GetComponent<Image>();
    }
    private void Update()
    {
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 0);
    }
    public void SetHearthImage(HeartStatus status)
    {
        switch (status)
        {
            case HeartStatus.Empty:
                heartImage.sprite = emptyHearth;
                break;
            case HeartStatus.Half:
                heartImage.sprite = halfHearth;
                break;
            case HeartStatus.Full:
                heartImage.sprite = fullHearth;
                break;
        }
    }

}
public enum HeartStatus
{
    Empty = 0,
    Half = 1,
    Full = 2
}

