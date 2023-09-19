using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BossAttackController_sc : MonoBehaviour
{
    Transform player;
    NavMeshAgent agent;
    float distance;
    [SerializeField] bool lookPlayer;


    [Header ("SummonEnemies")]
    [SerializeField] GameObject enemyPrefab; 
    [SerializeField] Transform[] entryPoints;

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

    private void SummonEnemies()
    {
        Debug.Log("invoca");
        int numberOfEnemiesToSummon = Random.Range(1, 3); // Entre 1 y 2 enemigos
        List<int> availableEntryPoints = new List<int>(entryPoints.Length);

        for (int i = 0; i < entryPoints.Length; i++)
        {
            availableEntryPoints.Add(i);
        }

        for (int i = 0; i < numberOfEnemiesToSummon; i++)
        {
            if (availableEntryPoints.Count > 0)
            {
                int randomIndex = Random.Range(0, availableEntryPoints.Count);
                int entryPointIndex = availableEntryPoints[randomIndex];
                Vector3 spawnPosition = entryPoints[entryPointIndex].position;
                
                // Crear el enemigo en la posición de la entrada seleccionada
                if (entryPointIndex == 0)
                {
                    GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.Euler(0, -90, 0));
                    enemy.transform.parent = entryPoints[entryPointIndex];

                }
                else if (entryPointIndex == 1)
                {
                    GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.Euler(0, 90, 0));
                    enemy.transform.parent = entryPoints[entryPointIndex];
                }
                availableEntryPoints.RemoveAt(randomIndex);
            }
        }
    }


    void Crystals()
    {
        Instantiate(prefabCrystals, originInstantiate.position, Quaternion.LookRotation(originInstantiate.forward));
    }

    //Funciones que se activan en las animaciones
    #region active attacks
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
    #endregion
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(peckAttack.position, radiusPeckAttack);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(jumpAttack.position, radiusJumpAttack);
    }
}
