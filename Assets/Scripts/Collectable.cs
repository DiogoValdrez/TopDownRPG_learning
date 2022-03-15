using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Collidable
{
    // Logic
    protected bool collected; //protected are acessible for children

    protected override void OnColide(Collider2D coll)
    {
        if(coll.name == "Player")
        {
            OnCollect();
        }
    }

    protected virtual void OnCollect()
    {
        collected = true;
    }
}
