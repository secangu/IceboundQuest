using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth_sc : MonoBehaviour
{
    Animator animator;
    [SerializeField]MonoBehaviour[] scripts;
    [SerializeField] float _health;
    Collider _collider;
    Rigidbody rb;
    void Start()
    {
        animator=GetComponent<Animator>();
        _collider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
    }
    public void TakeDamage(float damage)
    {
        _health -= damage;
        
        if (_health <= 0)
        {            
            StartCoroutine(CorroutineDeath());
            _collider.enabled = false;
            rb.isKinematic = true;
            for (int i = 0; i < scripts.Length; i++)
            {
                if(scripts!=null) scripts[i].enabled = false;
            }
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
        //llamar game over;
        Destroy(gameObject,0.2f);        
    }
}
