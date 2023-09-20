using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlayerHealth_sc : MonoBehaviour
{
    PlayerHealth_sc playerHealth;
    void Awake()
    {
        playerHealth=FindObjectOfType<PlayerHealth_sc>();
    }
}
