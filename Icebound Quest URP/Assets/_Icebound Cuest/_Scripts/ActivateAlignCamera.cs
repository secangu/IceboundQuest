using UnityEngine;

public class ActivateAlignCamera : MonoBehaviour
{
    [SerializeField] GameObject _cameraPlayer;
    [SerializeField] GameObject _cameraImages;
    [SerializeField] MonoBehaviour[] scriptsPlayer;
    bool active;

    void Update()
    {
        if (active && Input.GetKeyDown(KeyCode.T))
        {
            for (int i = 0; i < scriptsPlayer.Length; i++)
            {
                scriptsPlayer[i].enabled = false;
            }
            _cameraPlayer.SetActive(false);
            _cameraImages.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            active = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _cameraPlayer.SetActive(true);
            _cameraImages.SetActive(false);
            active = false;

        }
    }
}

