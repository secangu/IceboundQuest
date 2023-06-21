using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrollState_sc : StateMachineBehaviour
{
    float timer;
    [SerializeField]float timerPatrolling;

    [SerializeField] float distance; // distancia a la que esta del jugador
    [SerializeField] float alertDistance; // distancia minima para detectar al player y entrar en alerta
    
    Transform player;

    List<Transform> wayPoints = new List<Transform>();
    NavMeshAgent agent;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        
        agent.speed = 1f;
        timer = 0;
        
        GameObject go = GameObject.FindGameObjectWithTag("WayPoints");
        foreach (Transform t in go.transform) wayPoints.Add(t); // recorre los elementos hijos del objeto con tag WayPoints
        agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)].position); //define su primer destino
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer +=Time.deltaTime;
        if(timer > timerPatrolling) animator.SetBool("IsPatrolling", false); //cuando timer supere a timerpatrolling dejara de patrullar y pasa a idle

        if (agent.remainingDistance <= agent.stoppingDistance) // detecta si llego al destino y actualiza a un nuevo destino
            agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)].position);

        distance = Vector3.Distance(player.position, animator.transform.position);
        if (distance < alertDistance) animator.SetBool("IsAlert", true); //detecta si el jugador esta cerca y pasa a modo alerta
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position); // Deja de patrullar y se queda en esa posicion
    }    
}
