using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.UI.Components.Animations
{
    public class SizeUIAnimation : UIAnimation
    {
        [Header("Size Animation Settings")]
        [SerializeField] private AnimationCurve _resizeCurve = AnimationCurve.Linear(0f, 0f, 1f,1f);
        [SerializeField] private int _aniamtionPerSecond = 60;
        [SerializeField] private Vector2 _targetScale = new Vector2(1,1);
        private IEnumerator _animationCoroutine;
        private RectTransform _target;
        [SerializeField]private Vector2 _startSize;
        [SerializeField]private Vector2 _targetSize;
        private void Awake()
        {
            _target = GetComponent<RectTransform>();
        }
        private void Start()
        {
            _startSize = _target.sizeDelta;
            _targetSize = _startSize * _targetScale;
        }
        protected override void PlayAnimation()
        {
            if(_animationCoroutine != null) StopCoroutine(_animationCoroutine);
            _animationCoroutine = ResizeAnimation(_target.sizeDelta, _targetSize);
            StartCoroutine(_animationCoroutine);
        }
        protected override void RewindAnimation()
        {
            if(_animationCoroutine != null) StopCoroutine(_animationCoroutine);
            _animationCoroutine = ResizeAnimation(_target.sizeDelta, _startSize);
            StartCoroutine(ResizeAnimation(_targetSize, _startSize));
        }
        private IEnumerator ResizeAnimation(Vector2 start, Vector2 end){
            float timePerUpdate = 1f / _aniamtionPerSecond;
            float timer = 0f;
            while (timer < _animationTime) {
                timer += timePerUpdate;
                _target.sizeDelta = Vector2.Lerp(start, end,_resizeCurve.Evaluate(timer / _animationTime));
                yield return new WaitForSeconds(timePerUpdate);
            }
            Complete();
            _animationCoroutine = null;
        }
    }
}