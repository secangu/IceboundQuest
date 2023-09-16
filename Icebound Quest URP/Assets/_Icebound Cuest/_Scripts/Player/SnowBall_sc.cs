using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall_sc : MonoBehaviour
{
    [SerializeField]ParticleSystem _particleSystem;
    [SerializeField]int damage;
    
    private void OnCollisionEnter(Collision other)
    {
        _particleSystem.Play();
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        this.gameObject.GetComponent<Collider>().enabled = false;
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.transform.GetComponent<EnemyHealth_sc>()?.TakeDamage(damage);
            other.transform.GetComponent<BossHealth_sc>()?.TakeDamage(damage);

        }
    }
}
