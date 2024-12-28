using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : Enemy
{
    public override void Hit(float damageAmount, Vector2 direction)
    {
        TakeDamage(damageAmount);
        animator.SetTrigger("hit");
    }

    protected override void Attack(Player player)
    {
        player.TakeDamage(attackDamage);
    }

    protected override void Die()
    {
        animator.SetTrigger("die");
    }
}
