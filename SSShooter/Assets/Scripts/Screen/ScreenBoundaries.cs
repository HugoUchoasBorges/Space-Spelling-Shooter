using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ScreenBoundaries : MonoBehaviour
{

    #region Variables

    // Components
    private SpriteRenderer spriteRenderer;

    private Vector2 screenBounds;
    private Vector2 objectDimensions;

    #endregion

    private void Awake()
    {
        spriteRenderer = transform.GetComponent<SpriteRenderer>();

        Assert.IsNotNull(spriteRenderer, "The GameObject " + gameObject.name + "must have a SpriteRenderer for the ScreenBoundaries script to work.");
    }

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        if (spriteRenderer)
        {
            objectDimensions = spriteRenderer.bounds.size;
            screenBounds.x += 0.3f * objectDimensions.x;
            screenBounds.y += 0.3f * objectDimensions.y;
        }
    }

    private void limitMovement()
    {
        Vector3 objectPos = transform.position;

        if (Mathf.Abs(objectPos.x) > screenBounds.x)
            objectPos.x = -objectPos.x;

        if (Mathf.Abs(objectPos.y) > screenBounds.y)
            objectPos.y = -objectPos.y;

        transform.position = objectPos;
    }

    void LateUpdate()
    {
        limitMovement();
    }
}
