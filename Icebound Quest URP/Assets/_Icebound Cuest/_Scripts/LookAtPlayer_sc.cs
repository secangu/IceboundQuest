using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer_sc : MonoBehaviour
{
    [SerializeField] Transform _camera;
    void Update()
    {
        transform.LookAt(_camera);
    }
}
