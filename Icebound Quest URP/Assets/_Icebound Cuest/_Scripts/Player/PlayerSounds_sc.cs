using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds_sc : MonoBehaviour
{
    [SerializeField] AudioSource hurt, melt, die, punch, walk;
    public void HurtSound()
    {
        hurt.Play();
    }
    public void MeltSoud()
    {
        melt.Play();
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
}
