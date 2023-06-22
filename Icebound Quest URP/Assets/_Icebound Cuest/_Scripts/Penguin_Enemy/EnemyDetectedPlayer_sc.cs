using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDetectedPlayer_sc : MonoBehaviour
{
    [SerializeField] LayerMask player;
    [Header("DetectorVista")]
    //Detector Vista
    [SerializeField] Transform frontCheckPlayer;
    [SerializeField] Vector3 sizeFrontBox;
    [SerializeField] float distanceFrontBox;
    RaycastHit hitFront;
    bool front;

    [Header("DetectorEspalda")]
    //Detector Espalda
    [SerializeField] Transform backCheckPlayer;
    [SerializeField] float distanceBackBox;
    [SerializeField] Vector3 sizeBackBox;
    RaycastHit hitBack;
    bool back;

    [Header("SignoExclamacion")]
    //Cambiar color de signo
    [SerializeField] GameObject exclamationMark;
    Material material;
    Color color;
    [SerializeField] bool playerDetected;

    public bool PlayerDetected { get => playerDetected; set => playerDetected = value; }
    public Color Color { get => color; set => color = value; }

    void Start()
    {
        material = exclamationMark.GetComponent<Renderer>().material;
        Color = material.color;
        Color = Color.green;
    }

    void Update()
    {
        PlayerDetected = false;
        front = false;
        back = false;
        material.color = color;
        /*No borrar el layer mask del physics porque por alguna razon detecta al player a 
         traves de algunas paredes*/

        //Rayo detecta al jugador con la "Vista"
        if (frontCheckPlayer != null)
        {
            if (Physics.BoxCast(frontCheckPlayer.position, sizeFrontBox, frontCheckPlayer.forward, out hitFront,
                frontCheckPlayer.rotation, distanceFrontBox,player))
            {
                if (hitFront.collider.tag == "Player")
                {
                    PlayerDetected = true;
                    front = true;
                }
            }


        }
        //Caja que detecta al jugador por la espalda
        if (backCheckPlayer != null)
        {
            if (Physics.BoxCast(backCheckPlayer.position, sizeBackBox, backCheckPlayer.forward, out hitBack,
                backCheckPlayer.rotation, distanceBackBox,player))
            {
                if (hitBack.collider.tag == "Player")
                {
                    PlayerDetected = true;
                    back = true;
                }
            }
        }

    }
    private void OnDrawGizmos()
    {
        if (playerDetected && front)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(frontCheckPlayer.position, frontCheckPlayer.forward * hitFront.distance);
            Gizmos.DrawWireCube(frontCheckPlayer.position + frontCheckPlayer.forward * hitFront.distance, sizeFrontBox);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(frontCheckPlayer.position, frontCheckPlayer.forward * distanceFrontBox);
        }
        if (playerDetected && back)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(backCheckPlayer.position, backCheckPlayer.forward * hitBack.distance);
            Gizmos.DrawWireCube(backCheckPlayer.position + backCheckPlayer.forward * hitBack.distance, sizeBackBox);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(backCheckPlayer.position, backCheckPlayer.forward * distanceBackBox);
        }

    }
}