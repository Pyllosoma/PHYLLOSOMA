using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.UI.Components.Animations
{
    public class SizeUIAnimation : UIAnimation
    {
        [SerializeField] private AnimationCurve _resizeCurve = AnimationCurve.Linear(0f, 0f, 1f,1f);
        [SerializeField] private int _logResizePerSecond = 30;
        [SerializeField] private Vector2 _targetScale = new Vector2(1,1);
        private RectTransform _target;
        private Vector2 _startSize;
        private Action _onComplete;
        private void Awake()
        {
            _target = GetComponent<RectTransform>();
        }
        private void Start()
        {
            _startSize = _target.sizeDelta;
            
        }
        public override void Play(Action onComplete = null)
        {
            _onComplete = onComplete;
            _targetScale = _startSize * _targetScale;
            StartCoroutine(ResizeAnimation());
        }
        public override void Rewind(Action onComplete = null)
        {
            _onComplete = onComplete;
            _targetScale = _startSize;
            StartCoroutine(ResizeAnimation());
        }
        private IEnumerator ResizeAnimation(){
            float timePerUpdate = 1f / _logResizePerSecond;
            float timer = 0f;
            while (timer < _animationTime) {
                timer += timePerUpdate;
                _target.sizeDelta = Vector2.Lerp(_target.sizeDelta, _targetScale,_resizeCurve.Evaluate(timer / _animationTime));
                yield return new WaitForSeconds(timePerUpdate);
            }
            _onComplete?.Invoke();
        }
    }
}