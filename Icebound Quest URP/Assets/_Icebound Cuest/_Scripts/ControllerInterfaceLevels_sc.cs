using UnityEngine;

public class ControllerInterfaceLevels_sc : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject canvasPrincipal;
    [SerializeField] MouseLock_sc mouselock;
    void Start()
    {
        if (PlayerPrefs.GetInt("idLevel") >= 2)
        {
            gameObject.SetActive(false);
            player.SetActive(true);
            canvasPrincipal.SetActive(true);
            mouselock.Mouse = false;
        }
    }

}
