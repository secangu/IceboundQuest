using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhaseBossController_sc : MonoBehaviour
{
    InterfaceController_sc interfaceController;
    BossHealth_sc bossHealth;

    [SerializeField] int escene;

    [SerializeField] GameObject[] disbledObjects;
    [SerializeField] GameObject spawn;
    [SerializeField] GameObject boss;
    [SerializeField] GameObject _camera;
    GameObject player;
    Animator animator;


    bool cambia;
    void Start()
    {
        interfaceController = FindObjectOfType<InterfaceController_sc>();
        bossHealth = FindObjectOfType<BossHealth_sc>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = player.GetComponent<Animator>();
    }

    void Update()
    {

        if (bossHealth.Health <= 10)
        {
            StartCoroutine(Phase2());
            spawn.SetActive(false);
        }
    }
    IEnumerator Phase2()
    {
        animator.Play("Point");
        _camera.SetActive(true);
        boss.transform.position =new Vector3(-60.01f,2.167f,-3.67f);
        boss.transform.rotation = Quaternion.Euler(0,0,0);

        player.transform.position = new Vector3(-60.01f, 2.167f, -1f);
        player.transform.rotation = Quaternion.Euler(0,180,0);
        
        yield return new WaitForSeconds(3.0f);
        Debug.Log("cambia");

        for (int i = 0; i < disbledObjects.Length; i++)
        {
            disbledObjects[i].SetActive(false);
        }
        if (!cambia)
        {
            interfaceController.ChangeScene(escene);
            cambia = true;
        }
        
        yield break;
    }
}
