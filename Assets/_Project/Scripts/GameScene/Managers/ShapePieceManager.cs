using System.Collections.Generic;
using System;
using UnityEngine;

public class ShapePieceManager : SingleTon<ShapePieceManager>
{
    public List<ShapePiece> shapePieceList = new List<ShapePiece>(); 
    public Dictionary<StatType, float> shapePieceDic = new Dictionary<StatType, float>();

    void Start()
    {
        foreach (StatType stat in Enum.GetValues(typeof(StatType)))
        {
            shapePieceDic[stat] = 0f;
        }
    }

    public void CalculateShapePiece(ShapePiece shapePiece)
    {
        shapePieceDic[shapePiece.statType] += shapePiece.statValue; 
        Debug.Log($"{shapePieceDic[shapePiece.statType]} 의 밸류는 {shapePiece.statValue} 입니다.");
    }
}
