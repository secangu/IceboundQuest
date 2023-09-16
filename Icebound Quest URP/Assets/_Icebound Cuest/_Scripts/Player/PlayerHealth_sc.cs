using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth_sc : MonoBehaviour
{
    HeartSystem_sc heartSystem;
    ProjectileThrow projectileThrow;
    PlayerMovement_sc playerMovement;
    Animator animator;
    [SerializeField] float health;
    [SerializeField] float maxHealth;
    [SerializeField] GameObject _death;
    public float Health { get => health; set => health = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement_sc>();
        heartSystem = FindObjectOfType<HeartSystem_sc>();
        projectileThrow = FindObjectOfType<ProjectileThrow>();
        animator=GetComponent<Animator>();
        health = maxHealth;
        heartSystem.DrawHearts();
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        heartSystem.DrawHearts();
        if(projectileThrow!=null)projectileThrow.WhileThrowing();
        if (Health <= 0)
        {
            gameObject.tag = "wa";
            StartCoroutine(CorroutineDeath());
        }
        else
        {
            animator.SetTrigger("Damage");
        }
    }
    public void StunningTakeDamage(float damage)
    {
        Health -= damage;
        heartSystem.DrawHearts();

        if (playerMovement.SlideLoop || playerMovement.IsSliding && playerMovement.SlideAttackTimer <= 0) 
            playerMovement.SlideAttackTimer = 10;

        if (projectileThrow != null) projectileThrow.WhileThrowing();
        
        if (Health <= 0)
        {
            gameObject.tag = "wa";
            StartCoroutine(CorroutineDeath());
        }
        else
        {
            animator.SetTrigger("Stunning");
        }
    }
    IEnumerator CorroutineDeath() 
    {
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(2.5f);
        Time.timeScale = 0;
        _death.SetActive(true);
    }
}
