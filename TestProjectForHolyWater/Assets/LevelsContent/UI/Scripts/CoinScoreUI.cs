using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CoinScoreUI : MonoBehaviour
{
    private ScoreManager _scoreManager;
    private Text _scroreText;

    [Inject]
    private void Construct(ScoreManager scoreManager) 
    {
        _scoreManager = scoreManager;
        _scoreManager.OnScoreChanged += ChangeScore;
    }

    private void OnDestroy()
    {
        _scoreManager.OnScoreChanged -= ChangeScore;
    }

    private void Awake()
    {
        _scroreText = GetComponent<Text>();
    }

    private void ChangeScore(int newScore)
    {
        _scroreText.text = "x " + newScore;
    }
}
