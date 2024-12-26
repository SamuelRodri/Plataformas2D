using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesSystem : MonoBehaviour
{
    [SerializeField] private float lives;

    public void TakeDamage(float damageAmount)
    {
        lives -= damageAmount;

        if (lives <= 0)
        {
            Destroy(gameObject);
        }
    }
}
