using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int Score { get; private set; }

    public event Action<int> OnScoreChanged;
    
    public void AddScore(int add)
    {
        Score++;
        OnScoreChanged?.Invoke(Score);
    }
}
