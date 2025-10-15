using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScoreSystem : MonoBehaviour
{
    public int _startScore = 100;
    public int _scoreStep = 20;

    public int CurrentScore { get; private set; }

    public event Action<int> OnScoreChanged;
    public event Action GameOver;

    private void Awake()
    {
        CurrentScore = _startScore;
        OnScoreChanged?.Invoke(CurrentScore);
    }

    private void Update()
    {
        // 임시 update 코드
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
            AddScore();
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
            ReduceScore();
    }

    public void AddScore()
    {
        CurrentScore += _scoreStep;
        OnScoreChanged?.Invoke(CurrentScore);
        Debug.Log("스획");
    }

    public void ReduceScore()
    {
        CurrentScore -= _scoreStep;
        OnScoreChanged?.Invoke(CurrentScore);
        Debug.Log("스잃");
        if (CurrentScore <= 0)
        {
            GameOver?.Invoke();
        }
    }
}
