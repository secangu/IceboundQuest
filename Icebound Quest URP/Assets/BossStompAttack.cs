using UnityEngine;

public class BossStompAttack : MonoBehaviour
{
    [SerializeField] int damage;
    float timerDamage;
    PlayerMovement_sc playerMovement;
    private void Start()
    {
        playerMovement=FindObjectOfType<PlayerMovement_sc>();
    }
    void DestroyObject()
    {
        Destroy(gameObject);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (timerDamage <= 0)
            {
                if (playerMovement.SlideLoop || playerMovement.IsSliding && playerMovement.SlideAttackTimer <= 0)
                    playerMovement.SlideAttackTimer = 10;

                timerDamage = 1.5f;
                other.GetComponent<PlayerHealth_sc>().TakeDamage(damage);
            }
            else
            {
                timerDamage -= Time.deltaTime;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timerDamage = 0;
        }
    }
}
