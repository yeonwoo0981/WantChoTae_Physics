using System;
using DG.Tweening;
using UnityEngine;

public class TitleDOTween : MonoBehaviour
{
    private RectTransform _rectTransform;
    private Vector2 _defaultPosition;
    [SerializeField] private float _yOffset;
    [SerializeField] private float _move = 400f;

    private void Awake()
    {
        _rectTransform = transform.GetComponent<RectTransform>();
        _defaultPosition = _rectTransform.anchoredPosition;
        _rectTransform.anchoredPosition = _defaultPosition + Vector2.up * _yOffset;
    }

    private void Start()
    {
        _rectTransform
            .DOAnchorPosY(_defaultPosition.y + _move, 1.5f)
            .SetEase(Ease.Flash)
            .SetUpdate(true);
    }
}
