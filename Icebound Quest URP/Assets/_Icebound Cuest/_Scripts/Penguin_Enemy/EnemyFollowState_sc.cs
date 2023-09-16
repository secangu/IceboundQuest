using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowState_sc : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;

    [SerializeField] float distance; // distancia a la que esta del jugador
    [SerializeField] float attackDistance; // distancia atacar al player
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        agent.speed = 2f;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player != null)
        {
            agent.SetDestination(player.position);
            distance = Vector3.Distance(player.position, animator.transform.position);
        }

        if (distance < attackDistance) animator.SetBool("IsAttacking", true);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(animator.transform.position);
    }
}
