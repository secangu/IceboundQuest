using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class FoundController_sc : MonoBehaviour
{
    [SerializeField] List<EnemyDetectedPlayer_sc> enemiesDetectedPlayer = new List<EnemyDetectedPlayer_sc>();
    [SerializeField] List<MonoBehaviour> playerScripts = new List<MonoBehaviour>();
    [SerializeField] GameObject gameOver, _camera, player;
    [SerializeField]float timer;
    bool detected;
    
    void Awake()
    {
        //obtiene todos los scripts enemydetectedplayer de la escena
        EnemyDetectedPlayer_sc[] foundScripts = FindObjectsOfType<EnemyDetectedPlayer_sc>();
        enemiesDetectedPlayer.AddRange(foundScripts); // los agrega a la lista

        player = GameObject.FindGameObjectWithTag("Player");
        //obtienen todos los scripts del player
        MonoBehaviour[] scripts = player.GetComponents<MonoBehaviour>();
        playerScripts.AddRange(scripts); // los añade a la lista

    }

    void Update()
    {        
        if (detected)
        {
            //Recorre la lista para desactivarlos 
            foreach (MonoBehaviour playerScripts in playerScripts)
            {
                playerScripts.enabled = false;
            }
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                Time.timeScale = 0;
                timer = 0;
                gameOver.SetActive(true);
            }
        }
        else
        {
            //recorre los scripts para saber si algun enemigo detecto al jugador
            foreach (EnemyDetectedPlayer_sc script in enemiesDetectedPlayer)
            {
                bool playerDetected = script.PlayerDetected;
                if (playerDetected)
                {
                    _camera.SetActive(true);
                    detected = true;
                }
            }
        }
    }
}
