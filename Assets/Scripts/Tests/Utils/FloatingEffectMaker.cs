using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Tests.Utils
{
    public class FloatingEffectMaker : MonoBehaviour
    {
        [SerializeField] private bool _isLocal = true;
        [SerializeField] private float _floatingDistance = 1f;
        [SerializeField] private float _floatingTime = 1f;
        [SerializeField] private float _floatingUpdateRate = 0.1f;
        [SerializeField] private AnimationCurve _floatingCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        [SerializeField] private Vector3 _startPosition = Vector3.zero;
        [SerializeField] private Vector3 _endPosition = Vector3.zero;
        private bool _isStartPos = true;
        private void Start()
        {
            var position = _isLocal?transform.localPosition : transform.position;
            _startPosition = new Vector3(position.x, position.y, position.z);
            _endPosition = new Vector3(position.x, position.y + _floatingDistance, position.z);
            _isStartPos = false;
            StartCoroutine(CreateFloatEffect());
        }
        
        private IEnumerator CreateFloatEffect()
        {
            var startPosition = _isStartPos ? _startPosition : _endPosition;
            var endPosition = _isStartPos ? _endPosition : _startPosition;
            float time = 0f;
            while (time <= _floatingTime) {
                var pos = 
                    Vector3.Lerp(
                        startPosition, 
                        endPosition,
                        _floatingCurve.Evaluate(time / _floatingTime));
                if (_isLocal) {
                    transform.localPosition = pos;
                } else {
                    transform.position = pos;
                }
                time += _floatingUpdateRate;
                yield return new WaitForSeconds(_floatingUpdateRate);
            }
            _isStartPos = !_isStartPos;
            StartCoroutine(CreateFloatEffect());
        }
    }
}