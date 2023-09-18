using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds_sc : MonoBehaviour
{
    [SerializeField] AudioSource hurt, die, punch, walk, boost, stunned;
    public void HurtSound()
    {
        hurt.Play();
    }
    public void DieSound()
    {
        die.Play();
    }
    public void PunchSound()
    {
        punch.Play();
    }
    public void WalkSound()
    {
        walk.Play();
    }
    public void BoostSound()
    {
        boost.Play();
    }
    public void StunnedSound()
    {
        stunned.Play();
    }
}
