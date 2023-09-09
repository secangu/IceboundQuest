using UnityEngine;

public class MouseLock_sc : MonoBehaviour
{
    [SerializeField] bool mouse;

    public bool Mouse { get => mouse; set => mouse = value; }

    void Update()
    {
        if (Mouse)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
