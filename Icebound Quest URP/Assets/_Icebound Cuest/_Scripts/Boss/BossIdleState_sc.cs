using UnityEngine;

public class BossIdleState_sc : StateMachineBehaviour
{
    Transform player;

    int random;
    int previousRandom;
    float timer;
    [SerializeField] float timerIdle;
    bool hasAttacked;
    [Header("Distances")]
    [SerializeField] float playerDistance; // distancia a la que esta del jugador
    [SerializeField] float attackDistance; // distancia minima para detectar al player
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        timer = 0;
        hasAttacked = false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;

        if (timer > timerIdle)
        {
            animator.SetBool("Chasing", true);
        }

        if (player != null)
        {
            playerDistance = Vector3.Distance(player.position, animator.transform.position);
        }

        if (playerDistance <= attackDistance && !hasAttacked)
        {
            animator.SetTrigger("Attack");
            hasAttacked = true;
        }

    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        do
        {
            random = Random.Range(0, 3);
        } while (random == previousRandom && hasAttacked);

        previousRandom = random;

        animator.SetInteger("IdAttack", random);
        hasAttacked = false;
    }
}
