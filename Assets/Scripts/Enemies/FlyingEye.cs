using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : Enemy
{
    protected override void Attack(Player player)
    {
        player.TakeDamage(attackDamage);
    }
}
