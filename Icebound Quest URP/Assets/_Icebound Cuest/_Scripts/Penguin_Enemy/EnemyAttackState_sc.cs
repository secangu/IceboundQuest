using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState_sc : StateMachineBehaviour
{
    Transform player;

    [SerializeField] float distance; // distancia a la que esta del jugador
    [SerializeField] float maxAttackDistance; // distancia maxima para atacar

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.LookAt(player.position);
        animator.transform.rotation = Quaternion.Euler(0, animator.transform.rotation.eulerAngles.y, 0);

        if (player!=null)distance = Vector3.Distance(player.position, animator.transform.position);
        if (distance > maxAttackDistance) animator.SetBool("IsAttacking", false); //si se aleja cancela el ataque 
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
