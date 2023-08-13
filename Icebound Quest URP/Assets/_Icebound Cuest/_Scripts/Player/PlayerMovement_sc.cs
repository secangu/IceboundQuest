using UnityEngine;

public class PlayerMovement_sc : MonoBehaviour
{
    [SerializeField] Transform _camera; //camara que sigue al jugador
    Rigidbody _rbPlayer;
    Animator _animator;
    [SerializeField] int random;
    float timer;
    bool _snowDrift; // bool para reducir la velocidad del player

    [Header("Movement")]

    [SerializeField] float transitionTime; /*Hace que la velocidad cambie gradualmente*/
    [SerializeField] float _timeSmoothRotation; //Establece el tiempo de la rotacion al momento de caminar en diferentes direcciones
    [SerializeField] float walkSpeed, runSpeed, slideSpeed;
    [SerializeField] float speed, currentSpeed;
    float _velocitySmoothRotation; //ajusta la velocidad la rotacion al momento de caminar en diferentes direcciones 

    [Header("Jump")]

    [SerializeField] float jumpForce;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] bool isGrounded;

    [Header("Gizmos")]

    [SerializeField] Transform _groundCheck;
    [SerializeField] float _groundCheckRadius;

    [Header("Slide")]
    bool _isSlide;
    bool slideLoop;
    float slideAnimationTimer; // tiempo para activar la animacion de slideloop
    float slideAttackTimer; // tiempo que tarda en deslizarse nuevamente si choco con un enemigo

    public float JumpForce { get => jumpForce; set => jumpForce = value; }
    public bool SlideLoop { get => slideLoop; set => slideLoop = value; }

    void Start()
    {
        _rbPlayer = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _rbPlayer.isKinematic = false;
    }

    void Update()
    {
        Movement();
    }
    public void Movement()
    {
        float horizontal;
        float vertical;

        if (_isSlide && slideAttackTimer <= 0)
        {
            vertical = 1;
            horizontal = 0;
            currentSpeed = slideSpeed;

            if (_snowDrift)
            {
                currentSpeed -= 70 * Time.deltaTime;                
            }
            else if (_snowDrift && currentSpeed < 3.5f)
            {
                _isSlide = false; SlideLoop = false; slideAnimationTimer = 0;
            }
        }
        else
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            currentSpeed = horizontal != 0 || vertical != 0 ? (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed) : 0;
            if (_snowDrift) currentSpeed = horizontal != 0 || vertical != 0 ? currentSpeed < 1 ? currentSpeed = 1 : currentSpeed -= 50 * Time.deltaTime : 0;
        }

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized; //Recibe input horizontal y vertical
        isGrounded = Physics.CheckSphere(transform.position, _groundCheckRadius, _groundLayer); //Comprueba si el jugador esta en el suelo

        //Detecta si esta caminando o corriendo
        if (speed != currentSpeed) speed = Mathf.MoveTowards(speed, currentSpeed, transitionTime * Time.deltaTime);

        if (direction.magnitude >= 0.1f)
        {
            //Rota al player segun la vista de la camara y suaviza esta rotacion
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _velocitySmoothRotation, _timeSmoothRotation);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;//detecta en que rotacion debe caminar
            _rbPlayer.MovePosition(_rbPlayer.position + moveDirection * speed * Time.deltaTime);//mueve al player

            random = Random.Range(0, 3);//Genera un random entre las animaciones idle
        }
        else
        {
            if (timer > 1.5f) _animator.SetFloat("IdleNumber", random); else timer += Time.deltaTime;
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            _rbPlayer.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);


        //Controla cuando se desliza el jugador
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _isSlide = !_isSlide;
            SlideLoop = false;
            slideAnimationTimer = 0;

        }
        if (_isSlide)
        {
            if (slideAnimationTimer > 0.14f)
            {
                SlideLoop = true;
            }
            else
            {
                slideAnimationTimer += Time.deltaTime;
            }
        }

        if (slideAttackTimer > 0)
        {
            slideAttackTimer -= Time.deltaTime;
            _isSlide = false; SlideLoop = false; slideAnimationTimer = 0;
        }



        _animator.SetBool("IsGrounded", isGrounded);
        _animator.SetFloat("Speed", speed);
        _animator.SetBool("Slide", _isSlide);
        _animator.SetBool("SlideLoop", SlideLoop);
    }


    private void OnCollisionStay(Collision other)
    {

        if (other.gameObject.tag == ("Wall") || other.gameObject.tag == ("Dissolve")) { _isSlide = false; SlideLoop = false; slideAnimationTimer = 0; }
        if (other.gameObject.tag == ("Enemy") && slideAttackTimer <= 0) { slideAttackTimer = 5; }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "SnowDrift") _snowDrift = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SnowDrift") _snowDrift = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);
    }
}
