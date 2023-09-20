using UnityEngine;
using UnityEngine.AI;

public class BossChaseState_sc : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;

    int random;
    int previousRandom;
    [SerializeField] float timer;
    [SerializeField] float timerChasing;
    bool hasAttacked;

    [Header("Distances")]
    [SerializeField] float distance; // distancia a la que esta del jugador
    [SerializeField] float attackDistance; // distancia atacar al player
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        timer = 0;
        hasAttacked = false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        agent.speed = 2f;

        if (timer > timerChasing)
        {
            animator.SetBool("Chasing", false);
        }

        if (player != null)
        {
            agent.SetDestination(player.position);
            distance = Vector3.Distance(player.position, animator.transform.position);
        }

        if (distance <= attackDistance && !hasAttacked)
        {
            animator.SetTrigger("Attack");
            animator.SetBool("Chasing", false);
            hasAttacked = true;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(animator.transform.position);

        do
        {
            random = Random.Range(0, 3);
        } while (random == previousRandom && hasAttacked);

        previousRandom = random;

        animator.SetInteger("IdAttack", random);
        hasAttacked = false;
    }
}
