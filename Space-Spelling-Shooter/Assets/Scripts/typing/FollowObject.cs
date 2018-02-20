using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowObject : MonoBehaviour {

    protected GameObject trackObject;
    protected Dictionary<string,Vector3> offset;

    private float radius;

    private void Awake()
    {
        trackObject = gameObject.transform.parent.transform.parent.gameObject;
        radius = trackObject.GetComponent<CircleCollider2D>().radius;
        gameObject.transform.position = Camera.main.WorldToScreenPoint(trackObject.GetComponent<CircleCollider2D>().bounds.center);
    }

    private void Start()
    {

        offset = new Dictionary<string, Vector3>()
        {
            {"left", Vector3.left * 2*radius},
            {"right", Vector3.right * 2*radius},
            {"up", Vector3.up * radius},
            {"down", Vector3.down * radius},
            {"default", Vector3.down * radius}

        };

        trackObject.GetComponentInChildren<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
    }

    // Update is called once per frame
    void Update () {
        gameObject.transform.position = Camera.main.WorldToScreenPoint(trackObject.GetComponent<CircleCollider2D>().bounds.center + offset["default"]/1.5f);

        CheckVisibility();
    }

    // Keeps text on Screen
    private void CheckVisibility()
    {
        Vector2 position = trackObject.transform.position;
        
        if (Mathf.Abs(position.x) >= EdgeGenerator.bottomRightCorner.x - radius / 2)
            if (position.x >= 0)
            {
                offset["default"] = offset["left"];
                GetComponent<RectTransform>().rotation = Quaternion.identity;
                GetComponent<RectTransform>().Rotate(Vector3.forward, 90);
            }
            else
            {
                offset["default"] = offset["right"];
                GetComponent<RectTransform>().rotation = Quaternion.identity;
                GetComponent<RectTransform>().Rotate(Vector3.forward, -90);
            }

        else if (Mathf.Abs(position.y) >= EdgeGenerator.upperRightCorner.y - radius / 2)
            if (position.y >= 0)
            {
                offset["default"] = offset["down"];
                GetComponent<RectTransform>().rotation = Quaternion.identity;
            }
            else
            {
                offset["default"] = offset["up"];
                GetComponent<RectTransform>().rotation = Quaternion.identity;
            }

        else
        {
            offset["default"] = offset["down"];
            GetComponent<RectTransform>().rotation = Quaternion.identity;
        }
    }
}
