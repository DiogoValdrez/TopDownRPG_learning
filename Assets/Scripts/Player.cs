using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This line creates a box colider 2d if he didnt existed already
// [RequireComponent(typeof(BoxCollider2D)]

public class Player : Mover
{
    protected override void Start()
    {
        base.Start();
        DontDestroyOnLoad(gameObject);// this also saves its children
    }
    protected override void ReceiveDamage(Damage dmg)
    {
        base.ReceiveDamage(dmg);
        GameManager.instance.OnHitPointChange();
    }
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal"); //if you go to edit->project settings->input manager, you can see what keys trigger this axis raw
        float y = Input.GetAxisRaw("Vertical");

        UpdateMotor(new Vector3(x, y, 0));
    }
    public void SwapSprite(int skinid)
    {
        GetComponent<SpriteRenderer>().sprite = GameManager.instance.playerSprites[skinid]; // we could get a reference to sprite renderer in protected start, it would make this more eficient
    }
    public void OnLevelUp()
    {
        maxHitpoint++;
        hitpoint = maxHitpoint;
    }
    public void SetLevel(int level)
    {
        for(int i = 1; i < level; i++)
        {
            OnLevelUp();
        }
    }
    public void Heal(int healingAmount)
    {
        if (hitpoint == maxHitpoint)
        {
            return;
        }
        hitpoint += healingAmount;
        if (hitpoint > maxHitpoint)
        {
            hitpoint = maxHitpoint;
        }
                  
        GameManager.instance.ShowText("x" + healingAmount.ToString() + "hp", 25, Color.green, transform.position, Vector3.up * 30, 1.0f);
        GameManager.instance.OnHitPointChange();
    }

}