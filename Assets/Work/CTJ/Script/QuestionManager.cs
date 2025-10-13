using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    public int _currentQuestionNum = 1;
    [SerializeField] private string[] _questions;

    [SerializeField] private GameObject questionPanel;
    [SerializeField] private GameObject OPanel;
    [SerializeField] private GameObject XPanel;

    [SerializeField] private TextMeshProUGUI _questionText;
    [SerializeField] private TextMeshProUGUI _numText;

    [SerializeField] private float _timeDelayed = 5.0f;


    private void Start()
    {
        StartQuestions();
    }

    public void OtextOn()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(OPanel.transform.DOMoveY(0, 0.5f));
        StartCoroutine(TimeDelayed(OPanel));
    }

    public void XtextOn()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(XPanel.transform.DOMoveY(0, 0.5f));
        StartCoroutine(TimeDelayed(XPanel));
    }

    IEnumerator TimeDelayed(GameObject panel)
    {
        Sequence sequence = DOTween.Sequence();

        yield return new WaitForSeconds(_timeDelayed);

        sequence.Append(panel.transform.DOMoveY(-150, 0.5f));
        sequence.Kill();
    }

    public void StartQuestions()
    {
        _currentQuestionNum++;
        _numText.text = $"{_currentQuestionNum}.";
        _questionText.text = _questions[_currentQuestionNum - 1];
    }
}
