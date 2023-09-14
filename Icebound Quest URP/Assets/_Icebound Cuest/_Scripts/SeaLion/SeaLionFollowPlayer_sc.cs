using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaLionFollowPlayer : MonoBehaviour
{
    Transform player;
    Animator animator;

    [Header("Movement")]
    [SerializeField] float movementSpeed;
    [SerializeField] float movementSpeedtoPlayer;
    [SerializeField] int damage;

    [Header("Patrol")]
    [SerializeField] GameObject waypoints;
    List<Transform> wayPoints = new List<Transform>();
    int currentWaypointIndex;

    [Header("Attack Sphere")]
    [SerializeField] Transform attackSphereTransform;
    [SerializeField] float sphereRadius;
    [SerializeField] float attackDistance;

    [Header("Detected Area")]
    [SerializeField] Transform areaSizeTransform;
    [SerializeField] Vector3 patrolAreaSize;
    [SerializeField] Vector3 chaseAreaSize;
    [SerializeField] Vector3 areaSize;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        animator = GetComponent<Animator>();

        foreach (Transform t in waypoints.transform)
        {
            wayPoints.Add(t);
        }
    }
    private void Update()
    {
        // Si no está siguiendo al jugador, sigue el patrullaje.
        if (AreaDetected())
        {
            areaSize = chaseAreaSize;
            FollowPlayer();
            animator.SetBool("Patrol", false);
        }
        else
        {
            areaSize = patrolAreaSize;
            Patrol();
            animator.SetBool("Patrol",true);
            animator.SetBool("Follow",false);
        }
    }
    bool AreaDetected()
    {
        Collider[] colliders = Physics.OverlapBox(areaSizeTransform.position, areaSize);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }
    void FollowPlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized; // Calcula la dirección hacia el punto de referencia actual.
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer); // Calcula la rotacion deseada hacia el jugador.

        float rotationSpeed = 5.0f; // Ajusta la velocidad de rotacion segun tus preferencias.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, player.position, movementSpeedtoPlayer * Time.deltaTime);

        // Si el jugador está dentro del rango de ataque, inicia la animación de ataque.
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackDistance)
        {
            animator.SetTrigger("Attack");
            animator.SetBool("Follow", false);
        }
        else
        {
            animator.SetBool("Follow", true);
        }
    }
    void Patrol()
    {
        Transform currentWaypoint = wayPoints[currentWaypointIndex];
        Vector3 directionToWaypoint = (currentWaypoint.position - transform.position).normalized;  
        Quaternion targetRotation = Quaternion.LookRotation(directionToWaypoint);

        float rotationSpeed = 5.0f; // Ajusta la velocidad de rotacion segun tus preferencias.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, movementSpeed * Time.deltaTime);

        // Si el leon marino está cerca del punto de referencia actual, avanza al siguiente.
        float distanceToWaypoint = Vector3.Distance(transform.position, currentWaypoint.position);

        if (distanceToWaypoint < 1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % wayPoints.Count;
        }
    }
    public void Attack()
    {
        Collider[] colliders = Physics.OverlapSphere(attackSphereTransform.position, sphereRadius);

        foreach (Collider checkAttackCollision in colliders)
        {
            if (checkAttackCollision.CompareTag("Player"))
            {
                PlayerHealth_sc playerHealth = checkAttackCollision.GetComponent<PlayerHealth_sc>();

                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                    break;
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackSphereTransform.position, sphereRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(areaSizeTransform.position, areaSize);
    }
}
