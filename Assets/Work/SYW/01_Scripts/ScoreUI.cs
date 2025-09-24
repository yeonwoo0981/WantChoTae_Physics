using System;
using TMPro;
using UnityEngine;

namespace Work.SYW._01_Scripts
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private ScoreSystem _scoreSystem;

        private void Awake()
        {
            _scoreText.text = $"score: {_scoreSystem.CurrentScore}";
        }

        private void OnEnable()
        {
            _scoreSystem.OnScoreChanged += UpdateScoreUI;
        }

        private void OnDisable()
        {
            _scoreSystem.OnScoreChanged -= UpdateScoreUI;
        }

        private void UpdateScoreUI(int score)
        {
            _scoreText.text = $"score: {score}";
        }
    }
}
