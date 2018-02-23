using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class EnemyMovement : Movement {

    Enemy enemy;

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

    protected void SetStartPositionDirection()
    {
        transform.Rotate(Vector3.forward, Random.Range(0.0f, 360.0f));
        Vector3 position = new Vector3
            (
                Random.Range(EdgeGenerator.bottomLeftCorner.x, EdgeGenerator.bottomRightCorner.x),
                Random.Range(EdgeGenerator.bottomRightCorner.y, EdgeGenerator.upperRightCorner.y),
                0
            );

        if (Random.value < 0.25f)
            position.x = EdgeGenerator.bottomLeftCorner.x;
        else if (Random.value < 0.5f)
            position.x = EdgeGenerator.bottomRightCorner.x;
        else if (Random.value < 0.75f)
            position.y = EdgeGenerator.bottomRightCorner.y;
        else
            position.y = EdgeGenerator.upperRightCorner.y;

        transform.position = position;
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
