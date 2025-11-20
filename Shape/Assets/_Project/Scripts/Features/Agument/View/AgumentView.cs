using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AgumentView : MonoBehaviour
{
    [Header("증강 관련")]
    public GameObject augument_Panel;


    public void UpdateAgumentUI(LevelSystem levelSystem)
    {
        GameObject[] agumentBtns = AgumentManager.Instance.agumentBtns;
        for (int i = 0; i < agumentBtns.Length; i++)
        {
            AgumentData data = agumentBtns[i].GetComponent<AgumentData>();
            Text agumentName = agumentBtns[i].transform.GetChild(0).GetComponent<Text>();
            Text agumentDesc = agumentBtns[i].transform.GetChild(1).GetComponent<Text>();
            Image agumentImg = agumentBtns[i].transform.GetChild(2).GetComponent<Image>();

            agumentName.text = data.agument.agumentName;
            agumentDesc.text = data.agument.agumentDesc;
        }
    }
}
