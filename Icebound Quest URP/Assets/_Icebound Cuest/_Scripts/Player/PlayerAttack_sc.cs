using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack_sc : MonoBehaviour
{
    Animator animator;
    [SerializeField]MonoBehaviour[] scripts;

    [SerializeField] Transform checkAttack;
    [SerializeField] float time;
    [SerializeField] float attackTime;
    [SerializeField] float damage;
    [SerializeField] float checkAttackRadius;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (time > 0)
        {
            for (int i = 0; i < scripts.Length; i++)
            {
                scripts[i].enabled = false;

            }
            time -= Time.deltaTime;
        }        
        if (time <= 0)
        {
            for (int i = 0; i < scripts.Length; i++)
            {
                scripts[i].enabled = true;
            }
        }
        if (Input.GetMouseButtonDown(0) && time <= 0)
        {
            time = attackTime;
            animator.SetTrigger("Attack");
            //sonidoAtaque.Play();
        }
    }
    public void Attack()
    {
        Collider[] checkAttacks = Physics.OverlapSphere(checkAttack.position, checkAttackRadius);

        foreach(Collider checkAttackCollision in checkAttacks)
        {
            if (checkAttackCollision.gameObject.tag == "Enemy")
            {
                checkAttackCollision.transform.GetComponent<EnemyHealth_sc>().TakeDamage(damage);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(checkAttack.position, checkAttackRadius);
    }
}
