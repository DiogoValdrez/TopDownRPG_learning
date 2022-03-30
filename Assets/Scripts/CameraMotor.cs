using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    private Transform lookAt;
    public float boundX = 0.15f;
    public float boundY = 0.05f;

    private void Start()
    {
        lookAt = GameObject.Find("Player").transform;// to fix lookat error
    }
    // Is called after Update and FixUpdate
    private void LateUpdate() 
    {
        Vector3 delta = Vector3.zero;
        float deltaX = lookAt.position.x - transform.position.x;//distance between the "player" and the camera
        float deltaY = lookAt.position.y - transform.position.y;

        // check if we are un the bounds on the x axis
        if (deltaX > boundX || deltaX < -boundX)
        {
            // check if the camera needs to go right or left
            if(transform.position.x < lookAt.position.x) 
            {
                delta.x = deltaX - boundX;
            }
            else
            {
                delta.x = deltaX + boundX;
            }
        }

        // check if we are un the bounds on the y axis
        if (deltaY > boundY || deltaY < -boundY)
        {
            if (transform.position.y < lookAt.position.y)
            {
                delta.y = deltaY - boundY;
            }
            else
            {
                delta.y = deltaY + boundY;
            }
        }

        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
