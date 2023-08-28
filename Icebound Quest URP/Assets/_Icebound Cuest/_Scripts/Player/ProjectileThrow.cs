using UnityEngine;

[RequireComponent(typeof(TrajectoryPredictor))]
public class ProjectileThrow : MonoBehaviour
{
    TrajectoryPredictor trajectoryPredictor;
    PlayerButtonsController_sc throwButtonScript;

    [SerializeField] Rigidbody objectToThrow;
    [SerializeField, Range(0.0f, 50.0f)] float force;
    [SerializeField] Transform StartPosition;

    [SerializeField] float throwCooldown; // Tiempo de espera entre lanzamientos
    float throwTime; // �ltimo tiempo en que se lanz� un proyectil

    private void Start()
    {
        trajectoryPredictor = GetComponent<TrajectoryPredictor>();

        if (StartPosition == null)
            StartPosition = transform;

        throwTime = throwCooldown; // Inicializa lastThrowTime para permitir el primer lanzamiento
    }

    private void Update()
    {
        Predict(); // Llama al m�todo para predecir y visualizar la trayectoria

        throwTime -= Time.deltaTime; // Actualiza el tiempo transcurrido desde el �ltimo lanzamiento

        if (Input.GetMouseButtonDown(1) && throwTime >= throwCooldown) // Bot�n derecho del mouse para lanzar el proyectil
        {
            ThrowObject();
            throwTime = throwCooldown; // Reinicia el tiempo transcurrido
        }
    }

    private void Predict()
    {
        trajectoryPredictor.PredictTrajectory(ProjectileData());
    }

    private ProjectileProperties ProjectileData()
    {
        ProjectileProperties properties = new ProjectileProperties();
        Rigidbody r = objectToThrow.GetComponent<Rigidbody>();

        properties.direction = StartPosition.forward;
        properties.initialPosition = StartPosition.position;
        properties.initialSpeed = force;
        properties.mass = r.mass;
        properties.drag = r.drag;

        return properties;
    }

    private void ThrowObject()
    {
        // Crea una instancia del objeto a lanzar en la posici�n de inicio y sin rotaci�n
        Rigidbody thrownObject = Instantiate(objectToThrow, StartPosition.position, Quaternion.identity);

        // Agrega una fuerza de impulso al objeto lanzado en la direcci�n especificada y con la fuerza determinada
        thrownObject.AddForce(StartPosition.forward * force, ForceMode.Impulse);
    }
}
