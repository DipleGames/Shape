using UnityEngine;


public enum OperationType { add, mul }

[CreateAssetMenu(fileName = "Agument", menuName = "Aguments/StatAgument")]
public class StatAgument : ScriptableObject
{
    public string agumentName;
    public string agumentDesc;
    public StatType statType;
    public OperationType operationType;
    public float value;
}
