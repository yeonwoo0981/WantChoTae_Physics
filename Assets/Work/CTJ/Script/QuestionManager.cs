using DG.Tweening;
using TMPro;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    [SerializeField] private GameObject questionPanel;
    [SerializeField] private GameObject OPanel;
    [SerializeField] private GameObject XPanel;

    [SerializeField] private TextMeshProUGUI _questionText;
    [SerializeField] private TextMeshProUGUI _numText;

    Sequence sequence = DOTween.Sequence();
}
