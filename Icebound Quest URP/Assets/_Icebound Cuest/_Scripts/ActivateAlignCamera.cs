using UnityEngine;

public class ActivateAlignCamera : MonoBehaviour
{
    [SerializeField] GameObject _cameraPlayer;
    [SerializeField] GameObject _cameraImages;
    [SerializeField] GameObject _player;
    bool active;

    public void AlignCamera()
    {
        if (active)
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

