using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

    protected GameObject trackObject;
    protected Dictionary<string,Vector3> offset;


    private void Start()
    {
        trackObject = gameObject.transform.parent.transform.parent.gameObject;
        float diametro = 2*trackObject.GetComponent<CircleCollider2D>().radius;

        offset = new Dictionary<string, Vector3>()
        {
            {"left", Camera.main.ScreenToWorldPoint(Vector3.left * diametro)},
            {"right", Camera.main.ScreenToWorldPoint(Vector3.right * diametro)},
            {"up", Camera.main.ScreenToWorldPoint(Vector3.up * diametro)},
            {"down", Camera.main.ScreenToWorldPoint(Vector3.down * diametro)},

        };
    }

    // Update is called once per frame
    void Update () {
        gameObject.transform.position = Camera.main.WorldToScreenPoint(trackObject.transform.position) + offset["down"];
    }
}
