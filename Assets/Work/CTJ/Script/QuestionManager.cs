using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    [SerializeField] private ScoreSystem score;    // �ν����Ϳ��� �Ҵ� ����

    [Header("Question Data")]
    [Tooltip("���� �������� ����(0���� ����)")]
    [SerializeField] private int _currentIndex = 0;
    public string[] _questions;
    [Tooltip("1=���� O, 0=���� X")]
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
    [Tooltip("���� ������ ǥ�õǴ� �ð�")]
    public float _timeDelayed = 1.5f;
    [Tooltip("�ؼ��� ǥ�õǴ� �ð�")]
    public float _commentaryTime = 2.0f;

    [Header("Anim Y Pos")]
    public float showY = 0f;       // ������ Y(��Ŀ)
    public float hideY = -150f;    // ���� Y(��Ŀ)

    private void Start()
    {
        // ���� �� �г� ��ġ �ʱ�ȭ
        if (OPanel) OPanel.anchoredPosition = new Vector2(OPanel.anchoredPosition.x, hideY);
        if (XPanel) XPanel.anchoredPosition = new Vector2(XPanel.anchoredPosition.x, hideY);
        if (CPanel) CPanel.SetActive(false);

        RefreshQuestionUI();
    }

    // ====== �ܺο��� ȣ��Ǵ� �������̽� ======
    public void ChooseO()
    {
        bool isCorrect = IsCorrect(choiceIsO: true);
        ShowChoice(OPanel);
        ApplyScore(isCorrect);
        // �����̸� �ؼ� ǥ��(��ġ ������ �Ʒ� �� �ּ� ó��)
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
        // Sai: ����/���� ���� ��� ���� ����
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
        // ����� ���� ProceedAfterDelay���� �ϰ� ó��
    }

    private IEnumerator ProceedAfterDelay()
    {
        yield return new WaitForSeconds(_timeDelayed);
        // �г� �����
        HidePanel(OPanel);
        HidePanel(XPanel);
        // �ؼ��� �ڽ� Ÿ�̸ӷ� ����

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

        // ������ ���� �Ѱ�ٸ� ���� ó��(���ϴ� �������� ��ü)
        if (_currentIndex >= _questions.Length)
        {
            _currentIndex = _questions.Length - 1;
            // ��: ��� �г� �����, ������ ǥ�� ��
            if (_questionText) _questionText.text = "��!";
            if (_numText) _numText.text = "";
            return;
        }

        if (_numText) _numText.text = $"{_currentIndex + 1}.";
        if (_questionText) _questionText.text = _questions[_currentIndex];
    }
}
