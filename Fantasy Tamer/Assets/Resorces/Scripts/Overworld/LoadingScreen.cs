using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    public static GameObject instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = gameObject.transform.parent.gameObject;
        }
        else
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
        DontDestroyOnLoad(instance);
    }
}
