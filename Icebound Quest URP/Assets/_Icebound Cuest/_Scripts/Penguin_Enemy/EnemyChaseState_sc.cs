using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState_sc : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;
    EnemyDetectedPlayer_sc enemy;
    float timer;
    [SerializeField] float distance; // distancia a la que esta del jugador
    [SerializeField] float maxChasingDistance; // distancia maxima para dejar de perseguir al player
    [SerializeField] float attackDistance; // distancia atacar al player
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent.speed = 2f;
        enemy = animator.GetComponent<EnemyDetectedPlayer_sc>();
        timer= 0;
        enemy.Color = Color.red;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(player!=null)agent.SetDestination(player.position);

        distance = Vector3.Distance(player.position, animator.transform.position);

        if (distance >= maxChasingDistance && !enemy.PlayerDetected && timer > 1.2f) // si se aleja o deja de ver al jugador durante 1.2 segundo deja de seguirlo
        {
            animator.SetBool("IsChanging", false); 
        }
        if(!enemy.PlayerDetected) timer += Time.deltaTime; else timer = 0; // cuenta cuanto tiempo lleva sin ver al jugador

        if (distance < attackDistance) animator.SetBool("IsAttacking", true);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(animator.transform.position);
    }
}
