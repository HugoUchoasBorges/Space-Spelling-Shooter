using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public static float bulletSpeed = 8f;

    // Use this for initialization
    void Start () {
        bulletSpeed = 8f;
    }
	
	// Update is called once per frame
	void Update () {
        
        // Get the bullet's current position
        Vector2 position = transform.position;

        // compute the bullet's new position
        position = new Vector2(position.x, position.y + bulletSpeed * Time.deltaTime);

        // update the bullet's position
        transform.position = position;

        // this is the top-right point of the screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        // if the bullet went outside the screen on the top, then destroy it
        if (transform.position.y > max.y)
        {
            Destroy(gameObject);
        }
    }
}
