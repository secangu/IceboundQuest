using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrollGameObjects_sc : MonoBehaviour
{
    EnemyPatrollState_sc patrollState;

    NavMeshAgent agent;
    List<Transform> wayPoints = new List<Transform>();

    [SerializeField] GameObject go;

    public GameObject Go { get => go; set => go = value; }

    private void Start()
    {
        Animator animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        foreach (Transform t in Go.transform) wayPoints.Add(t); // recorre los elementos hijos del objeto

        // Obtener todos los estados del Animator
        StateMachineBehaviour[] stateMachineBehaviours = animator.GetBehaviours<StateMachineBehaviour>();

        // Buscar el estadi deseado dentro de todos los estados
        foreach (StateMachineBehaviour stateMachineBehaviour in stateMachineBehaviours)
        {
            if (stateMachineBehaviour is EnemyPatrollState_sc)
            {
                patrollState = stateMachineBehaviour as EnemyPatrollState_sc;


                agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)].position); //define su primer destino        
                break;
            }
        }
    }

    private void Update()
    {
        if (patrollState != null)
        {
            if (patrollState.Patrolling)
            {
                if (agent.remainingDistance <= agent.stoppingDistance) // detecta si llego al destino y actualiza a un nuevo destino
                    agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)].position);
            }
            else agent.SetDestination(agent.transform.position); // Deja de patrullar y se queda en esa posicion
        }
    }
}
