using UnityEngine;

public class FloatingCristal_sc : MonoBehaviour
{
    [SerializeField] GameObject _interface;
    [SerializeField] GameObject _pressF;
    InterfaceController_sc interfaceController;
    Animator animator;
    Transform target;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void CloseInterface()
    {
        interfaceController.Turorial = false;
        _interface.SetActive(false);
        interfaceController.Resume();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            interfaceController.Turorial = true;
            _interface.SetActive(true);
            interfaceController.Pause();
            animator.SetTrigger("Static");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _pressF.SetActive(true);
            interfaceController = FindObjectOfType<InterfaceController_sc>();            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _pressF.SetActive(false);
        }
    }
}
