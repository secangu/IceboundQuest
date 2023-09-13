using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrollGameObjects_sc : MonoBehaviour
{
    EnemyPatrollState_sc patrollState;

    NavMeshAgent agent;
    List<Transform> wayPoints = new List<Transform>();
    int currentWaypointIndex;
    [SerializeField] GameObject go;
    bool hasNewDestination;
    bool move;
    public GameObject Go { get => go; set => go = value; }

    private void Start()
    {
        Animator animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        foreach (Transform t in Go.transform) wayPoints.Add(t); // recorre los elementos hijos del objeto

        // Obtener todos los estados del Animator
        StateMachineBehaviour[] stateMachineBehaviours = animator.GetBehaviours<StateMachineBehaviour>();

        // Buscar el estado deseado dentro de todos los estados
        foreach (StateMachineBehaviour stateMachineBehaviour in stateMachineBehaviours)
        {
            if (stateMachineBehaviour is EnemyPatrollState_sc)
            {
                patrollState = stateMachineBehaviour as EnemyPatrollState_sc;

                agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)].position); // define su primer destino
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
                Debug.Log(currentWaypointIndex); // Puedes agregar esto para depuración
                Debug.Log(hasNewDestination); // Puedes agregar esto para depuración
                agent.SetDestination(wayPoints[currentWaypointIndex].position);
                if (hasNewDestination && agent.remainingDistance <= agent.stoppingDistance)
                {
                    // Detecta si llegó al destino y actualiza a un nuevo destino diferente al actual
                    int newWaypointIndex = currentWaypointIndex;
                    while (newWaypointIndex == currentWaypointIndex)
                    {
                        newWaypointIndex = Random.Range(0, wayPoints.Count);
                    }
                    currentWaypointIndex = newWaypointIndex;
                    agent.SetDestination(wayPoints[currentWaypointIndex].position);

                    hasNewDestination = false; // Marca que se ha elegido un nuevo destino
                }
                else if (!hasNewDestination && agent.remainingDistance <= agent.stoppingDistance && move)
                {
                    hasNewDestination = true;
                }
                move = true;
            }
            else
            {
                agent.SetDestination(agent.transform.position); // Deja de patrullar y se queda en esa posición
                move = false;
            }
        }
    }
}
