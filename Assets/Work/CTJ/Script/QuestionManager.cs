using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    [SerializeField] private ScoreSystem score;    // 인스펙터에서 할당 권장

    [Header("Question")]
    public int _currentQuestionNum = 1;
    public string[] _questions;

    [Header("Panels (RectTransform)")]
    public RectTransform OPanel;
    public RectTransform XPanel;

    [Header("UI Texts")]
    public TextMeshProUGUI _questionText;
    public TextMeshProUGUI _numText;

    [Header("Timing")]
    public float _timeDelayed = 5.0f;

    [Header("Anim Y Pos")]
    public float showY = 0f;       // 보여줄 때의 앵커 Y
    public float hideY = -150f;    // 숨길 때의 앵커 Y

    private void Start()
    {
        // 시작 시 패널을 숨긴 위치로 정렬(선택)
        if (OPanel) OPanel.anchoredPosition = new Vector2(OPanel.anchoredPosition.x, hideY);
        if (XPanel) XPanel.anchoredPosition = new Vector2(XPanel.anchoredPosition.x, hideY);

        StartQuestions();
    }

    public void OtextOn()
    {
        Debug.Log("OCheck");

        if (OPanel == null) return;
        // UI는 RectTransform의 앵커 포지션으로 움직이는게 안정적
        OPanel.DOKill();
        OPanel.DOAnchorPosY(showY, 0.5f).SetEase(Ease.OutCubic);
        StartCoroutine(AutoHide(OPanel));

        if (score != null) score.AddScore();
    }

    public void XtextOn()
    {
        Debug.Log("XCheck");

        if (XPanel == null) return;
        XPanel.DOKill();
        XPanel.DOAnchorPosY(showY, 0.5f).SetEase(Ease.OutCubic);
        StartCoroutine(AutoHide(XPanel));

        if (score != null) score.ReduceScore();
    }

    private IEnumerator AutoHide(RectTransform panel)
    {
        yield return new WaitForSeconds(_timeDelayed);
        if (panel != null)
        {
            panel.DOKill();
            panel.DOAnchorPosY(hideY, 0.5f).SetEase(Ease.InCubic);
        }
    }

    public void StartQuestions()
    {
        // 현재 번호와 텍스트 갱신
        _numText.text = $"{_currentQuestionNum}.";
        if (_questions != null && _questions.Length >= _currentQuestionNum)
        {
            _questionText.text = _questions[_currentQuestionNum - 1];
        }
        else
        {
        }

        // 다음 문제로 넘어갈 준비
        _currentQuestionNum++;
    }
}
