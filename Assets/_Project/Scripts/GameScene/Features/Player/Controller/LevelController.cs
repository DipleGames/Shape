// LevelController.cs
using UnityEngine;
using System;

public class LevelController : MonoBehaviour
{
    public PlayerManager pm;

    public LevelModel levelModel = new LevelModel(0,100f);

    public event Action<LevelModel> OnLevelChanged;
    public event Action<LevelModel> OnExpChanged;
    public event Action<int> OnRequiredExpChanged;

    void Awake()
    {
        pm = PlayerManager.Instance;
    }

    void Start()
    {
        OnExpChanged += UIManager.Instance.playerView.UpdateUIOnChangePlayerVital;
        OnLevelChanged += UIManager.Instance.playerView.UpdateUIOnLevelUp;
        OnLevelChanged += AgumentManager.Instance.SetAgument;
        OnLevelChanged += UIManager.Instance.agumentView.UpdateAgumentUI;
        OnLevelChanged += pm.statCalculator.CalculateOnLevelUp;
    }

    public void AddExp(float amount)
    {
        if (amount <= 0f) return;

        levelModel.AddExp(amount);
        OnExpChanged?.Invoke(levelModel);

        TryLevelUpLoop();
    }

    private void TryLevelUpLoop()
    {
        while (levelModel.CanLevelUp())
        {
            levelModel.LevelUp();
            OnLevelChanged?.Invoke(levelModel);

            UpdateRequiredExp(levelModel.Level);
        }

        OnExpChanged?.Invoke(levelModel);
    }

    private void UpdateRequiredExp(int level)
    {
        float nextRequired = levelModel.RequiredExp + (level - 1) * 5f;
        levelModel.SetRequiredExp(nextRequired);
        OnRequiredExpChanged?.Invoke(level);
    }
}
