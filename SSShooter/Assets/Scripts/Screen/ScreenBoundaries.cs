using UnityEngine;
using UnityEngine.Assertions;


public class ScreenBoundaries : MonoBehaviour
{

    #region Variables

    // Components
    private SpriteRenderer _spriteRenderer;
    
    private Vector2 _screenBounds;
    public Vector2 screenBoundsOffset;
    private Vector2 _objectDimensions;

    #endregion

    private void Awake()
    {
        _screenBounds = GlobalVariables.ScreenBounds;
        
        if (_screenBounds == Vector2.zero &&  Camera.main != null)
            _screenBounds = GlobalVariables.ScreenBounds = Camera.main.ScreenToWorldPoint(
                new Vector3(Screen.width, Screen.height,Camera.main.transform.position.z));
        
        _spriteRenderer = transform.GetComponent<SpriteRenderer>();
        
        if (_spriteRenderer)
        {
            _objectDimensions = _spriteRenderer.bounds.size;
            screenBoundsOffset.x = 1 * _objectDimensions.x / 2f;
            screenBoundsOffset.y = 1 * _objectDimensions.y / 2f;
        }

        Assert.IsNotNull(_spriteRenderer, "The GameObject " + gameObject.name + "must have a SpriteRenderer for the ScreenBoundaries script to work.");
    }

    private void LimitMovement()
    {
        Vector3 objectPos = transform.position;

        float absPosX = Mathf.Abs(objectPos.x);
        if (absPosX >= _screenBounds.x + screenBoundsOffset.x)
            objectPos.x = objectPos.x * (0.1f*screenBoundsOffset.x / absPosX - 1);

        float absPosY = Mathf.Abs(objectPos.y);
        if (absPosY >= _screenBounds.y + screenBoundsOffset.y)
            objectPos.y = objectPos.y * (0.1f*screenBoundsOffset.y / absPosY - 1);

        transform.position = objectPos;
    }
    
    private void LateUpdate()
    {
        LimitMovement();
    }
}
