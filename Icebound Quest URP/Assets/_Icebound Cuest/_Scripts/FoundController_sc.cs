using System.Collections.Generic;
using UnityEngine;

public class FoundController_sc : MonoBehaviour
{
    [SerializeField] List<EnemyDetectedPlayer_sc> enemiesDetectedPlayer = new List<EnemyDetectedPlayer_sc>();
    [SerializeField] GameObject gameOver;
    void Awake()
    {
        EnemyDetectedPlayer_sc[] foundScripts = FindObjectsOfType<EnemyDetectedPlayer_sc>();
        enemiesDetectedPlayer.AddRange(foundScripts);
    }

    void Update()
    {
        foreach(EnemyDetectedPlayer_sc script in enemiesDetectedPlayer)
        {
            bool playerDetected = script.PlayerDetected;
            if (playerDetected)
            {
                Time.timeScale = 0;
                gameOver.SetActive(true);
            }
        }
    }
}
