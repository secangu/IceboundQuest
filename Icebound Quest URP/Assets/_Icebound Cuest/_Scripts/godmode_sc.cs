using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class godmode_sc : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float flySpeed = 10f;

    [SerializeField] PlayerMovement_sc playermove; 
    private bool isFlying = false;
    Rigidbody rb;
    void Start()
    {
        playermove=GetComponent<PlayerMovement_sc>();
        playermove.enabled = false;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }
    void Update()
    {
        playermove.enabled = false;

        if (Input.GetKey(KeyCode.O))
        {
            playermove.enabled = true; 
            rb.isKinematic = false;
            this.enabled = false;
        }
        // Obtener las entradas de movimiento horizontal y vertical
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calcular el desplazamiento del movimiento
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical) * moveSpeed * Time.deltaTime;

        // Aplicar el desplazamiento al jugador
        transform.Translate(movement);
        bool jump = Input.GetKeyDown(KeyCode.Space);

        // Cambiar el estado de volar al presionar o soltar la barra espaciadora
        if (jump)
        {
            isFlying = !isFlying;
        }

        // Aplicar el movimiento de vuelo si está activado
        if (isFlying)
        {
            // Obtener la entrada vertical para el vuelo
            float flyVertical = Input.GetAxis("Vertical");

            // Calcular el desplazamiento del vuelo
            Vector3 flyMovement = Vector3.up.normalized * flyVertical * flySpeed * Time.deltaTime;

            // Aplicar el desplazamiento del vuelo al jugador
            transform.Translate(flyMovement);
        }

        // Obtener la entrada de rotación horizontal

        // Calcular la rotación en función de la entrada horizontal

        // Aplicar la rotación al jugador
    }
    
}
