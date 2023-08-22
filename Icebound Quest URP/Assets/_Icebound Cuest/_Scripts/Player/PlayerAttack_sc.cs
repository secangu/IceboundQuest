using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack_sc : MonoBehaviour
{
    Animator animator;
    [SerializeField] Transform checkAttack;
    [SerializeField] float time;
    [SerializeField] float attackTime;
    [SerializeField] float damage;
    [SerializeField] float stunningDamage;
    [SerializeField] float checkAttackRadius;
    PlayerMovement_sc playerMovement;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement_sc>();
    }

    void Update()
    {
        if (time > 0) time -= Time.deltaTime;
            
        if (Input.GetMouseButtonDown(0) && time <= 0)
        {
            playerMovement.CancelSlide();
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
   
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (playerMovement.SlideLoop || playerMovement.IsSlide && playerMovement.SlideAttackTimer <= 0)
            {
                other.transform.GetComponent<EnemyHealth_sc>().StunningDamage(stunningDamage);
                playerMovement.SlideAttackTimer = 10;
            }            
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(checkAttack.position, checkAttackRadius);
    }
}
