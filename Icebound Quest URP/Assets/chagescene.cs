using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chagescene : MonoBehaviour
{
    Buttons_sc button;
    [SerializeField] string scene;
    void Start()
    {
        button=FindObjectOfType<Buttons_sc>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            button.GameScene(scene);
        }
    }
}
