using System;
using UnityEngine;

public class Manager_Score : MonoBehaviour,ITakeResurses
{
  
    private int _score;
    private int _coins;

    public event Action<int, int> OnChangeScoreCoins;

    public void Take(int coins, int score)
    {
        _coins += coins;
        _score += score;
        OnChangeScoreCoins?.Invoke(_coins, _score);
    }
}
