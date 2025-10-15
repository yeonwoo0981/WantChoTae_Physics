using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    [SerializeField] private ScoreSystem score;    // �ν����Ϳ��� �Ҵ� ����

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
    public float showY = 0f;       // ������ ���� ��Ŀ Y
    public float hideY = -150f;    // ���� ���� ��Ŀ Y

    private void Start()
    {
        // ���� �� �г��� ���� ��ġ�� ����(����)
        if (OPanel) OPanel.anchoredPosition = new Vector2(OPanel.anchoredPosition.x, hideY);
        if (XPanel) XPanel.anchoredPosition = new Vector2(XPanel.anchoredPosition.x, hideY);

        StartQuestions();
    }

    public void OtextOn()
    {
        Debug.Log("OCheck");

        if (OPanel == null) return;
        // UI�� RectTransform�� ��Ŀ ���������� �����̴°� ������
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
        // ���� ��ȣ�� �ؽ�Ʈ ����
        _numText.text = $"{_currentQuestionNum}.";
        if (_questions != null && _questions.Length >= _currentQuestionNum)
        {
            _questionText.text = _questions[_currentQuestionNum - 1];
        }
        else
        {
        }

        // ���� ������ �Ѿ �غ�
        _currentQuestionNum++;
    }
}
