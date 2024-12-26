using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private float patrolSpeed;
    [SerializeField] private float attackDamage;

    private Vector3 actualDestination;
    private int actualIndex = 0;

    private void Start()
    {
        actualDestination = wayPoints[actualIndex].position;
        LookAtDestination();
        StartCoroutine(Patrol());
    }
    IEnumerator Patrol()
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

    private void SetNewDestination()
    {
        actualIndex = ++actualIndex % wayPoints.Length;
        actualDestination = wayPoints[actualIndex].position;
        LookAtDestination();
    }

    private void LookAtDestination()
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DetectionPlayer"))
        {
            Debug.Log("Jugador detectado!");
        }
        else if (collision.gameObject.CompareTag("PlayerHitBox"))
        {
            LivesSystem livesSystem = collision.GetComponent<LivesSystem>();
            livesSystem.TakeDamage(attackDamage);
        }
    }
}
