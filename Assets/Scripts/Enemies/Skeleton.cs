using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    public override void Hit(float damageAmount, Vector2 direction)
    {
        hitted = true;
        rb.isKinematic = false;
        rb.AddForce(direction * repulseForce, ForceMode2D.Impulse);
        TakeDamage(damageAmount);
        animator.SetTrigger("hit");
        rb.isKinematic = true;
        hitted = false;
    }

    protected override void Attack(Player player)
    {
        animator.SetTrigger("attack");
        player.TakeDamage(attackDamage);
    }

    protected override void Die()
    {
        actualPatrolSpeed = 0;
        animator.SetTrigger("die");
    }
}
