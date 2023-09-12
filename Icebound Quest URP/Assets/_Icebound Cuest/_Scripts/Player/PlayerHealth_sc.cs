using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth_sc : MonoBehaviour
{
    HeartSystem_sc heartSystem;
    ProjectileThrow projectileThrow;
    Animator animator;
    [SerializeField] float health;
    [SerializeField] float maxHealth;
    Collider _collider;
    Rigidbody rb;
    [SerializeField] GameObject _death;
    public float Health { get => health; set => health = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    void Start()
    {
        heartSystem = FindObjectOfType<HeartSystem_sc>();
        projectileThrow = FindObjectOfType<ProjectileThrow>();
        animator=GetComponent<Animator>();
        _collider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
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
            StartCoroutine(CorroutineDeath());
            _collider.enabled = false;
            rb.isKinematic = true;                       
        }
        else
        {
            animator.SetTrigger("Damage");
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
