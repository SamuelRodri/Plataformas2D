using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    public override void Hit(float damageAmount)
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
