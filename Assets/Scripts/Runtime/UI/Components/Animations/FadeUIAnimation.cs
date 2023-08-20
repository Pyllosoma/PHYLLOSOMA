using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.UI.Components.Animations
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FadeUIAnimation : UIAnimation
    {
        [Header("Fade Animation Settings")]
        [SerializeField] private Ease _fadeEase = Ease.Linear;
        [SerializeField] private float _startAlpha = 0f;
        [SerializeField] private float _endAlpha = 1f;
        private CanvasGroup _canvasGroup;
        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }
        protected override void PlayAnimation()
        {
            _canvasGroup.alpha = _startAlpha;
            _canvasGroup.DOFade(_endAlpha, _animationTime).SetEase(_fadeEase).OnComplete(Complete);
        }
        protected override void RewindAnimation()
        {
            _canvasGroup.alpha = _endAlpha;
            _canvasGroup.DOFade(_startAlpha, _animationTime).SetEase(_fadeEase).OnComplete(Complete);
        }
    }
}