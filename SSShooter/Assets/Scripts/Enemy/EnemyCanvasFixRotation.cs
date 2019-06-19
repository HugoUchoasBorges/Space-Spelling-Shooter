using UnityEngine;

public class EnemyCanvasFixRotation : MonoBehaviour
{
    #region Variables

    private Quaternion _rotation;

    #endregion
    
    private void Awake()
    {
        _rotation = transform.rotation;
    }
    
    private void LateUpdate()
    {
        transform.rotation = _rotation;
    }
}
