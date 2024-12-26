using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Action OnDoorOpen;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Open()
    {
        animator.SetTrigger("open");
    }

    public void OpenAnimationEnded()
    {
        OnDoorOpen?.Invoke();
    }
}
