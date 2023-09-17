using UnityEngine;

public class PhaseBossController_sc : MonoBehaviour
{
    InterfaceController_sc interfaceController;
    BossHealth_sc bossHealth;

    [SerializeField] int escene;
    [SerializeField] GameObject[] disbledObjects;
    void Start()
    {
        interfaceController = FindObjectOfType<InterfaceController_sc>();
        bossHealth = FindObjectOfType<BossHealth_sc>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossHealth.Health <= 10)
        {
            for (int i = 0; i < disbledObjects.Length; i++)
            {
                disbledObjects[i].SetActive(false);
            }
            interfaceController.ChangeScene(escene);
        }
    }
}
