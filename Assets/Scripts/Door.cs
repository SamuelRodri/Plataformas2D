using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Action OnDoorOpen;

    private Animator animator;

    [SerializeField] private GameObject openText;
    [SerializeField] private GameObject keyText;
    [SerializeField] private AudioClip openSound;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Open(bool hasKey)
    {
        if (!hasKey)
        {
            keyText.SetActive(true);
            return;
        }

        openText.SetActive(false);
        keyText.SetActive(false);
        animator.SetTrigger("open");
        GetComponent<AudioSource>().PlayOneShot(openSound);
    }

    public void OpenAnimationEnded()
    {
        OnDoorOpen?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHitBox"))
        {
            openText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHitBox"))
        {
            openText.SetActive(false);
            keyText.SetActive(false);
        }
    }
}
