using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossHealth_sc : MonoBehaviour
{
    [SerializeField] BossHeartSystem_sc bossHeartSystem;
    [SerializeField] float health;
    [SerializeField] float maxHealth;
    bool invocate;
    public float Health { get => health; set => health = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    Animator animator;
    NavMeshAgent agent;
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        health = maxHealth;
        bossHeartSystem.DrawHearts();
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        bossHeartSystem.DrawHearts();

        if (Health <= 20&&!invocate)
        {
            animator.SetTrigger("Invocation");
            invocate= true;
        }
        else
        {
            animator.SetTrigger("Damage");
        }
    }
    IEnumerator CorroutinePhase2()
    {
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }

    void ChageTag()
    {
        gameObject.tag = "wa";
    }
    void RestoreTag()
    {
        gameObject.tag = "Enemy";
    }
}
