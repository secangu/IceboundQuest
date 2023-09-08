using System.Collections.Generic;
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
    [SerializeField] GameObject gameObjectText;
    float timer = 3;
    bool wall;
    bool spawn;
    [Header("GUI")]
    [SerializeField] TextMeshProUGUI text;

    private void Start()
    {
        foreach (Transform t in this.transform) spawnPoints.Add(t); // recorre los elementos hijos del objeto
        SpawnEnemy(true);
        colliderDissolve.enabled = true;
        playerHealth = FindObjectOfType<PlayerHealth_sc>();
        spawn = true;
    }

    private void Update()
    {
        if (timer > 0 && spawn)
        {            
            gameObjectText.SetActive(true);
            text.text = "Enemies left: " + (numEnemy - defeatCount).ToString();
            timer -= Time.deltaTime;
            if (timer <= 0) spawn = false;
        }
        else if (wall)
        {
            text.text = "Now you can go to the shiny wall you can melt pressing (Q)";
            gameObjectText.SetActive(true);
        }
        else
        {
            gameObjectText.SetActive(false);
        }


        if (defeatCount >= numEnemy)
        {
            SpawnEnemy(false);
            colliderDissolve.enabled = false;
            wall = true;
            spawn = false;
        }
        else if (activeEnemy == null)
        {
            defeatCount++;
            timer = 3;
            spawn = true;
            if (defeatCount < numEnemy) SpawnEnemy(true);
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
