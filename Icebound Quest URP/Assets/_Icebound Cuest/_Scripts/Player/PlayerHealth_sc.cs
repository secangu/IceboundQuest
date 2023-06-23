using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth_sc : MonoBehaviour
{
    Animator animator;
    [SerializeField]MonoBehaviour[] scripts;
    [SerializeField] float health;
    Collider _collider;
    Rigidbody rb;
    [SerializeField]Slider _healthBar;
    public float Health { get => health; set => health = value; }

    void Start()
    {
        animator=GetComponent<Animator>();
        _collider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

        _healthBar.maxValue = Health;
        _healthBar.value = Health;
    }

    void Update()
    {
    }
    public void TakeDamage(float damage)
    {
        Health -= damage;
        _healthBar.value = Health;

        if (Health <= 0)
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
