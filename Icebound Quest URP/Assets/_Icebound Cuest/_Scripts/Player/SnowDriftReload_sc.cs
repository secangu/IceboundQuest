using UnityEngine;

public class SnowDriftReload_sc : MonoBehaviour
{
    ProjectileThrow projectileThrow;
    bool reload;

    [SerializeField] GameObject textGameObject;
    [SerializeField]ParticleSystem particles;
    [SerializeField] GameObject particlesCanReload;
    void Start()
    {
        projectileThrow = FindObjectOfType<ProjectileThrow>();
        textGameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && reload)
        {
            projectileThrow.Reload();
            particles.Play();
            Destroy(gameObject, 1.20f);
        }
        if(projectileThrow.CurrentBall < projectileThrow.MaxBall) particlesCanReload.SetActive(true);else particlesCanReload.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")&& projectileThrow.CurrentBall < projectileThrow.MaxBall)
        {
            reload = true;
            textGameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            reload = false;
            textGameObject.SetActive(false);
        }
    }
}
