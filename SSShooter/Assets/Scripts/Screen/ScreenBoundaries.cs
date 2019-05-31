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
            ScreenBoundsOffset.x = 0.3f * _objectDimensions.x;
            ScreenBoundsOffset.y = 0.3f * _objectDimensions.y;
        }
    }

    private void LimitMovement()
    {
        Vector3 objectPos = transform.position;

        if (Mathf.Abs(objectPos.x) > ScreenBounds.x + ScreenBoundsOffset.x)
            objectPos.x = -objectPos.x;

        if (Mathf.Abs(objectPos.y) > ScreenBounds.y + ScreenBoundsOffset.y)
            objectPos.y = -objectPos.y;

        transform.position = objectPos;
    }

    private void LateUpdate()
    {
        LimitMovement();
    }
}
