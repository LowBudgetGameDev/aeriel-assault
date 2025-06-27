using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public event EventHandler OnScoreChanged;

    private int score;

    private void Awake()
    {
        Instance = this;
    }

    public void IncreaseScore(int amount = 1)
    {
        score += amount;

        OnScoreChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetScore()
    {
        return score;
    }
}
