using UnityEngine;

[CreateAssetMenu(fileName ="Move",menuName = "CreateMove/Move")]
public class AcionMove : ScriptableObject
{
    public enum AuraType
    {
        Null,
        Blaze,
        Aqua,
        Floral,
        Spark
    }
    public AuraType type;
    public int basePower;
    public int Priority;
    public bool critHigh;
    public string discription;
}
