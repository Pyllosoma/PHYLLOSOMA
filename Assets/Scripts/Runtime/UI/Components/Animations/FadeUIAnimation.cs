using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI.Components.Animations
{
    public class FadeUIAnimation : UIAnimation
    {
        [Header("Fade Animation Settings")] 
        [SerializeField] private bool _resetAlphaOnStart = false;
        [SerializeField] private Ease _fadeEase = Ease.Linear;
        [SerializeField] private float _startAlpha = 0f;
        [SerializeField] private float _endAlpha = 1f;
        private CanvasGroup _canvasGroup;
        private Image _image;
        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _image = GetComponent<Image>();
        }

        private void Start()
        {
            if (!_resetAlphaOnStart) return;
            if (_canvasGroup) _canvasGroup.alpha = _startAlpha;
            if (_image) _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, _startAlpha);
        }

        protected override void PlayAnimation()
        {
            //Debug.Log("PlayAnimation : " + name);
            if (_canvasGroup) {
                _canvasGroup.alpha = _startAlpha;
                _canvasGroup.DOFade(_endAlpha, _animationTime).SetEase(_fadeEase).OnComplete(Complete);
            }
            if (_image) {
                _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, _startAlpha);
                _image.DOFade(_endAlpha, _animationTime).SetEase(_fadeEase).OnComplete(Complete);
            }
        }
        protected override void RewindAnimation()
        {
            //Debug.Log("RewindAnimation : " + name);
            if (_canvasGroup) {
                _canvasGroup.alpha = _endAlpha;
                _canvasGroup.DOFade(_startAlpha, _animationTime).SetEase(_fadeEase).OnComplete(Complete);
            }
            if (_image) {
                _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, _endAlpha);
                _image.DOFade(_startAlpha, _animationTime).SetEase(_fadeEase).OnComplete(Complete);
            }
        }
    }
}