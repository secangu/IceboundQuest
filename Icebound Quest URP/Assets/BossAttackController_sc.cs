using UnityEngine;
using UnityEngine.AI;

public class BossAttackController_sc : MonoBehaviour
{
    Transform player;
    NavMeshAgent agent;
    float distance;
    [SerializeField] bool lookPlayer;

    [Header("PeckAttack")]
    [SerializeField] bool activePeck;
    [SerializeField] Transform peckAttack;
    [SerializeField] float radiusPeckAttack;
    [SerializeField] int damagePeckAttack;

    [Header("StompAttack")]
    [SerializeField] GameObject prefabCrystals;
    [SerializeField] Transform originInstantiate;

    [Header("JumpAttack")]
    [SerializeField] bool activeJump;
    [SerializeField] bool follow = false;
    [SerializeField] Transform jumpAttack;
    [SerializeField] float radiusJumpAttack;
    [SerializeField] int damageJumpAttack;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        follow = false;
    }

    void Update()
    {
        if (lookPlayer)
        {
            Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
            targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);

            float rotationSpeed = 5.0f; // Ajusta la velocidad de rotacion segun tus preferencias.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (activePeck) PeckAttack();
        if (activeJump) JumpAttack();

        if (follow)
        {
            if (player != null)
            {
                agent.SetDestination(player.position);
                distance = Vector3.Distance(player.position, transform.position);
            }
        }
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

    void Crystals()
    {
        Instantiate(prefabCrystals, originInstantiate.position, Quaternion.LookRotation(originInstantiate.forward));
    }
    void ActivePeckAttackActive()
    {
        activePeck = true;
    }
    void ActiveJumpAttackActive()
    {
        activeJump = true;
    }
    void DisabledPeckAttackActive()
    {
        activePeck = false;
    }
    void DisabledJumpAttackActive()
    {
        activeJump = false;
    }
    void StopFollow()
    {
        follow = false;
        agent.SetDestination(transform.position);
    }
    void Follow()
    {
        follow = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(peckAttack.position, radiusPeckAttack);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(jumpAttack.position, radiusJumpAttack);
    }
}
