using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class UIManager : SingleTon<UIManager>
{
    [Header("View")]
    public PlayerView playerView;
    public AgumentView agumentView;
    public SkillRuntimeView skillRuntimeView;
    public ThreatGaugeView threatGaugeView;
    public ShopView shopView;
    public CoinView coinView;
    public ShapeGrowthView shapeGrowthView;

    [Header("UI 패널")]
    public GameObject gameover_Panel;


    void OnEnable()  => HideCursor();
    void OnDisable() => ShowCursor();

    public void SwitchUI(GameObject ui)
    {
        bool b = ui.activeSelf ? false : true;
        ui.SetActive(b);
    }

    void HideCursor()
    {
        Cursor.visible = false;

    }
    
    void ShowCursor()
    {
        Cursor.visible = true;
    }


}
