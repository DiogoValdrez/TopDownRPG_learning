using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : Collidable
{
    public int damage = 1;
    public float pushForce = 5;

    protected override void OnColide(Collider2D coll)
    {
        if(coll.name == "Player")
        {
            //create new damage object before sending it to the player
            Damage dmg = new Damage
            {
                damageAmount = damage,
                origin = transform.position,
                pushForce = pushForce
            };
            coll.SendMessage("ReceiveDamage", dmg);
        }
    }
}
