using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.UI.Components.Animations
{
    public class MoveUIAnimation : UIAnimation
    {
        [Header("Move Animation Settings")]
        [SerializeField] private Vector2 _movePos = Vector2.zero;
        [SerializeField] private Ease _moveEase = Ease.Linear;
        private RectTransform _rectTransform;
        private Vector2 _startPosition;
        public void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _startPosition = _rectTransform.anchoredPosition;
        }
        protected override void PlayAnimation()
        {
            _rectTransform.DOAnchorPos(_startPosition + _movePos, _animationTime).SetEase(_moveEase).OnComplete(Complete);
        }
        protected override void RewindAnimation()
        {
            _rectTransform.DOAnchorPos(_startPosition, _animationTime).SetEase(_moveEase).OnComplete(Complete);
        }
    }
}