using UnityEngine;

public class GlobalVariables : Singleton<GlobalVariables>
{

    #region Variables
    
    // Static Components
    public static GuiController GuiController;
    public static EnemyManager EnemyManager;
    public static Vector2 ScreenBounds;

    #endregion
}
