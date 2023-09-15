using UnityEngine;

public class BossAttackController_sc : MonoBehaviour
{
    [Header("PeckAreaAttack")]
    [SerializeField] bool activePeck;
    [SerializeField] Transform peckAttack;
    [SerializeField] float radiusPeckAttack;
    [SerializeField] int damagePeckAttack;

    [Header("StompAreaAttack")]
    [SerializeField] bool activeStomp;
    [SerializeField] Transform stompAttack;
    [SerializeField] Vector3 sizeStompAttack;
    [SerializeField] int damageStompAttack;

    [Header("JumpAreaAttack")]
    [SerializeField] bool activeJump;
    [SerializeField] Transform jumpAttack;
    [SerializeField] float radiusJumpAttack;
    [SerializeField] int damageJumpAttack;
    void Start()
    {

    }

    void Update()
    {
        if (activePeck) PeckAttack();
        if (activeStomp) StompAttack();
        if (activeJump) JumpAttack();
    }
    private void PeckAttack()
    {
        Collider[] colliders = Physics.OverlapSphere(peckAttack.position, radiusPeckAttack);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                collider.GetComponent<PlayerHealth_sc>().TakeDamage(damagePeckAttack);
                activePeck = false;
                break;
            }
        }
    }
    private void StompAttack()
    {
        Collider[] colliders = Physics.OverlapBox(stompAttack.position, sizeStompAttack);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                collider.GetComponent<PlayerHealth_sc>().TakeDamage(damageStompAttack);
                activeStomp = false;
                break;
            }
        }
    }
    private void JumpAttack()
    {
        Collider[] colliders = Physics.OverlapSphere(jumpAttack.position, radiusJumpAttack);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                collider.GetComponent<PlayerHealth_sc>().StunningTakeDamage(damageJumpAttack);
                activeJump = false;
                break;
            }
        }
    }
    public void ActivePeckAttackActive()
    {
        activePeck = true;
    }
    public void ActiveStompttackActive()
    {
        activeStomp = true;
    }
    public void ActiveJumpAttackActive()
    {
        activeJump = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(peckAttack.position, radiusPeckAttack);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(stompAttack.position, sizeStompAttack);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(jumpAttack.position, radiusJumpAttack);
    }
}
