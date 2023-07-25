using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene_sc : MonoBehaviour
{
    InterfaceController_sc interfaceController;
    [SerializeField] int scene;
    void Start()
    {
        interfaceController=FindObjectOfType<InterfaceController_sc>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interfaceController.ChangeScene(scene);
        }
    }
}
