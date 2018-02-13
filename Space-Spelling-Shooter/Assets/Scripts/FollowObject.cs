using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

    protected GameObject trackObject;
    protected Dictionary<string,Vector3> offset;

    private void Start()
    {
        trackObject = gameObject.transform.parent.transform.parent.gameObject;
        float raio = trackObject.GetComponent<CircleCollider2D>().radius;

        offset = new Dictionary<string, Vector3>()
        {
            {"left", Vector3.left * raio},
            {"right", Vector3.right * raio},
            {"up", Vector3.up * raio},
            {"down", Vector3.down * raio},

        };
    }

    // Update is called once per frame
    void Update () {
        gameObject.transform.position = Camera.main.WorldToScreenPoint(trackObject.GetComponent<CircleCollider2D>().bounds.center + offset["down"]/1.5f);
    }
}
