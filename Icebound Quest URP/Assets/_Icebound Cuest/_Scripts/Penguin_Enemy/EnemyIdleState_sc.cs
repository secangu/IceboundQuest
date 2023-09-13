using UnityEngine;

public class EnemyIdleState_sc : StateMachineBehaviour
{
    float timer;
    [SerializeField] float timerIdle;

    [SerializeField] float distance; // distancia a la que esta del jugador
    [SerializeField] float alertDistance; // distancia minima para detectar al player
    Transform player;
    EnemyDetectedPlayer_sc enemy;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<EnemyDetectedPlayer_sc>();
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        enemy.Color = Color.green;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (timer > timerIdle) animator.SetBool("IsPatrolling", true);

        if (player != null) distance = Vector3.Distance(player.position, animator.transform.position);
        if (distance < alertDistance && enemy.TimeSinceAlert <= 0) animator.SetBool("IsAlert", true); else if (enemy.TimeSinceAlert > 0) enemy.TimeSinceAlert -= Time.deltaTime;
    }
}
