using System;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerMovement : MonoBehaviour
{
    #region Variables

    // Components
    private Rigidbody2D _rigidbody2D;
    private Camera _camera;

    [Space] [Header("Attributes________________")]
    [Range(1f, 20f)] public float speed = 5f;
    
    [Space] [Header("Arcade Style Control________________")]
    // Arcade Style Control Variables
    [Range(1f, 20f)] public float rotationSlerp = 7f;

    [Space] [Header("Thrust Style Control________________")]
    // Thrust Style Control Variables
    [Range(0.5f, 3f)] public float thrust = 2.5f;
    [Range(0.2f, 1f)] public float inverseThrust = 1f;
    [Range(0.1f, 0.5f)] public float turnThrust = 0.2f;

    #endregion

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
        Assert.IsNotNull(_rigidbody2D);
    }

    private void Start()
    {
        _camera = Camera.main;
        SetStartPosition();
    }

    public void SetStartPosition()
    {
        Vector3 centerPosition = _camera.ScreenToWorldPoint( 
            new Vector3(Screen.width/2f, Screen.height/2f, _camera.nearClipPlane) );

        transform.SetPositionAndRotation(centerPosition, Quaternion.identity);
        
        if (_rigidbody2D)
        {
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.angularVelocity = 0f;
        }
    }
    

    #region Move Methods

    public void Move(Vector2 inputAxis)
    {
        if(!_rigidbody2D)
            MoveArcadeStyle(inputAxis);
        
        MoveThrustStyle(inputAxis);
    }
    
    private void MoveThrustStyle(Vector2 inputAxis)
    {
        Vector2 relativeForce = inputAxis.y * Vector2.up;
        if (inputAxis.y > 0)
            relativeForce *= thrust;
        else 
            relativeForce *= inverseThrust;
        
        _rigidbody2D.AddRelativeForce(relativeForce);

        float torque = -(turnThrust * inputAxis.x);
        _rigidbody2D.AddTorque(torque);
    }

    private void MoveArcadeStyle(Vector2 inputAxis)
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

    #endregion
    
}
