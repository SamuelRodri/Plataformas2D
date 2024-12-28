using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LivesSystem), typeof(Animator))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected Transform[] wayPoints;
    [SerializeField] protected float patrolSpeed;
    [SerializeField] protected float attackDamage;
    [SerializeField] protected float repulseForce;

    protected Vector3 actualDestination;
    protected int actualIndex = 0;
    protected float actualPatrolSpeed;

    protected LivesSystem livesSystem;
    protected Animator animator;
    protected Rigidbody2D rb;

    private void Awake()
    {
        livesSystem = GetComponent<LivesSystem>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        actualDestination = wayPoints[actualIndex].position;
        actualPatrolSpeed = patrolSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        LookAtDestination();
        StartCoroutine(Patrol());
    }

    protected IEnumerator Patrol()
    {
        while (true)
        {
            while (transform.position != actualDestination)
            {
                transform.position = Vector3.MoveTowards(transform.position, actualDestination, actualPatrolSpeed * Time.deltaTime);
                yield return null;
            }

            SetNewDestination();
        }
    }

    protected void LookAtDestination()
    {
        if (actualDestination.x > transform.position.x)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    protected void TakeDamage(float damageAmount)
        => livesSystem.TakeDamage(damageAmount);

    private void SetNewDestination()
    {
        actualIndex = ++actualIndex % wayPoints.Length;
        actualDestination = wayPoints[actualIndex].position;
        LookAtDestination();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DetectionPlayer"))
        {
            Debug.Log("Jugador detectado!");
        }
        else if (collision.gameObject.CompareTag("PlayerHitBox"))
        {
            Attack(collision.GetComponent<Player>());
        }
    }

    protected abstract void Attack(Player player);
    protected abstract void Die();
    public abstract void Hit(float amountDammage, Vector2 direction);

    // Call by animation event
    protected void EndDeadAnimation()
        => Destroy(gameObject);
}
