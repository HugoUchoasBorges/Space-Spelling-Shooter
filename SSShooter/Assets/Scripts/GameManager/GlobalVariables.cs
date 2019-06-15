using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    #region Singleton

    private static GlobalVariables _instance;
    
    private static object _lock = new object();

    public static GlobalVariables Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType(typeof(GlobalVariables)) as GlobalVariables;
                }

                if (_instance == null)
                {
                    GameObject singleton = new GameObject();
                    _instance = singleton.AddComponent<GlobalVariables>();

                    singleton.name = "(singleton) GlobalVariables";
                    
                    DontDestroyOnLoad(singleton);
                }

                return _instance;
            }
        }
    }

    #endregion

    #region Variables

    public static GuiController GuiController;

    #endregion
}
