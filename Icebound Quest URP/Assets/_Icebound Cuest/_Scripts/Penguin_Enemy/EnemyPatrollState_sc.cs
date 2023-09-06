using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrollState_sc : StateMachineBehaviour
{
    float timer;
    [SerializeField] float timerPatrolling;

    [SerializeField] float distance; // distancia a la que esta del jugador
    [SerializeField] float alertDistance; // distancia minima para detectar al player y entrar en alerta
    int random;
    Transform player;

    NavMeshAgent agent;
    bool patrolling; // bool que le dice al script de PatrollGameObjects que inicio a patrullar 
    EnemyDetectedPlayer_sc enemy;
    public bool Patrolling { get => patrolling; set => patrolling = value; }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<EnemyDetectedPlayer_sc>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        agent = animator.GetComponent<NavMeshAgent>();

        enemy.Color = Color.green;

        agent.speed = 1f;
        timer = 0;

        Patrolling = true;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (timer > timerPatrolling) animator.SetBool("IsPatrolling", false); //cuando timer supere a timerpatrolling dejara de patrullar y pasa a idle        

        if (player != null) distance = Vector3.Distance(player.position, animator.transform.position);
        if (distance < alertDistance) animator.SetBool("IsAlert", true); //detecta si el jugador esta cerca y pasa a modo alerta
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        patrolling = false;
        random = Random.Range(0, 2);
        animator.SetFloat("Idle", random);

    }
}
