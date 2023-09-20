using System.Collections;
using UnityEngine;

public class WaterBossController_sc : MonoBehaviour
{
    Transform player;
    Animator animator;
    Rigidbody _rb;
    [SerializeField] GameObject _camera;

    [Header("statistics")]
    [SerializeField] float speed;
    [SerializeField] float forceTornado;
    [SerializeField] float attackDistance;
    [SerializeField] float maxDistance;
    
    [Header("Timers")]
    [SerializeField] float timerDamage;
    [SerializeField] float timerAttack;

    [Header("AreaAttack")]
    [SerializeField] Transform areaAttack;
    [SerializeField] float radiusAreaAttack;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        StartCoroutine(DisabledCamera());
    }

    void Update()
    {
        FollowPlayer();
        Attack();

        float distance = Vector3.Distance(transform.position, player.position);
        //Debug.Log(distance);
    
        if (distance <= attackDistance && timerAttack >= 4)
        {
            timerAttack = 0;
            animator.SetTrigger("Attack");
        }
        else
        {
            timerAttack += Time.deltaTime;
        }



    }
    void FollowPlayer()
    {
        //Debug.Log("a");
        Vector3 directionToPlayer = (player.position - transform.position).normalized; // Calcula la dirección hacia el punto de referencia actual.
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer); // Calcula la rotacion deseada hacia el jugador.

        float rotationSpeed = 5.0f; // Ajusta la velocidad de rotacion segun tus preferencias.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);        
    }
    void Attack()
    {
        if (timerDamage >= 1.5f)
        {
            Collider[] colliders = Physics.OverlapSphere(areaAttack.position, radiusAreaAttack);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    collider.GetComponent<PlayerHealth_sc>().TakeDamage(2);
                    timerDamage = 0;
                    break;
                }
            }
        }
        else
        {
            timerDamage += Time.deltaTime;
        }
    }
    void TornadoImpulse()
    {
        Vector3 forwardDirection = transform.forward;
        _rb.AddForce(forwardDirection * forceTornado, ForceMode.Impulse);
    }
    void CancelImpulse()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }
    IEnumerator DisabledCamera()
    {
        yield return new WaitForSeconds(0.5f);
        _camera.SetActive(false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(areaAttack.position, radiusAreaAttack);
    }
}
