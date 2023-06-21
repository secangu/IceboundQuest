using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAlertState_sc : StateMachineBehaviour
{
    float timer;
    [SerializeField] float distance; // distancia a la que esta del jugador
    [SerializeField] float alertDistance; // distancia para salir de la alerta

    Transform player;
    EnemyDetectedPlayer_sc enemy;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = animator.GetComponent<EnemyDetectedPlayer_sc>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer+= Time.deltaTime;

        distance = Vector3.Distance(player.position, animator.transform.position);

        if (enemy.PlayerDetected) animator.SetBool("IsChanging", true); //Si detecta al jugador pasa a perseguirlo
        if (distance >= alertDistance || timer > 5.21f) animator.SetBool("IsAlert", false); //si el jugador se aleja o se acaba el tiempo finaliza la alerta
        else timer = 0;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
