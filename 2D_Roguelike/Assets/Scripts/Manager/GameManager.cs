using UnityEngine;
using System;
using Unity.VisualScripting;

public class GameManager : SingleTon<GameManager>
{
    public void SwitchGame()
    {
        Time.timeScale =Time.timeScale == 0f ? 1f : 0f;
    }
}
