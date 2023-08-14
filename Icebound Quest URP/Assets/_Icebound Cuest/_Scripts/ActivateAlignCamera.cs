using UnityEngine;

public class ActivateAlignCamera : MonoBehaviour
{
    [SerializeField] GameObject _cameraPlayer;
    [SerializeField] GameObject _cameraImages;
    [SerializeField] GameObject _player;
    [SerializeField] MonoBehaviour[] scriptsPlayer;
    [SerializeField] GameObject _text;
    bool active;

    void Update()
    {
        if (active && Input.GetKeyDown(KeyCode.T))
        {
            _player.SetActive(false);
            _cameraPlayer.SetActive(false);
            _cameraImages.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            active = true;
            _text.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _cameraPlayer.SetActive(true);
            _cameraImages.SetActive(false);
            _text.SetActive(false);
            active = false;

        }
    }
}

