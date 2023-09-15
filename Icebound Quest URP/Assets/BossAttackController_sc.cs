using UnityEngine;

public class BossAttackController_sc : MonoBehaviour
{
    Transform player;
    [SerializeField] bool lookPlayer;
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
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
    public void DisabledPeckAttackActive()
    {
        activePeck = false;
    }
    public void DisabledStompttackActive()
    {
        activeStomp = false;
    }
    public void DisabledJumpAttackActive()
    {
        activeJump = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(peckAttack.position, radiusPeckAttack);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(jumpAttack.position, radiusJumpAttack);

        Gizmos.color = Color.blue;
        Gizmos.matrix = Matrix4x4.TRS(stompAttack.position, stompAttack.rotation, Vector3.one); ;
        Gizmos.DrawWireCube(Vector3.zero, sizeStompAttack);

        
    }
}
