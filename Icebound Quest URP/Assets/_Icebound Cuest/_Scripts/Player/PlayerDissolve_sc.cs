using UnityEngine;

public class PlayerDissolve_sc : MonoBehaviour
{
    Animator animator;
    PlayerMovement_sc playerMovement ;

    [SerializeField] Transform rayCheckDissolve;
    [SerializeField] AudioSource _meltSound;
    [SerializeField] float time;
    [SerializeField] float dissolveTime;
    [SerializeField] bool freezed;
    [SerializeField] bool melted;
    [SerializeField] bool dissolve;
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement= GetComponent<PlayerMovement_sc>();
    }

    void Update()
    {
        Dissolve();

        if (time > 0)
        {
            time -= Time.deltaTime;
        }
       
    }
    
    public void Dissolve()
    {
        dissolve = false;
        if (rayCheckDissolve != null)
        {
            RaycastHit hitDissolve;
            Ray rayDissolve = new Ray(rayCheckDissolve.position, rayCheckDissolve.forward);

            if (Physics.Raycast(rayDissolve, out hitDissolve, 4f))
            {
                if (hitDissolve.collider.tag == ("Dissolve"))
                {                    
                    dissolve = true;
                    if(Input.GetKeyDown(KeyCode.Q) && dissolve && time <= 0)
                    {
                        time = dissolveTime;
                        _meltSound.Play();
                        freezed = hitDissolve.transform.GetComponent<DissolveIce_sc>().Freez; //obtiene si el objeto esta congelado o derretido
                        melted = hitDissolve.transform.GetComponent<DissolveIce_sc>().Melt;

                        if (freezed) animator.SetTrigger("Melted"); //si el objeto esta congelado hacer animacion de derretir y viceversa
                        if (melted) animator.SetTrigger("Freezed");
                        playerMovement.CancelSlide();
                        hitDissolve.transform.GetComponent<DissolveIce_sc>().CanDissolve(); // pasa info de derretir o congelar
                    }                    
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
