using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    public override void Hit(float amountDammage)
    {
        throw new System.NotImplementedException();
    }

    protected override void Attack(Player player)
    {
        player.TakeDamage(attackDamage);
    }

    protected override void Die()
    {
        throw new System.NotImplementedException();
    }
}
