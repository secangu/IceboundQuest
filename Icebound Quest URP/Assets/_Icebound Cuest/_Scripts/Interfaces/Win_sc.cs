using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win_sc : MonoBehaviour
{
    [SerializeField] GameObject win;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            win.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
