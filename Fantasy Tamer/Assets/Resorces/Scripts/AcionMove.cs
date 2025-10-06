using UnityEngine;

public class AcionMove : MonoBehaviour
{
    public enum AuraType
    {
        Blaze,
        Aqua,
        Floral,
        Spark
    }
    public AuraType type;
    public int basePower;
    public bool sideEffect;
    public int Priority;
    public bool critHigh;
    public virtual void SideEffect()
    {

    }
}
