using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LivesSystem))]
public class Player : MonoBehaviour
{
    public Action OnDie;
    public Action OnGetKey;

    [Header("Movement System")]
    [SerializeField] private Transform feet;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundDetectionDistance;
    [SerializeField] private LayerMask jumpable;
    [SerializeField] private float repulseForceWhenVertical;
    [SerializeField] private float repulseForceWhenHorizontal;
    [SerializeField] private float yLimit;

    [Header("Attack System")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private float attackAmount;
    [SerializeField] private LayerMask damageable;

    [Header("Others")]
    [SerializeField] private float trapsDamage;

    private Animator anim;
    private Rigidbody2D rb;
    private LivesSystem liveSystem;
    private float inputH;
    private float currentY;

    private bool hasKey = false;
    private bool nearDoor = false;
    private Door door;

    private bool hasJump = false;
    private bool hasDie = false;

    private Vector2 positionBeforeFall;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        liveSystem = GetComponent<LivesSystem>();
        liveSystem.OnDie += Die;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasDie) return;

        if (transform.position.y <= yLimit)
        {
            DieForFall();
        }

        ThrowAttack();

        hasJump = !InGround();

        Jump();

        if (Input.GetKeyDown(KeyCode.E) && nearDoor && hasKey)
        {
            door.Open();
        }

        Fall();

        Movement();

        if (InGround())
        {
            positionBeforeFall = transform.position;
        }
    }

    private void DieForFall()
    {
        transform.position = positionBeforeFall;
    }

    public void FallInTrap()
    {
        anim.SetTrigger("hit");

        if (Convert.ToInt32(rb.velocity.y) != 0)
        {
            rb.AddForce(Vector3.up * repulseForceWhenVertical, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(Vector3.up * repulseForceWhenHorizontal, ForceMode2D.Impulse);
        }

        liveSystem.TakeDamage(trapsDamage);
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

    // Se ejecuta desde evento de animación
    private void Attack()
    {
        Collider2D[] touchedColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, damageable);

        foreach (Collider2D collider in touchedColliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            enemy.Hit(attackAmount);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !hasJump)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetTrigger("jump");
            hasJump = true;
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

    private void Die()
    {
        hasDie = true;
        rb.velocity = Vector3.zero;
        anim.SetTrigger("die");

        if (rb != null) rb.simulated = false;

        foreach (var script in GetComponents<MonoBehaviour>())
        {
            script.enabled = false;
        }

        OnDie?.Invoke();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(attackPoint.position, attackRadius);
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(0.000001f);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        //rb.AddForce(-transform.right * repulseForce, ForceMode2D.Impulse);
        liveSystem.TakeDamage(damageAmount);
        anim.SetTrigger("hit");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            nearDoor = true;
            door = collision.GetComponent<Door>();
        }
        else if (collision.CompareTag("Key"))
        {
            Destroy(collision.gameObject);
            hasKey = true;
            OnGetKey?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            nearDoor = false;
        }
    }
}
