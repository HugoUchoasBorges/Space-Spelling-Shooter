using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
{
    private static T _instance;

    // ReSharper disable once UnusedMember.Global
    public static T Instance
    {
        get
        {
            if (isQuittingGame)
                return null;
            
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(T)) as T;
                if (!_instance)
                {
                    GameObject singleton = new GameObject();
                    _instance = singleton.AddComponent<T>();
                    singleton.name = "(Singleton)" + typeof(T);
                    
                    DontDestroyOnLoad(singleton);
                }
            }

            return _instance;
        }
    }

    // ReSharper disable once StaticMemberInGenericType
    private static bool isQuittingGame;

    private void OnDestroy()
    {
        isQuittingGame = true;
    }
}
