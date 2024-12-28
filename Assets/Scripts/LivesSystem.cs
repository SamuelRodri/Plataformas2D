using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesSystem : MonoBehaviour
{
    public Action OnDie;

    [SerializeField] private float lives;

    public float Lives { get => lives; set => lives = value; }

    public void TakeDamage(float damageAmount)
    {
        lives -= damageAmount;

        if (lives <= 0)
        {
            OnDie?.Invoke();
        }
    }
}
