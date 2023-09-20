using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSounds_sc : MonoBehaviour
{
    [SerializeField] AudioSource invocation, stomp, damage, tornado;

    public void DamageSound()
    {
        damage.Play();
    }
    public void StompSound()
    {
        stomp.Play();
    }
    public void InvocationSound()
    {
        invocation.Play();
    }
    public void TornadoSound()
    {
        tornado.Play();
    }
}
