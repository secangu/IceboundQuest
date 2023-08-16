using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene_sc : MonoBehaviour
{
    InterfaceController_sc interfaceController;
    [SerializeField] int scene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interfaceController = FindObjectOfType<InterfaceController_sc>();
            interfaceController.ChangeScene(scene);
        }
    }
}
