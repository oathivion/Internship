using UnityEngine;

public class KeepUI : MonoBehaviour
{
    private static KeepUI instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist between scenes
        }
        else
        {
            Destroy(gameObject); // Avoid duplicates
        }
    }
}
