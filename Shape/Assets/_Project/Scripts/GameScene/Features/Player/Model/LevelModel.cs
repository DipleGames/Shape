// LevelModel.cs
using System;

[Serializable]
public class LevelModel
{
    private int _level;
    private float _exp;
    private float _requiredExp;

    public int Level => _level;
    public float Exp => _exp;
    public float RequiredExp => _requiredExp;

    public LevelModel(int startLevel = 0, float startRequiredExp = 100f)
    {
        _level = startLevel;
        _requiredExp = startRequiredExp;
        _exp = 0f;
    }

    public void AddExp(float amount)
    {
        _exp += amount;
    }

    public bool CanLevelUp()
    {
        return _exp >= _requiredExp;
    }

    public void LevelUp()
    {
        _exp -= _requiredExp;
        _level++;
    }

    public void SetRequiredExp(float value)
    {
        _requiredExp = value;
    }
}
