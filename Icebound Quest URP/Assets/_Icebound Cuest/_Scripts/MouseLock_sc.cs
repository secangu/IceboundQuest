using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLock_sc : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
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
