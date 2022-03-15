using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter //Abstracted means that can only be inherited and cant be drag and droped somwhere
{
    protected BoxCollider2D boxColider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    protected float ySpeed = 0.75f;
    protected float xSpeed = 1.0f;

    protected virtual void Start()//Awake is called first and, unlike Start, will be called even if the script component is disabled
    {
        boxColider = GetComponent<BoxCollider2D>(); //<> is where you put the type when you use arbitary type
    }
    /*
     *  FixedUpdate should be used when applying forces, torques, or other physics-related functions 
     *  - because you know it will be executed exactly in sync with the physics engine itself.
     *  Whereas Update() can vary out of step with the physics engine, either faster or slower, 
     *  depending on how much of a load the graphics are putting on the rendering engine at any given time, which 
     *  - if used for physics - would give correspondingly variant physical effects!
     */

    protected virtual void UpdateMotor(Vector3 input)
    {
        // Reset moveDelta
        moveDelta = new Vector3(input.x*xSpeed, input.y*ySpeed, 0);

        // Swap sprite direction
        if (moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (moveDelta.x < 0) //we use else if to make sure if we dont press anything it does not flip to a defaul orientation
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }


        //Add push vector if any
        moveDelta += pushDirection;
        //reduce the push force every frame based of recovery speed
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);


        // Check if we can move ia a direction 
        // We need to go to edit->project setting->physics 2d and turn of Queries start in coliders, this way the player doesnt colide with himself
        hit = Physics2D.BoxCast(transform.position, boxColider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            // Make move
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);//this make sure that doenst depend on fps
        }

        hit = Physics2D.BoxCast(transform.position, boxColider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            // Make move
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);//this make sure that doenst depend on fps
        }


    }
}
