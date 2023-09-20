using System.Collections;
using UnityEngine;

public class PlayerHealth_sc : MonoBehaviour
{
    AudioController_sc audioController;

    HeartSystem_sc heartSystem;
    ProjectileThrow projectileThrow;
    PlayerMovement_sc playerMovement;
    Animator animator;
    [SerializeField] bool boss;
    [SerializeField] float health;
    [SerializeField] float maxHealth;
    [SerializeField] GameObject _death;
    [SerializeField] GameObject _counterController;
    public float Health { get => health; set => health = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    void Start()
    {
        audioController = FindObjectOfType<AudioController_sc>();
        playerMovement = GetComponent<PlayerMovement_sc>();
        heartSystem = FindObjectOfType<HeartSystem_sc>();
        projectileThrow = FindObjectOfType<ProjectileThrow>();
        animator = GetComponent<Animator>();
        if (boss) health = PlayerPrefs.GetFloat("HealthPlayer"); else health = maxHealth;
        heartSystem.DrawHearts();
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        PlayerPrefs.SetFloat("HealthPlayer", Health);
        heartSystem.DrawHearts();
        if (projectileThrow != null) projectileThrow.WhileThrowing();
        if (Health <= 0)
        {
            gameObject.tag = "wa";
            StartCoroutine(CorroutineDeath());
        }
        else
        {
            animator.SetTrigger("Damage");
        }
    }
    public void StunningTakeDamage(float damage)
    {
        Health -= damage;
        PlayerPrefs.SetFloat("HealthPlayer", Health);
        heartSystem.DrawHearts();

        if (playerMovement.SlideLoop || playerMovement.IsSliding && playerMovement.SlideAttackTimer <= 0)
            playerMovement.SlideAttackTimer = 10;

        if (projectileThrow != null) projectileThrow.WhileThrowing();

        if (Health <= 0)
        {
            gameObject.tag = "wa";
            StartCoroutine(CorroutineDeath());
        }
        else
        {
            animator.SetTrigger("Stunning");
        }
    }
    IEnumerator CorroutineDeath()
    {
        if (_counterController != null) _counterController.SetActive(false);
        animator.SetTrigger("Die");
        audioController.StopSounds();
        yield return new WaitForSeconds(2.5f);
        Time.timeScale = 0;
        _death.SetActive(true);
    }
}
