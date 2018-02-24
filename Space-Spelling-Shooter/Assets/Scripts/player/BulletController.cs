using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public static float bulletSpeed = 8f;
    public static GameObject target;

    // Use this for initialization
    void Start () {
        bulletSpeed = 20f;
        target = TypingSystem.target;
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

        // this is the top-right point of the screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        // if the bullet went outside the screen on the top, then destroy it
        if (transform.position == target.transform.position)
        {
            Destroy(gameObject);
        }
    }
}
