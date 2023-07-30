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
    [SerializeField] float radiusFront;
    [SerializeField] float distanceFrontBox;
    RaycastHit hitFront;
    bool front;

    [Header("DetectorEspalda")]
    //Detector Espalda
    [SerializeField] Transform backCheckPlayer;
    [SerializeField] float distanceBackBox;
    [SerializeField] float radiusBackBox;
    RaycastHit hitBack;
    bool back;

    [Header("SignoExclamacion")]
    //Cambiar color de signo
    [SerializeField] GameObject exclamationMark;
    Material material;
    Color color;
    [SerializeField] bool playerDetected;
    [SerializeField] float intensityEmission;
    EnemySounds_sc enemySounds;

    public bool PlayerDetected { get => playerDetected; set => playerDetected = value; }
    public Color Color { get => color; set => color = value; }

    void Start()
    {
        material = exclamationMark.GetComponent<Renderer>().material;
        Color = material.color;
        Color = Color.green;
        enemySounds=GetComponent<EnemySounds_sc>();
    }

    void Update()
    {
        PlayerDetected = false;
        front = false;
        back = false;
        material.color = color;
        material.SetColor("_EmissionColor", color*2);
        //Rayo detecta al jugador con la "Vista"
        if (frontCheckPlayer != null)
        {
            if (Physics.SphereCast(frontCheckPlayer.position, radiusFront, frontCheckPlayer.forward, out hitFront,
                distanceFrontBox))
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
            if (Physics.SphereCast(backCheckPlayer.position, radiusBackBox, backCheckPlayer.forward, out hitBack,
               distanceBackBox))
            {
                if (hitBack.collider.tag == "Player")
                {
                    PlayerDetected = true;
                    back = true;
                }
            }
        }
        if(PlayerDetected) enemySounds.SeenSound(); 

    }
    private void OnDrawGizmos()
    {
        if (playerDetected && front)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(frontCheckPlayer.position, frontCheckPlayer.forward * hitFront.distance);
            Gizmos.DrawWireSphere(frontCheckPlayer.position + frontCheckPlayer.forward * hitFront.distance, radiusFront);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(frontCheckPlayer.position, frontCheckPlayer.forward * distanceFrontBox);
            Gizmos.DrawWireSphere(frontCheckPlayer.position + frontCheckPlayer.forward * hitFront.distance, radiusFront);

        }
        if (playerDetected && back)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(backCheckPlayer.position, backCheckPlayer.forward * hitBack.distance);
            Gizmos.DrawWireSphere(backCheckPlayer.position + backCheckPlayer.forward * hitBack.distance, radiusBackBox);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(backCheckPlayer.position, backCheckPlayer.forward * distanceBackBox);
            Gizmos.DrawWireSphere(backCheckPlayer.position + backCheckPlayer.forward * hitBack.distance, radiusBackBox);
        }

    }
}