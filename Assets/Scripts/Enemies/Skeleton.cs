using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    public override void Hit(float damageAmount, Vector2 direction)
    {
        rb.isKinematic = false;
        rb.AddForce(direction * repulseForce, ForceMode2D.Impulse);
        TakeDamage(damageAmount);
        rb.isKinematic = true;

        if (livesSystem.Lives > 0)
        {
            animator.SetTrigger("hit");
        }
        else
        {
            Die();
        }
    }

    protected override void Attack(Player player)
    {
        animator.SetTrigger("attack");
        player.TakeDamage(attackDamage);
    }

    protected override void Die()
    {
        animator.SetTrigger("die");
        actualPatrolSpeed = 0;
    }
}
