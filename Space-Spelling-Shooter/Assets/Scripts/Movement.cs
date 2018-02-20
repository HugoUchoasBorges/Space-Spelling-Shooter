using UnityEngine;

public class Movement : MonoBehaviour {

    // OffScreen zone where objects can move
    protected float deadZone;

    protected Rigidbody2D rigidBody2D;

    // Player control variables
    public float impulseThreshold;
    public float rotationThreshold;
    protected float inputImpulse;
    protected float inputRotation;

    protected float deltaTime;

    protected virtual void Awake()
    {

    }

    // Use this for initialization
    protected virtual void Start () {

        deltaTime = Time.deltaTime;

        rigidBody2D = GetComponent<Rigidbody2D>();

        rigidBody2D.angularDrag = GlobalVariables.angularDrag;
        rigidBody2D.drag = GlobalVariables.linearDrag;
        impulseThreshold = GlobalVariables.impulseThreshold;
        rotationThreshold = GlobalVariables.rotationThreshold;

        deadZone = GetComponent<CircleCollider2D>().radius;
    }

    // Update is called once per frame
    protected virtual void Update () {

        checkScreenEdges();
    }

    private void checkScreenEdges()
    {
        Vector2 newPosition = transform.position;

        if (Mathf.Abs(transform.position.x) > EdgeGenerator.bottomRightCorner.x + deadZone)
            if (transform.position.x > 0)
                newPosition.x = EdgeGenerator.bottomLeftCorner.x - deadZone / 2;
            else
                newPosition.x = EdgeGenerator.bottomRightCorner.x + deadZone / 2;

        if (Mathf.Abs(transform.position.y) > EdgeGenerator.upperRightCorner.y + deadZone)
            if (transform.position.y > 0)
                newPosition.y = EdgeGenerator.bottomRightCorner.y - deadZone / 2;
            else
                newPosition.y = EdgeGenerator.upperRightCorner.y + deadZone / 2;

        transform.position = newPosition;

    }

    protected virtual void FixedUpdate()
    {

    }
}
