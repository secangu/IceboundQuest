using UnityEngine;

public class PlayerMovementSea_sc : MonoBehaviour
{
    [SerializeField] Transform _camera; //camara que sigue al jugador
    [SerializeField] PlayerButtonsController_sc boostButtonScript;
    Rigidbody _rbPlayer;
    Animator _animator;

    [Header("Movement")]

    [SerializeField] float transitionTime; /*Hace que la velocidad cambie gradualmente*/
    [SerializeField] float _timeSmoothRotation; //Establece el tiempo de la rotacion al momento de caminar en diferentes direcciones
    [SerializeField] float swimmingSpeed, fastSwimmingSpeed;
    [SerializeField] float speed, currentSpeed;
    [SerializeField] bool canSwim=true;

    [Header("Boost")]
    [SerializeField] float boostForce;

    [Header("Slide")]
    [SerializeField] float slideBoostTimer; // tiempo que tarda en impulsarse nuevamente si choco con un enemigo    



    void Start()
    {
        _rbPlayer = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _rbPlayer.isKinematic = false;
        canSwim = true;
    }

    void Update()
    {
        Movement();
    }
    public void Movement()
    {
        if(canSwim)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            currentSpeed = horizontal != 0 || vertical != 0 ? (Input.GetKey(KeyCode.LeftShift) ? fastSwimmingSpeed : swimmingSpeed) : 0;

            Vector3 inputDirection = new Vector3(horizontal, 0, vertical).normalized;

            if (speed != currentSpeed) speed = Mathf.MoveTowards(speed, currentSpeed, transitionTime * Time.deltaTime);

            if (inputDirection.magnitude >= 0.1f)
            {
                // Rota al jugador según la vista de la cámara y suaviza esta rotación
                Vector3 cameraForward = _camera.transform.forward;
                Vector3 cameraRight = _camera.transform.right;

                Vector3 targetDirection = inputDirection.x * cameraRight + inputDirection.z * cameraForward;

                Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _timeSmoothRotation);

                // Detecta en qué dirección debe moverse el jugador
                Vector3 moveDirection = targetDirection.normalized;
                _rbPlayer.MovePosition(_rbPlayer.position + moveDirection * speed * Time.deltaTime); // Mueve al jugador
            }
            _animator.SetFloat("Speed", speed);
        }
        



        //Impulso 
        if (Input.GetKeyDown(KeyCode.LeftControl) && slideBoostTimer <= 0)
        {
            Vector3 forwardDirection = transform.forward;
            _rbPlayer.AddForce(forwardDirection * boostForce, ForceMode.Impulse);
            slideBoostTimer = 10;
        }

        if (slideBoostTimer > 0)
        {
            slideBoostTimer -= Time.deltaTime;
            if (boostButtonScript != null)
            {
                boostButtonScript.DisabledSprites();
                boostButtonScript.SliderValue(slideBoostTimer);
            }
        }
        else
        {
            if (boostButtonScript != null) boostButtonScript.ActiveSprites();
        }
    }
}
