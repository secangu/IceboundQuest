using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer_sc : MonoBehaviour
{
    [SerializeField] Transform _camera;
    private void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }
    void Update()
    {
        transform.LookAt(_camera);
    }
}
