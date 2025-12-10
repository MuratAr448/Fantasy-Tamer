using UnityEngine;

public class Dontdest : MonoBehaviour
{
    public static GameObject instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this.gameObject;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
    }
}
