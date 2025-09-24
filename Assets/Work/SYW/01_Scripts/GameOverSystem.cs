using System;
using UnityEngine;

namespace Work.SYW._01_Scripts
{
    public class GameOverSystem : MonoBehaviour
    {
        [SerializeField] private ScoreSystem _scoreSystem;

        private void OnEnable()
        {
            _scoreSystem.GameOver += GameOverHandler;
        }

        private void OnDisable()
        {
            _scoreSystem.GameOver -= GameOverHandler;
        }

        private void GameOverHandler()
        {
            Application.Quit();
        }
    }
}
