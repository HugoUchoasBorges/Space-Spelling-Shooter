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

        //GetComponentInChildren<TrailRenderer>().enabled = false;
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
        position = new Vector2(position.x, position.y + bulletSpeed * Time.deltaTime);
        position = Vector2.MoveTowards(transform.position, target.transform.position, step);

        // update the bullet's position
        transform.position = position;

        // if the bullet reaches the target
        if (transform.position == target.transform.position)
        {
            Destroy(gameObject);
        }
    }
}
