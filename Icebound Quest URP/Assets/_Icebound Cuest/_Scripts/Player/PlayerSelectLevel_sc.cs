using System.Collections;
using UnityEngine;

public class PlayerSelectLevel_sc : MonoBehaviour
{
    [SerializeField] AnimationClip jumpIntoWater;
    [SerializeField] float rotation;
    [SerializeField] int scene;
    bool enter;
    bool cancel;

    Animator animator;
    InterfaceController_sc interfaceController;
    private void Start()
    {
        interfaceController = FindObjectOfType<InterfaceController_sc>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (enter && Input.GetKeyUp(KeyCode.F)&&!cancel)
        {            
            cancel = true;
            enter = false;
            transform.rotation = Quaternion.Euler(0, rotation, 0);
            StartCoroutine(Jump());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SelectLevel"))
        {
            rotation = other.GetComponentInParent<LockedLevels_sc>().Rotation;
            scene= other.GetComponentInParent<LockedLevels_sc>().Scene;
            enter = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SelectLevel"))
        {
            enter = false;
        }
    }
    IEnumerator Jump()
    {
        animator.Play("JumpIntoWater");
        yield return new WaitForSeconds(jumpIntoWater.length);
        interfaceController.ChangeScene(scene);
    }
}
