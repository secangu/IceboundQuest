using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossHealth_sc : MonoBehaviour
{
    [SerializeField] BossHeartSystem_sc bossHeartSystem;
    [SerializeField] float health;
    [SerializeField] float maxHealth;
    [SerializeField] AnimationClip invocation;
    bool invocate;
    public float Health { get => health; set => health = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();

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
            animator.SetBool("BoolInvocation",true);
            StartCoroutine(CacelBool());
            invocate= true;
        }
        else if (Health <= 10)
        {
            animator.Play("Final");
            animator.SetBool("BoolFinal",true);
        }
        else
        {
            animator.SetTrigger("Damage");
        }
    }
    IEnumerator CacelBool()
    {
        yield return new WaitForSeconds(invocation.length);
        animator.SetBool("BoolInvocation", false);
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
