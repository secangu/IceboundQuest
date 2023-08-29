using UnityEngine;

[RequireComponent(typeof(TrajectoryPredictor))]
public class ProjectileThrow : MonoBehaviour
{
    TrajectoryPredictor trajectoryPredictor;
    
    [SerializeField] PlayerButtonsController_sc throwButtonScript;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody objectToThrow;

    [SerializeField, Range(0.0f, 50.0f)] float force;

    [SerializeField] Transform StartPosition;
    [SerializeField] GameObject throwCamera;

    [SerializeField] float throwCooldown;
    [SerializeField] bool canThrow=true;
    bool aiming = false;

    float throwTime; 

    private void Start()
    {
        trajectoryPredictor = GetComponent<TrajectoryPredictor>();
        canThrow = true;
        throwButtonScript.ActiveSprites();
    }

    private void Update()
    {
        if (throwTime <= 0 && canThrow)
        {
            throwButtonScript.ActiveSprites();
            if (canThrow)
            {
                if (Input.GetMouseButton(1))
                {
                    throwCamera.SetActive(true);
                    Predict(true);
                    if (!aiming)
                    {
                        animator.SetTrigger("Aim");
                        aiming = true;
                        trajectoryPredictor.SetTrajectoryVisible(true);
                    }
                }
                if (Input.GetMouseButtonUp(1))
                {
                    throwTime = throwCooldown;
                    animator.SetTrigger("Throw");
                    aiming = false;

                    ThrowObject();

                    Predict(false);
                    throwCamera.SetActive(false);
                    trajectoryPredictor.SetTrajectoryVisible(false);
                }
            }            
        }
        else if (throwTime > 0)
        {
            throwButtonScript.DisabledSprites();
            throwButtonScript.SliderValue(throwTime);
            throwTime -= Time.deltaTime;
        }
    }

    private void Predict(bool Predict)
    {
        if (Predict) trajectoryPredictor.PredictTrajectory(ProjectileData());
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
        // Crea una instancia del objeto a lanzar en la posición de inicio y sin rotación
        Rigidbody thrownObject = Instantiate(objectToThrow, StartPosition.position, Quaternion.identity);

        // Agrega una fuerza de impulso al objeto lanzado en la dirección especificada y con la fuerza determinada
        thrownObject.AddForce(StartPosition.forward * force, ForceMode.Impulse);
    }
}