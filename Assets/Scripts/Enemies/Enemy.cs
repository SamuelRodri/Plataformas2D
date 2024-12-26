using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected Transform[] wayPoints;
    [SerializeField] protected float patrolSpeed;
    [SerializeField] protected float attackDamage;

    protected Vector3 actualDestination;
    protected int actualIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        actualDestination = wayPoints[actualIndex].position;
        LookAtDestination();
        StartCoroutine(Patrol());
    }

    protected IEnumerator Patrol()
    {
        while (true)
        {
            while (transform.position != actualDestination)
            {
                transform.position = Vector3.MoveTowards(transform.position, actualDestination, patrolSpeed * Time.deltaTime);
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
}
