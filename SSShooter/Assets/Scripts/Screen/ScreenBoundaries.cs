using UnityEngine;
using UnityEngine.Assertions;


public class ScreenBoundaries : MonoBehaviour
{

    #region Variables

    // Components
    private SpriteRenderer _spriteRenderer;
    
    public static Vector2 ScreenBounds;
    public static Vector2 ScreenBoundsOffset;
    private Vector2 _objectDimensions;

    #endregion

    private void Awake()
    {
        _spriteRenderer = transform.GetComponent<SpriteRenderer>();

        Assert.IsNotNull(_spriteRenderer, "The GameObject " + gameObject.name + "must have a SpriteRenderer for the ScreenBoundaries script to work.");
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (Camera.main != null)
            ScreenBounds = Camera.main.ScreenToWorldPoint(
                new Vector3(Screen.width, Screen.height,Camera.main.transform.position.z));

        if (_spriteRenderer)
        {
            _objectDimensions = _spriteRenderer.bounds.size;
            ScreenBoundsOffset.x = 1 * _objectDimensions.x / 2f;
            ScreenBoundsOffset.y = 1 * _objectDimensions.y / 2f;
        }
    }

    private void LimitMovement()
    {
        Vector3 objectPos = transform.position;

        float absPosX = Mathf.Abs(objectPos.x);
        if (absPosX >= ScreenBounds.x + ScreenBoundsOffset.x)
            objectPos.x = objectPos.x * (0.1f*ScreenBoundsOffset.x / absPosX - 1);

        float absPosY = Mathf.Abs(objectPos.y);
        if (absPosY >= ScreenBounds.y + ScreenBoundsOffset.y)
            objectPos.y = objectPos.y * (0.1f*ScreenBoundsOffset.y / absPosY - 1);

        transform.position = objectPos;
    }
    
    private void LateUpdate()
    {
        LimitMovement();
    }
}
