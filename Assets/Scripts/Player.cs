using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This line creates a box colider 2d if he didnt existed already
// [RequireComponent(typeof(BoxCollider2D)]

public class Player : Mover
{
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal"); //if you go to edit->project settings->input manager, you can see what keys trigger this axis raw
        float y = Input.GetAxisRaw("Vertical");

        UpdateMotor(new Vector3(x, y, 0));
    }
}