using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.UI.Components.Animations
{
    public class MoveUIAnimation : UIAnimation
    {
        [SerializeField] private Vector2 _movePos = Vector2.zero;
        [SerializeField] private Ease _moveEase = Ease.Linear;
        private RectTransform _rectTransform;
        private Vector2 _startPosition;
        public void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _startPosition = _rectTransform.anchoredPosition;
        }
        public override void Play(Action onComplete = null)
        {
            _rectTransform.DOAnchorPos(_startPosition + _movePos, _animationTime).SetEase(_moveEase).onComplete += () => {
                onComplete?.Invoke();
            };
        }
        public override void Rewind(Action onComplete = null)
        {
            _rectTransform.DOAnchorPos(_startPosition, _animationTime).SetEase(_moveEase).onComplete += () => {
                onComplete?.Invoke();
            };
        }
    }
}