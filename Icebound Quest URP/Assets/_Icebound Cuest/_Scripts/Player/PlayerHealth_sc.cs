using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth_sc : MonoBehaviour
{
    Animator animator;
    [SerializeField]MonoBehaviour[] scripts;
    [SerializeField] float _health;
    
    void Start()
    {
        animator=GetComponent<Animator>();
    }

    void Update()
    {
    }
    public void TakeDamage(float damage)
    {
        _health -= damage;
        
        if (_health <= 0)
        {
            for (int i = 0; i < scripts.Length; i++)
            {
                scripts[i].enabled = false;

            }
            StartCoroutine(CorroutineDeath());
        }
        animator.SetTrigger("Damage");
    }
    IEnumerator CorroutineDeath() 
    {
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(1.42f);
        //Activar game over
        Destroy(gameObject);
    }
}
