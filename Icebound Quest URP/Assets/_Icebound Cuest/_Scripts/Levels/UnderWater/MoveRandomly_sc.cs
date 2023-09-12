using UnityEngine;

public class MoveRandomly_sc : MonoBehaviour
{
    [SerializeField] float moveSpeed;          // Velocidad de movimiento del objeto.
    [SerializeField] float rotationSpeed;     // Velocidad de rotaci�n del objeto.
    [SerializeField] float changeInterval;     // Intervalo de cambio de direcci�n.
    [SerializeField] float rotationSmoothing;  // Suavizado de rotaci�n.

    [SerializeField] int damage;
    Vector3 initialPosition;
    Vector3 randomDirection;
    float changeTime;
    float timerDamage;

    private void Start()
    {
        initialPosition = transform.position;
        ChangeDirection();
    }

    private void Update()
    {
        // Si ha pasado el intervalo de cambio de direcci�n, cambia la direcci�n.
        if (Time.time >= changeTime)
        {
            ChangeDirection();
        }

        // Mueve el objeto hacia adelante en su direcci�n actual.
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        // Suaviza la rotaci�n hacia la direcci�n actual.
        Quaternion targetRotation = Quaternion.LookRotation(randomDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothing * Time.deltaTime);
    }

    private void ChangeDirection()
    {
        // Genera una nueva direcci�n de movimiento aleatoria.
        randomDirection = Random.insideUnitSphere;

        // Normaliza la direcci�n y establece la nueva posici�n de cambio.
        randomDirection.Normalize();
        changeTime = Time.time + changeInterval;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(timerDamage <= 0)
            {
                timerDamage = 1.5f;
                other.GetComponent<PlayerHealth_sc>().TakeDamage(damage);
            }
            else
            {
                timerDamage -= Time.deltaTime;
            }                        
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timerDamage = 0;
        }
    }
}
