using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TrajectoryPredictor))]
public class ProjectileThrow : MonoBehaviour
{
    TrajectoryPredictor trajectoryPredictor;
    [SerializeField] PlayerButtonsController_sc throwButtonScript;
    PlayerMovement_sc playerMovement;
    Animator animator;

    [SerializeField] GameObject throwCamera;
    [SerializeField] TextMeshProUGUI ballsAmount;
    [Header("Snow Ball")]
    [SerializeField] GameObject snowBallStack;
    List<Rigidbody> balls = new List<Rigidbody>(); // Lista para almacenar las instancias de bolas
    List<Rigidbody> ballInstances = new List<Rigidbody>(); // Lista para almacenar las bolas que se lanzaron

    [SerializeField] Rigidbody objectToThrow;
    [SerializeField, Range(0.0f, 50.0f)] float force;
    [SerializeField] Transform StartPosition;
    [SerializeField] float throwCooldown;
    [SerializeField] bool canThrow = true;
    int maxBall;
    int currentBall; // Bolas disponibles
    bool aiming = false;
    float throwTime;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement_sc>();
        animator = GetComponent<Animator>();
        trajectoryPredictor = GetComponent<TrajectoryPredictor>();
        throwTime = throwCooldown;

        foreach (Transform child in snowBallStack.transform)
        {
            Rigidbody rg = child.GetComponent<Rigidbody>();
            if (rg != null)
            {
                balls.Add(rg); // Agrega la bola a la lista
                rg.gameObject.SetActive(false); // Desactiva la bola inicialmente (para que no se vean en la escena)
            }
        }

        maxBall = balls.Count; // establece como maxball el tamaño de balas en la lista
        currentBall = maxBall; // Comienzas con la cantidad máxima de balas

        canThrow = true;
        throwButtonScript.ActiveSprites();


    }

    private void Update()
    {
        ballsAmount.text = currentBall + "/" + maxBall;
        animator.SetBool("Aiming", aiming);

        if (currentBall > 0 && throwTime <= 0)
        {
            throwButtonScript.ActiveSprites();
            if (Input.GetMouseButton(1) && canThrow && playerMovement.CanMove && playerMovement.IsGrounded)
            {
                playerMovement.CanMove = false;
                Predict(true);
                animator.SetTrigger("Aim");
                if (!aiming)
                {
                    throwCamera.SetActive(true);
                    aiming = true;
                    trajectoryPredictor.SetTrajectoryVisible(true);
                }
            }
            if (Input.GetMouseButtonUp(1) && canThrow)
            {
                WhileThrowing();
            }
        }
        else if (throwTime > 0)
        {
            throwButtonScript.DisabledSprites();
            throwButtonScript.SliderValue(throwTime);
            throwTime -= Time.deltaTime;
            canThrow = false;
            if (throwTime <= 0) canThrow = true;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
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

    public void ThrowObject()
    {
        if (currentBall > 0 && balls.Count > 0)
        {
            // Toma la primera bola de la lista
            Rigidbody thrownObject = balls[0];
            ballInstances.Add(thrownObject);

            // Reubica la bola en la posición de inicio y sin rotación
            thrownObject.transform.position = StartPosition.position;
            thrownObject.transform.rotation = Quaternion.identity;

            // Activa la bola si no estaba activa
            if (!thrownObject.gameObject.activeSelf)
                thrownObject.gameObject.SetActive(true);

            // Aplica una fuerza de impulso al objeto lanzado en la dirección especificada y con la fuerza determinada
            thrownObject.velocity = Vector3.zero;
            thrownObject.angularVelocity = Vector3.zero;
            thrownObject.AddForce(StartPosition.forward * force, ForceMode.Impulse);

            // Remueve la bola de la lista
            balls.RemoveAt(0);

            currentBall--; // Reduce la cantidad de bolas disponibles
        }
    }
    public void WhileThrowing()
    {
        if (aiming)
        {
            aiming = false;

            if (!aiming)
            {
                ThrowObject();
                throwTime = throwCooldown;
                animator.SetTrigger("Throw");
                Predict(false);
                throwCamera.SetActive(false);
                trajectoryPredictor.SetTrajectoryVisible(false);
                playerMovement.CanMove = true;
            }
        }
    }
    private void Reload()
    {
        if (currentBall < maxBall)
        {
            int remainingAmmo = maxBall - currentBall;
            int ammoToReload = Mathf.Min(remainingAmmo, ballInstances.Count); // Calcula cuántas balas se pueden recargar

            for (int i = 0; i < ammoToReload; i++)
            {
                Rigidbody ammoInstance = ballInstances[i];
                ammoInstance.GetComponent<MeshRenderer>().enabled = true;
                ammoInstance.GetComponent<Collider>().enabled = true;
                ammoInstance.isKinematic = false;
                ammoInstance.gameObject.SetActive(false); // Desactiva la bala
                ammoInstance.velocity = Vector3.zero; // Reinicia la velocidad
                ammoInstance.angularVelocity = Vector3.zero; // Reinicia la velocidad angular
                ammoInstance.transform.position = StartPosition.position; // Reposiciona la bala
                ammoInstance.transform.rotation = Quaternion.identity; // Reinicia la rotación
            }
            balls.AddRange(ballInstances); //recarga la lista encargada del disparo
            currentBall += ammoToReload; // Incrementa la cantidad de balas disponibles

            ballInstances.Clear(); // limpia la lista de recarga
        }
    }
}