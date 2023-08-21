using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HeartSystem_sc : MonoBehaviour
{
    [SerializeField] GameObject heartPrefab;
    [SerializeField] PlayerHealth_sc playerHealth;

    List<HealthHear_sc> hearts = new List<HealthHear_sc>();
    void Awake()
    {
        DrawHearts();
    }
    public void DrawHearts()
    {
        ClearHearts();
        float maxHealth = playerHealth.MaxHealth%2;
        int heartsToMake = (int)((playerHealth.MaxHealth / 2) + maxHealth);

        for (int i = 0; i < heartsToMake; i++)
        {
            CreateEmptyHeart();
        }
        for (int i = 0; i < hearts.Count; i++)
        {
            int heartStatusRemainder = (int)Mathf.Clamp(playerHealth.Health - (i * 2), 0, 2);
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
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<HealthHear_sc>();
    }
}
