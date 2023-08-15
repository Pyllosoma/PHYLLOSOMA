using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.UI.Components.Animations
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FadeUIAnimation : UIAnimation
    {
        [SerializeField] private Ease _fadeEase = Ease.Linear;
        private CanvasGroup _canvasGroup;
        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }
        public override void Play(Action onComplete = null)
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.DOFade(1f, _animationTime).SetEase(_fadeEase).onComplete += () => {
                onComplete?.Invoke();
            };
        }
        public override void Rewind(Action onComplete = null)
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.DOFade(0f, _animationTime).SetEase(_fadeEase).onComplete += () => {
                onComplete?.Invoke();
            };
        }
    }
}