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
        SpawnEnemy();
        colliderDissolve.enabled = false;
        playerHealth=FindObjectOfType<PlayerHealth_sc>();
    }

    private void Update()
    {
        text.text = "Enemies left: " + (numEnemy - defeatCount).ToString();
        if (activeEnemy == null)
        {
            playerHealth.Health += 10;
            defeatCount++;
            SpawnEnemy();
        }
        if(defeatCount >= numEnemy)
        {
            colliderDissolve.enabled = true;
            text.text = "Now you can go to the wall you can melt";
        }
    }

    private void SpawnEnemy()
    {
        
        activeEnemy = Instantiate(enemyPrefab, GetRandomSpawnPoint(), Quaternion.identity);
        activeEnemy.GetComponent<EnemyPatrollGameObjects_sc>().Go = patrollPoints;
    }

    private Vector3 GetRandomSpawnPoint()
    {
        int random = Random.Range(0, spawnPoints.Count);
        return spawnPoints[random].position;
    }
}
