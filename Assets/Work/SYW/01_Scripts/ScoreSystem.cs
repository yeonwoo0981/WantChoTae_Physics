using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScoreSystem : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI _scoreText;
   [SerializeField] private int _startScore = 100;
   private int _minusScore = 20;
   private bool _gameOver = false;
   [field:SerializeField] public int CurrentScore { get; private set; }

   private void Awake()
   {
      CurrentScore = _startScore;
      _scoreText.text = $"score: {CurrentScore}";
   }

   public void AddScore()
   {
      CurrentScore += 20;
      _scoreText.text = $"score: {CurrentScore}";
      Debug.Log("스코어 획득. 스획");
   }
   
   private void Update()
   {
      // 임시 코드
      if (Keyboard.current.digit1Key.wasPressedThisFrame)
      {
         AddScore();
      }

      if (Keyboard.current.digit2Key.wasPressedThisFrame)
      {
         GameOver(_minusScore);
      }
      // update 구문 다 임시
   }

   public void GameOver(int minus)
   {
         CurrentScore -= minus;
         _scoreText.text = $"score: {CurrentScore}";
         Debug.Log("스코어 잃음. 스잃");
         if (CurrentScore <= 0)
         {
            Debug.Log("ㅅㄱ");
            Application.Quit();
            _gameOver = true;
         }
   }
}
