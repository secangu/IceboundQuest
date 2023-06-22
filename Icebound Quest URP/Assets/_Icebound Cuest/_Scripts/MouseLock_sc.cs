using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLock_sc : MonoBehaviour
{
    private bool isMouseLocked = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isMouseLocked = !isMouseLocked;
        }

        if (isMouseLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
