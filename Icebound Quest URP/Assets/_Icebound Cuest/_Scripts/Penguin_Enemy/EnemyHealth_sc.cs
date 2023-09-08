using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyHealth_sc : MonoBehaviour
{
    EnemyHeartSystem_sc enemyHeartSystem;
    [SerializeField] float health;
    [SerializeField] float maxHealth;
    public float Health { get => health; set => health = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    Animator animator;
    NavMeshAgent agent;
    void Start()
    {
        animator=GetComponent<Animator>();
        agent=GetComponent<NavMeshAgent>();

        health = maxHealth;
        enemyHeartSystem.DrawHearts();
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        enemyHeartSystem.DrawHearts();
        if (Health <= 0)
        {
            agent.acceleration = 0;
            StartCoroutine(CorroutineDeath());
        }
        else
        {
            animator.SetTrigger("Damage");
        }
    }
    public void StunningDamage(float damage)
    {
        Health -= damage;
        enemyHeartSystem.DrawHearts();
        if (Health <= 0)
        {
            agent.acceleration = 0;
            StartCoroutine(CorroutineDeath());
        }
        else
        {
            animator.SetTrigger("Stunned");
        }
    }
    IEnumerator CorroutineDeath()
    {
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }
}
