using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int pesosAmount = 5;
    public int pesos;

    protected override void OnCollect()
    {
        //this calls the original function
        //base.OnCollect();
        //Debug.Log("Grant Pesos");

        if(!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.ShowText("+" + pesosAmount +  " pesos!", 25, Color.yellow, transform.position, Vector3.up * 50, 1.5f);
        }
    }
}
