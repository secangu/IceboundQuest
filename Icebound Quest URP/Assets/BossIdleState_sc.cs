using UnityEngine;

public class BossIdleState_sc : StateMachineBehaviour
{
    Transform player;

    int random;
    float timer;
    [SerializeField] float timerIdle;
    bool hasAttacked;
    [Header("Distances")]
    [SerializeField] float playerDistance; // distancia a la que esta del jugador
    [SerializeField] float attackDistance; // distancia minima para detectar al player
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
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
        random = Random.Range(0, 3);
        animator.SetFloat("IdAttack", random);
        hasAttacked = false;
    }
}
