using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Unity.Mathematics;
public class SpecialAgumentExecutor : MonoBehaviour
{
    [Header("prefab")]
    [SerializeField] private GameObject _fireField;

    public void Executor(SpecialAgument data, string agumentName)
    {
        switch(agumentName)
        {
            case "마나 젠":
                PlayerManager.Instance.playerController.manaRegen += 30f;
                break;
            case "스테미너 젠":
                PlayerManager.Instance.playerController.staminaRegen += 30f;
                break;
            case "생명력 흡수":
                PlayerManager.Instance.playerController.lifeLeech += 0.2f;
                break;
            case "Q쿨타임 감소":
                PlayerManager.Instance.character.Q_SkillInstance.cooldown *= 0.8f;
                break;
            case "W쿨타임 감소":
                PlayerManager.Instance.character.W_SkillInstance.cooldown *= 0.8f;
                break;
            case "E쿨타임 감소":
                PlayerManager.Instance.character.E_SkillInstance.cooldown *= 0.8f;
                break;
            case "R쿨타임 감소":
                PlayerManager.Instance.character.R_SkillInstance.cooldown *= 0.8f;
                break;
            case "불타는 발걸음":
                data.isUnique = true;
                Queue<GameObject> q = new Queue<GameObject>(); // 풀링
                for(int i=0; i<4; i++)
                {
                    var go = Instantiate(_fireField,transform);
                    go.GetComponent<FireField>().q = q;
                    go.SetActive(false);
                    q.Enqueue(go);
                }
                StartCoroutine(FireFieldCoroutine(q));
                break;
        }
    }

    IEnumerator FireFieldCoroutine(Queue<GameObject> q)
    {
        GameObject go;
        while(true)
        {
            go = q.Dequeue();
            go.SetActive(true);
            go.transform.position = PlayerManager.Instance.player.transform.position;
            go.GetComponent<FireField>().StartLifeCoroutine();
            yield return new WaitForSeconds(2f);
        }
    }
}
