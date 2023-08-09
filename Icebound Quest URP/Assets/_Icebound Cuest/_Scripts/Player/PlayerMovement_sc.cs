using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Timers;

public class PlayerMovement_sc : MonoBehaviour
{
    [SerializeField] Transform _camera; //camara que sigue al jugador
    Rigidbody _rbPlayer;
    Animator _animator;
    [SerializeField] int random;
    float timer;
    [Header("Movement")]
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float slideSpeed;
    [SerializeField] float speed, currentSpeed, transitionTime;
    [SerializeField] float jumpForce;
    [SerializeField] float _timeSmoothRotation; //Establece el tiempo de la rotacion al momento de caminar en diferentes direcciones
    float _velocitySmoothRotation; //ajusta la velocidad la rotacion al momento de caminar en diferentes direcciones 

    [Header("Jump")]
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] bool isGrounded;

    [Header("Gizmos")]
    [SerializeField] Transform _groundCheck;
    [SerializeField] float _groundCheckRadius;

    [Header("Slide")]
    bool _isSlide;

    public float JumpForce { get => jumpForce; set => jumpForce = value; }

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

        if (_isSlide)
        {
            vertical = 1;
            horizontal = 0;
            currentSpeed = slideSpeed;
        }
        else
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            currentSpeed = horizontal != 0 || vertical != 0 ? (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed) : 0;
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            _isSlide = true;
            
        }if(Input.GetKeyUp(KeyCode.R))_isSlide = false;

        _animator.SetBool("IsGrounded", isGrounded);
        _animator.SetFloat("Speed", speed);
        _animator.SetBool("Slide", _isSlide);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);
    }
}
