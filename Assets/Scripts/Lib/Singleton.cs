using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Inherit from this base class to create a singleton.
/// Example: public class MyClassName : Singleton<MyClassName> {}
/// This script will not prevent non singleton constructors from being used in your derived classes. To prevent this, add a protected constructor to each derived class.
/// When Unity quits it destroys objects in a random order and this can create issues for singletons. So we prevent access to the singleton instance when the application quits to prevent problems.
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // Check to see if we're about to be destroyed.
    private static bool ShuttingDown = false;
    private static object Lock = new object();
    private static T _Instance;

    public static T Instance
    {
        get
        {
            if (ShuttingDown)
            {
                // Debug.LogWarning("[Singleton] Instance '" + typeof(T) + "' already destroyed. Returning null.");
                return null;
            }

            lock (Lock)
            {
                if (_Instance == null)
                {
                    // Search for existing instance (note that this will IGNORE inactive GameObjects).
                    _Instance = (T)FindObjectOfType(typeof(T));

                    // Create new instance if one doesn't already exist.
                    if (_Instance == null)
                    {
                        //Debug.Log("Creating new instance of type: " + typeof(T));
                        // Need to create a new GameObject to attach the singleton to.
                        var singletonObject = new GameObject();
                        _Instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString() + "_Singleton";

                        // Make instance persistent.
                        DontDestroyOnLoad(singletonObject);
                    }
                }

                return _Instance;
            }
        }
    }

    protected virtual void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnApplicationQuit()
    {
        ShuttingDown = true;
    }

    private void OnDestroy()
    {
        ShuttingDown = true;
    }
}
