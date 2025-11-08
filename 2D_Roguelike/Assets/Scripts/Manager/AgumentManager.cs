using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;


public class AgumentManager : SingleTon<AgumentManager>
{
    private PlayerManager _pm;
    private UIManager _um;
    private AgumentDatabase _ad;
    HashSet<int> hash = new HashSet<int>();
 
    public GameObject[] agumentBtns;

    void Start()
    {
        _pm = PlayerManager.Instance;
        _um = UIManager.Instance;
        _ad = GetComponent<AgumentDatabase>();
    }

    public void SetAgument(LevelSystem levelSystem)
    {
        while (hash.Count < _ad.aguments.Length)
        {
            int ran = Random.Range(0, _ad.aguments.Length);
            hash.Add(ran);
        }

        List<int> list = hash.ToList();  // using System.Lin
        for(int i=0; i<agumentBtns.Length; i++)
        {
            AgumentData data = agumentBtns[i].GetComponent<AgumentData>();
            data.agument = _ad.aguments[list[i]];
        }
    }

    public void SelectAgument()
    {
        GameObject clickedObj = EventSystem.current.currentSelectedGameObject;
        AgumentData data = null; 
        if (clickedObj != null)
        {
            data = clickedObj.GetComponent<AgumentData>();
            Debug.Log(data.agument.agumentName);
        }
        _pm.statCalculator.CalculateOnSelecetAgument(data);
        _um.SwitchUI(_um.augument_Panel);
        GameManager.Instance.SwitchGame();
    }
}
