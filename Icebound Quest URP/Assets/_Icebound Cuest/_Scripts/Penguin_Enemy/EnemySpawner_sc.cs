using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner_sc : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    GameObject activeEnemy;
    List<Transform> spawnPoints = new List<Transform>();

    int defeatCount;
    [SerializeField] int numEnemy;
    [SerializeField] Collider colliderDissolve;
    float timer = 3;
    bool wall;
    bool spawn;
    [Header("GUI")]
    [SerializeField] GameObject advices;
    [SerializeField] TextMeshProUGUI text1;
    [SerializeField] GameObject gameObjectText2;


    private void Start()
    {
        foreach (Transform t in this.transform) spawnPoints.Add(t); // recorre los elementos hijos del objeto
        SpawnEnemy(true);
        colliderDissolve.enabled = true;
        spawn = true;
    }

    private void Update()
    {
        if (timer > 0 && spawn)
        {            
            advices.SetActive(true);

            int enemigosRestantes = numEnemy - defeatCount;
            text1.text = "Enemigos restantes: " + enemigosRestantes.ToString();

            timer -= Time.deltaTime;
            if (timer <= 0) spawn = false;
        }
        else if (wall)
        {
            text1.gameObject.SetActive(false);
            gameObjectText2.SetActive(true);
        }
        else
        {
            advices.SetActive(false);
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
            activeEnemy.transform.SetParent(transform);
        }
    }

    private Vector3 GetRandomSpawnPoint()
    {
        int random = Random.Range(0, spawnPoints.Count);
        return spawnPoints[random].position;
    }
}
