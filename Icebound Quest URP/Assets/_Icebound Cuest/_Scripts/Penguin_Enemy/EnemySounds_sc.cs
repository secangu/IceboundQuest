using UnityEngine;

public class EnemySounds_sc : MonoBehaviour
{
    [SerializeField] AudioSource alert, seen, attack, hurt, die;

    public void Alert()
    {
        alert.Play();
    }
    public void SeenSound()
    {
        seen.Play();
    }
    public void AttackSound()
    {
        attack.Play();
    }
    public void HurtSound()
    {
        hurt.Play();
    }
    public void dieSound() { die.Play(); }

}
