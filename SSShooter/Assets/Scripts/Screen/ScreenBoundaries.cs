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
    private Rigidbody2D _rigidbody2D;

    #endregion

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
            
        _screenBounds = GlobalVariables.ScreenBounds;
        Camera mainCamera = Camera.main;
        
        if (_screenBounds == Vector2.zero &&  mainCamera != null)
        {
            _screenBounds = GlobalVariables.ScreenBounds =
                mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
        }
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
        float absPosY = Mathf.Abs(objectPos.y);

        if (_rigidbody2D)
        {
            Vector2 velocity = _rigidbody2D.velocity;
            
            if (velocity.x > 0 && objectPos.x >= _screenBounds.x + screenBoundsOffset.x)
                objectPos.x *= 0.1f * screenBoundsOffset.x / absPosX - 1;
            else if (velocity.x < 0 && objectPos.x <= -(_screenBounds.x + screenBoundsOffset.x))
                objectPos.x *= 0.1f * screenBoundsOffset.x / absPosX - 1;
            
            if (velocity.y > 0 && objectPos.y >= _screenBounds.y + screenBoundsOffset.y)
                objectPos.y *= 0.1f * screenBoundsOffset.y / absPosY - 1;
            else if (velocity.y < 0 && objectPos.y <= -(_screenBounds.y + screenBoundsOffset.y))
                objectPos.y *= 0.1f * screenBoundsOffset.y / absPosY - 1;
        }
        else
        {
            if (absPosX >= _screenBounds.x + screenBoundsOffset.x)
                objectPos.x *= 0.1f * screenBoundsOffset.x / absPosX - 1;

            if (absPosY >= _screenBounds.y + screenBoundsOffset.y)
                objectPos.y *= 0.1f * screenBoundsOffset.y / absPosY - 1;
        }

        transform.position = objectPos;
    }
    
    private void LateUpdate()
    {
        LimitMovement();
    }
}
