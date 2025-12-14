using UnityEngine;

public class AgumentDatabase : MonoBehaviour
{
    [Header("증강 Model")]
    public StatAgument[] statAguments;
    public SpecialAgument[] specialAguments;

     // 복사본으로 체인지
    void Start()
    {
        for(int i=0; i<statAguments.Length; i++)
        {
            statAguments[i] = Instantiate(statAguments[i]);
        }

        for(int i=0; i<specialAguments.Length; i++)
        {
            specialAguments[i] = Instantiate(specialAguments[i]);
        }
    }
}
