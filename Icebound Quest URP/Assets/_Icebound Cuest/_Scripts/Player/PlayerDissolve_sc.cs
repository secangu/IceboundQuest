using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDissolve_sc : MonoBehaviour
{
    Animator animator;
    [SerializeField] MonoBehaviour[] scripts;

    [SerializeField] Transform rayCheckDissolve;

    [SerializeField] float time;
    [SerializeField] float dissolveTime;
    [SerializeField] float checkDissolveRadius;
    [SerializeField] bool freezed;
    [SerializeField] bool melted;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (time > 0)
        {
            for (int i = 0; i < scripts.Length; i++)
            {
                scripts[i].enabled = false; //Desactiva scripts mientras hace la accion
            }
            time -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Q) && time <= 0)
        {
            Dissolve();
            time = dissolveTime;
            //sonidoderretir.Play();
        }

        if (time <= 0)
        {
            for (int i = 0; i < scripts.Length; i++)
            {
                scripts[i].enabled = true; //Activa nuevamente los scripts desactivados
            }
        }
    }
    public void Dissolve()
    {
        if (rayCheckDissolve != null)
        {
            RaycastHit hitDissolve;
            Ray rayDissolve = new Ray(rayCheckDissolve.position, rayCheckDissolve.forward);

            if (Physics.Raycast(rayDissolve, out hitDissolve))
            {
                if (hitDissolve.collider.tag == ("Dissolve"))
                {
                    freezed = hitDissolve.transform.GetComponent<DissolveIce_sc>().Freez; //obtiene si el objeto esta congelado o derretido
                    melted = hitDissolve.transform.GetComponent<DissolveIce_sc>().Melt;

                    if (freezed) animator.SetTrigger("Melted"); //si el objeto esta congelado hacer animacion de derretir y viceversa
                    if (melted) animator.SetTrigger("Freezed");

                    hitDissolve.transform.GetComponent<DissolveIce_sc>().CanDissolve(); // pasa info de derretir o congelar

                    Debug.DrawRay(rayDissolve.origin, rayDissolve.direction * 4f, Color.red);
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(rayCheckDissolve.position, rayCheckDissolve.TransformDirection(Vector3.forward) * 4f);
    }
}
