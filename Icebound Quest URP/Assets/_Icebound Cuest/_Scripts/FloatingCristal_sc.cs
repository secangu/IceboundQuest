using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FloatingCristal_sc : MonoBehaviour
{
    [SerializeField] GameObject _interface;
    InterfaceController_sc interfaceController;

    void Start()
    {
    }

    public void CloseInterface()
    {
        interfaceController.Turorial = false;
        _interface.SetActive(false);
        interfaceController.Resume();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.F))
        {
            interfaceController = FindObjectOfType<InterfaceController_sc>();

            interfaceController.Turorial = true;
            _interface.SetActive(true);
            interfaceController.Pause();
        }
    }
}
