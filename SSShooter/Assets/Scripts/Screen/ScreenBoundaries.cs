using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBoundaries : MonoBehaviour
{

    #region Variables

    private Vector3 screenBounds;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    private void limitMovement()
    {
        Vector3 objectPos = transform.position;
        objectPos.x = Mathf.Clamp(objectPos.x, -screenBounds.x, screenBounds.x);
        objectPos.y = Mathf.Clamp(objectPos.y, -screenBounds.y, screenBounds.y);

        transform.position = objectPos;
    }

    void LateUpdate()
    {
        limitMovement();
    }
}
