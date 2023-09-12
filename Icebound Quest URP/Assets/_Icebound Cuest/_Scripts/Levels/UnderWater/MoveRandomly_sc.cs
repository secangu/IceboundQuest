using UnityEngine;

public class MoveRandomly_sc : MonoBehaviour
{
    [SerializeField] float moveSpeed;          // Velocidad de movimiento del objeto.
    [SerializeField] float rotationSpeed;     // Velocidad de rotación del objeto.
    [SerializeField] float changeInterval;     // Intervalo de cambio de dirección.
    [SerializeField] float rotationSmoothing;  // Suavizado de rotación.

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
        // Si ha pasado el intervalo de cambio de dirección, cambia la dirección.
        if (Time.time >= changeTime)
        {
            ChangeDirection();
        }

        // Mueve el objeto hacia adelante en su dirección actual.
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        // Suaviza la rotación hacia la dirección actual.
        Quaternion targetRotation = Quaternion.LookRotation(randomDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothing * Time.deltaTime);
    }

    private void ChangeDirection()
    {
        // Genera una nueva dirección de movimiento aleatoria.
        randomDirection = Random.insideUnitSphere;

        // Normaliza la dirección y establece la nueva posición de cambio.
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
