using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement {

    private Player player;
    public float speed;

    // Use this for initialization
    protected override void Start () {

        base.Start();
        player = gameObject.GetComponent<Player>();

        speed = GlobalVariables.defaultPlayerSpeed;
        // Centraliza o player no cenário
        Centralize();

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        ControllPlayer();
    }

    private void ControllPlayer()
    {
        // Captures user input
        float axisY = Input.GetAxis("Vertical");
        float axisX = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(axisX, axisY);

        // Normalizes diagonal speed
        if (movement.magnitude > 1)
            movement = movement.normalized;
        
        // Applies speed to the movement
        movement *= speed;

        // Move the player
        transform.Translate(movement * Time.deltaTime, Space.World);

        // Rotates the player
        if (movement.magnitude != 0)
        {
            Quaternion angle = Quaternion.Euler(0, 0, Vector3.Angle(Vector3.up, movement));
            if (movement.x > 0)
            {
                angle.z = -angle.z;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, angle, 7f * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Tells the game the player is deactivated
        GlobalVariables.playerIsActive = false;

        // Remove the locked target
        TypingSystem.RemoveTarget();

        // Destroys the collided enemy
        TypingSystem.destroiInimigo(collision.gameObject);

        Player.Lifes -= 1;

        if (Player.Lifes > 0)
            DeathSequence();
        else
            GameManager.GameOVer();
    }

    private void DeathSequence()
    {
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        GameManager.PauseGame();

        player.PlayAudio(GlobalVariables.ENUM_AUDIO.player_dying);

        // Turns player Invisible 
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        // Turns player intangible
        gameObject.layer = GlobalVariables.LAYER_INIMIGOS;

        // Wait for respawn
        yield return new WaitForSeconds(GlobalVariables.respawnTime);

        // Centralizes player
        Centralize();

        // Turns player visible
        gameObject.GetComponent<SpriteRenderer>().enabled = true;

        // Changes player color
        Color oldColor = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = GlobalVariables.invulnerableColor;

        // Wait
        yield return new WaitForSeconds(GlobalVariables.invulnerableTime);

        // Turns player tangible
        GetComponent<CircleCollider2D>().enabled = true;
        gameObject.layer = 0;

        // Changes player color back
        GetComponent<SpriteRenderer>().color = oldColor;

        // Tells the game the player is actived
        GlobalVariables.playerIsActive = true;

        GameManager.ResumeGame();
    }

    protected void Centralize()
    {
        // Statically centers the player
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        rigidBody2D.angularVelocity = 0;
        rigidBody2D.velocity = Vector2.zero;
    }
}
