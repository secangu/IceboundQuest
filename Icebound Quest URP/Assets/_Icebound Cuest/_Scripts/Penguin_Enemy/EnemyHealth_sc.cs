using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyHealth_sc : MonoBehaviour
{
    [SerializeField] float _health;
    [SerializeField] Slider _healthBar;
    Animator animator;
    NavMeshAgent agent;
    void Start()
    {
        animator=GetComponent<Animator>();
        agent=GetComponent<NavMeshAgent>();
        _healthBar.maxValue = _health;
        _healthBar.value = _health;
    }

    void Update()
    {
    }
    public void TakeDamage(float damage)
    {
        _health -= damage;
        _healthBar.value = _health;
        if (_health <= 0)
        {
            agent.acceleration = 0;
            GetComponent<Collider>().enabled = false;
            StartCoroutine(CorroutineDeath());
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
        Destroy(gameObject);
    }
}
