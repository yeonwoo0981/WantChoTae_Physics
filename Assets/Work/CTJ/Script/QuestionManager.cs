using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    [SerializeField] private ScoreSystem score;    // 인스펙터에서 할당 권장

    [Header("Question Data")]
    [Tooltip("현재 진행중인 문제(0부터 시작)")]
    [SerializeField] private int _currentIndex = 0;
    public string[] _questions;
    [Tooltip("1=정답 O, 0=정답 X")]
    public int[] _questionsTF;
    public string[] _questionsCommentary;

    [Header("Panels (RectTransform)")]
    public RectTransform OPanel;
    public RectTransform XPanel;
    public GameObject CPanel;

    [Header("UI Texts")]
    public TextMeshProUGUI _questionText;
    public TextMeshProUGUI _numText;
    public TextMeshProUGUI _commentaryText;

    [Header("Timing")]
    [Tooltip("선택 연출이 표시되는 시간")]
    public float _timeDelayed = 1.5f;
    [Tooltip("해설이 표시되는 시간")]
    public float _commentaryTime = 2.0f;

    [Header("Anim Y Pos")]
    public float showY = 0f;       // 보여줄 Y(앵커)
    public float hideY = -150f;    // 숨길 Y(앵커)

    private void Start()
    {
        // 시작 시 패널 위치 초기화
        if (OPanel) OPanel.anchoredPosition = new Vector2(OPanel.anchoredPosition.x, hideY);
        if (XPanel) XPanel.anchoredPosition = new Vector2(XPanel.anchoredPosition.x, hideY);
        if (CPanel) CPanel.SetActive(false);

        RefreshQuestionUI();
    }

    // ====== 외부에서 호출되는 인터페이스 ======
    public void ChooseO()
    {
        bool isCorrect = IsCorrect(choiceIsO: true);
        ShowChoice(OPanel);
        ApplyScore(isCorrect);
        // 오답이면 해설 표시(원치 않으면 아래 줄 주석 처리)
        if (!isCorrect) ShowCommentaryOnce();
        StartCoroutine(ProceedAfterDelay());
    }

    public void ChooseX()
    {
        bool isCorrect = IsCorrect(choiceIsO: false);
        ShowChoice(XPanel);
        ApplyScore(isCorrect);
        if (!isCorrect) ShowCommentaryOnce();
        StartCoroutine(ProceedAfterDelay());
    }

    public void Skip()
    {
        // Sai: 점수/연출 없이 즉시 다음 문제
        AdvanceQuestion();
    }
    // =====================================

    private bool IsCorrect(bool choiceIsO)
    {
        if (_questionsTF == null || _currentIndex < 0 || _currentIndex >= _questionsTF.Length)
            return false;

        int answer = _questionsTF[_currentIndex]; // 1=O, 0=X
        return (choiceIsO && answer == 1) || (!choiceIsO && answer == 0);
    }

    private void ApplyScore(bool isCorrect)
    {
        if (score == null) return;
        if (isCorrect) score.AddScore();
        else score.ReduceScore();
    }

    private void ShowChoice(RectTransform panel)
    {
        if (panel == null) return;
        panel.DOKill();
        panel.DOAnchorPosY(showY, 0.25f).SetEase(Ease.OutCubic);
        // 숨기는 것은 ProceedAfterDelay에서 일괄 처리
    }

    private IEnumerator ProceedAfterDelay()
    {
        yield return new WaitForSeconds(_timeDelayed);
        // 패널 숨기기
        HidePanel(OPanel);
        HidePanel(XPanel);
        // 해설은 자신 타이머로 꺼짐

        AdvanceQuestion();
    }

    private void HidePanel(RectTransform panel)
    {
        if (panel == null) return;
        panel.DOKill();
        panel.DOAnchorPosY(hideY, 0.25f).SetEase(Ease.InCubic);
    }

    private void ShowCommentaryOnce()
    {
        if (CPanel == null || _questionsCommentary == null) return;

        CPanel.SetActive(true);
        int idx = Mathf.Clamp(_currentIndex, 0, _questionsCommentary.Length - 1);
        if (_commentaryText) _commentaryText.text = _questionsCommentary[idx];
        StartCoroutine(HideCommentaryAfter(_commentaryTime));
    }

    private IEnumerator HideCommentaryAfter(float t)
    {
        yield return new WaitForSeconds(t);
        if (CPanel) CPanel.SetActive(false);
    }

    private void AdvanceQuestion()
    {
        _currentIndex++;
        RefreshQuestionUI();
    }

    private void RefreshQuestionUI()
    {
        if (_questions == null || _questions.Length == 0)
        {
            if (_questionText) _questionText.text = "";
            if (_numText) _numText.text = "";
            return;
        }

        // 마지막 문제 넘겼다면 종료 처리(원하는 로직으로 교체)
        if (_currentIndex >= _questions.Length)
        {
            _currentIndex = _questions.Length - 1;
            // 예: 모든 패널 숨기기, “끝” 표시 등
            if (_questionText) _questionText.text = "끝!";
            if (_numText) _numText.text = "";
            return;
        }

        if (_numText) _numText.text = $"{_currentIndex + 1}.";
        if (_questionText) _questionText.text = _questions[_currentIndex];
    }
}
