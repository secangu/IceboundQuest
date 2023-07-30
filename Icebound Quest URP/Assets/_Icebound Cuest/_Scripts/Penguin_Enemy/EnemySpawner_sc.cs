using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class EnemySpawner_sc : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    GameObject activeEnemy;
    PlayerHealth_sc playerHealth;
    List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] GameObject patrollPoints;//pasa el patrullaje al enemigo

    int defeatCount;
    [SerializeField] int numEnemy;
    [SerializeField] Collider colliderDissolve;

    [Header("GUI")]
    [SerializeField] TextMeshProUGUI text;

    private void Start()
    {
        foreach (Transform t in this.transform) spawnPoints.Add(t); // recorre los elementos hijos del objeto
        SpawnEnemy(true);
        colliderDissolve.enabled = true;
        playerHealth=FindObjectOfType<PlayerHealth_sc>();
    }

    private void Update()
    {
        text.text = "Enemies left: " + (numEnemy - defeatCount).ToString();
        
        if(defeatCount >= numEnemy)
        {
            SpawnEnemy(false);
            colliderDissolve.enabled = false;
            text.text = "Now you can go to the shiny wall you can melt pressing (Q)";
        }
        else if (activeEnemy == null)
        {
            playerHealth.Health += 5;
            defeatCount++;
            if(defeatCount<numEnemy) SpawnEnemy(true);
        }
    } 

    private void SpawnEnemy(bool spawn)
    {
        if (spawn)
        {
            activeEnemy = Instantiate(enemyPrefab, GetRandomSpawnPoint(), Quaternion.identity);
            activeEnemy.GetComponent<EnemyPatrollGameObjects_sc>().Go = patrollPoints;
        }                        
    }

    private Vector3 GetRandomSpawnPoint()
    {
        int random = Random.Range(0, spawnPoints.Count);
        return spawnPoints[random].position;
    }
}
