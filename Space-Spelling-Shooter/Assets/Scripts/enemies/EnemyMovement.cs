using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class EnemyMovement : Movement {

    Enemy enemy;

    public enum ENUM_INITIALEDGE
    {
        left,
        right,
        top,
        down,
        any
    }

    protected override void Awake()
    {
        // Setting up a start position a direction
        SetStartPositionDirection();
    }

    // Use this for initialization
    protected override void Start () {

        enemy = gameObject.GetComponent<Enemy>();

        base.Start();

        inputImpulse = GlobalVariables.inputImpulse;
        inputRotation = GlobalVariables.inputRotation;

        float additionalImpulse = (Mathf.Min(enemy.text.text.Length, 12)) / 6.67f;

        // Enemy's speed decreases according to the word's length
        inputImpulse -= additionalImpulse;
    }

    public void SetStartPositionDirection()
    {

        ENUM_INITIALEDGE e = GlobalVariables.ENUM_INITIALEDGE;

        Vector3 position = new Vector3
            (
                Random.Range(EdgeGenerator.bottomLeftCorner.x, EdgeGenerator.bottomRightCorner.x),
                Random.Range(EdgeGenerator.bottomRightCorner.y, EdgeGenerator.upperRightCorner.y),
                0
            );

        float degreesRotation = Random.Range(0.0f, 360.0f);

        switch (e)
        {
            case ENUM_INITIALEDGE.left:
                position.x = EdgeGenerator.bottomLeftCorner.x;

                if(position.y < 0)
                    degreesRotation = Random.Range(250f, 340f);
                else
                    degreesRotation = Random.Range(290f, 160f);

                break;

            case ENUM_INITIALEDGE.top:
                position.y = EdgeGenerator.upperRightCorner.y;

                if (position.x < 0)
                    degreesRotation = Random.Range(160f, 250f);
                else
                    degreesRotation = Random.Range(200f, 110f);

                break;

            case ENUM_INITIALEDGE.right:
                position.x = EdgeGenerator.bottomRightCorner.x;

                if (position.y < 0)
                    degreesRotation = Random.Range(110f, 20f);
                else
                    degreesRotation = Random.Range(70f, 160f);

                break;

            case ENUM_INITIALEDGE.down:
                position.y = EdgeGenerator.bottomRightCorner.y;

                if (position.x < 0)
                    degreesRotation = Random.Range(20f, -70f);
                else
                    degreesRotation = Random.Range(-20f, 70f);

                break;

            default:
                if (Random.value < 0.25f)
                    position.x = EdgeGenerator.bottomLeftCorner.x;
                else if (Random.value < 0.5f)
                    position.x = EdgeGenerator.bottomRightCorner.x;
                else if (Random.value < 0.75f)
                    position.y = EdgeGenerator.bottomRightCorner.y;
                else
                    position.y = EdgeGenerator.upperRightCorner.y;
                break;
        }
        
        transform.position = position;
        transform.Rotate(Vector3.forward, degreesRotation);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        rigidBody2D.AddRelativeForce(
            inputImpulse * (Vector2.up * impulseThreshold) * deltaTime
            );
        rigidBody2D.AddTorque(
            inputRotation * (rotationThreshold) * deltaTime
            );
    }
}
