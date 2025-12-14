using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Agument", menuName = "Aguments/SpecialAgument")]
public class SpecialAgument : ScriptableObject
{
    public string agumentName;
    public string agumentDesc;

    public bool isUnique = false;
}
