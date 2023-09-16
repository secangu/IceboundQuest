using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack_sc : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float checkAttackRadius;
    [SerializeField] Transform checkAttack;

    public void Attack()
    {
        Collider[] checkAttacks = Physics.OverlapSphere(checkAttack.position, checkAttackRadius);

        foreach (Collider checkAttackCollision in checkAttacks)
        {
            if (checkAttackCollision.gameObject.tag == "Player")
            {
                checkAttackCollision.transform.GetComponent<PlayerHealth_sc>()?.TakeDamage(damage);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(checkAttack.position, checkAttackRadius);
    }
}
