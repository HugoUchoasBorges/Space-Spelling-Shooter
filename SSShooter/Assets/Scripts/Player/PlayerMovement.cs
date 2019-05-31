using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables

    [Range(1f, 20f)] public float speed = 5f;
    [Range(1f, 20f)] public float rotationSlerp = 7f;

    #endregion

    public void Move(Vector2 inputAxis)
    {
        Vector2 movement = inputAxis;

        // Normalizes the Movement Vector 

        movement = movement.magnitude > 1 ? movement.normalized : movement;

        // Applies speed to the movement
        movement *= speed;

        // Move the player
        transform.Translate(movement * Time.deltaTime, Space.World);

        // Rotates the player
        if (Math.Abs(movement.magnitude) > 0.05f)
        {
            Quaternion angle = Quaternion.Euler(0, 0, Vector3.Angle(Vector3.up, movement));
            if (movement.x > 0)
            {
                angle.z = -angle.z;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, angle, rotationSlerp * Time.deltaTime);
        }
    }
}
