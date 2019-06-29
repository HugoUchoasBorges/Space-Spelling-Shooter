using UnityEngine;

public class GlobalVariables : Singleton<GlobalVariables>
{

    #region Variables
    
    // Static Components
    public static GuiController GuiController;
    public static EnemyManager EnemyManager;
    public static Vector2 ScreenBounds;
    
    // Static Layer Names
    public const string SELECTED_ENEMY_LAYER = "SelectedEnemy";
    public const string ENEMY_LAYER = "Enemy";
    public const string BULLET_LAYER = "Bullet";

    #endregion
}
