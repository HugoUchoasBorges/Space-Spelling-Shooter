using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public static float bulletSpeed;
    public Enemy target;

    public Player player;

    public bool hit;

    void Awake()
    {
        player = GameManager.player;
        player.bulletController = this;

        Physics2D.IgnoreCollision(GetComponentInChildren<Collider2D>(), player.GetComponent<Collider2D>());
    }

    // Use this for initialization
    void Start () {

        bulletSpeed = 15f;
        hit = false;

    }

    void OnDestroy()
    {
        hit = true;
        target.PlayAudio(GlobalVariables.ENUM_AUDIO.enemy_hit);
    }

    // Update is called once per frame
    void Update () {

        // Get the bullet's current position
        Vector2 position = transform.position;

        float step = bulletSpeed * Time.deltaTime;

        // compute the bullet's new position
        Rigidbody2D rigidBody2D = target.gameObject.GetComponent<Rigidbody2D>();
        position = new Vector2(position.x, position.y + bulletSpeed * Time.deltaTime);
        position = Vector2.MoveTowards(transform.position, rigidBody2D.worldCenterOfMass, step);

        // update the bullet's position
        transform.position = position;

        // if the bullet reaches the target
        if (new Vector2(transform.position.x, transform.position.y) == rigidBody2D.worldCenterOfMass)
        {
            Destroy(gameObject);
        }
    }
}
