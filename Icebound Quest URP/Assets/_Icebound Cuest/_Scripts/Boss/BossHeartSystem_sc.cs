using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHeartSystem_sc : MonoBehaviour
{
    [SerializeField] GameObject heartPrefab;
    [SerializeField] BossHealth_sc bossHealth;

    List<HealthHear_sc> hearts = new List<HealthHear_sc>();
    void Start()
    {
        DrawHearts(); 
    }
    public void DrawHearts() 
    {
        ClearHearts();
        float maxHealth = bossHealth.MaxHealth % 2;
        int heartsToMake = (int)((bossHealth.MaxHealth / 2) + maxHealth);

        for (int i = 0; i < heartsToMake; i++)
        {
            CreateEmptyHeart();
        }
        for (int i = 0; i < hearts.Count; i++)
        {
            int heartStatusRemainder = (int)Mathf.Clamp(bossHealth.Health - (i * 2), 0, 2);
            hearts[i].SetHearthImage((HeartStatus)heartStatusRemainder);
        }
    }
    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        HealthHear_sc heartComponent = newHeart.GetComponent<HealthHear_sc>();
        heartComponent.SetHearthImage(HeartStatus.Empty);
        hearts.Add(heartComponent);
    }
    public void ClearHearts()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<HealthHear_sc>();
    }
}
