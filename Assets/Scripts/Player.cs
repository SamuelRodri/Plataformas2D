using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement System")]
    [SerializeField] private Transform feet;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundDetectionDistance;
    [SerializeField] private LayerMask jumpable;

    [Header("Attack System")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private float attackAmount;
    [SerializeField] private LayerMask damageable;

    private Animator anim;
    private Rigidbody2D rb;
    private float inputH;
    private float currentY;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Fall();

        Movement();

        Jump();

        ThrowAttack();
    }

    private void Fall()
    {
        bool inGround = InGround();

        if (transform.position.y >= currentY && !inGround)
        {
            anim.SetBool("falling", true);
        }
        else if (inGround)
        {
            anim.SetBool("falling", false);
        }

        currentY = transform.position.y;
    }

    private void ThrowAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("attack");
        }
    }

    // Se ejecuta desde evento de animaci�n
    private void Attack()
    {
        Collider2D[] touchedColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, damageable);

        foreach (Collider2D collider in touchedColliders)
        {
            collider.GetComponent<LivesSystem>().TakeDamage(attackAmount);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && InGround())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetTrigger("jump");
        }
    }

    private bool InGround()
    {
        return Physics2D.Raycast(feet.position, Vector3.down, groundDetectionDistance, jumpable);
    }

    private void Movement()
    {
        inputH = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(inputH * movementSpeed, rb.velocity.y);

        if (inputH != 0)
        {
            anim.SetBool("running", true);
            if (inputH > 0)
            {
                transform.eulerAngles = Vector3.zero;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
        else
        {
            anim.SetBool("running", false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(attackPoint.position, attackRadius);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    anim.SetBool("falling", false);
    //}
}