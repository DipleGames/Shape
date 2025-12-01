using System;
using UnityEngine;

public class CoinManager : SingleTon<CoinManager>
{
    public event Action<int> OnCoinChanged;
    [SerializeField] private int _coin;
    public int Coin
    {
        get => _coin;
        set
        {
            int nv = value;
            _coin = nv;
            OnCoinChanged?.Invoke(_coin);
        }
    }

    void Start()
    {
        OnCoinChanged += UIManager.Instance.coinView.OnUpdateCoinText;
    }

    public void AddCoin(int amount)
    {
        Coin += amount;
    }

    public void SubtractCoin(int amount)
    {
        Coin -= amount;
    }
}
