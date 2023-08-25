using System.Collections;
using UnityEngine;

namespace Runtime.Utils
{
    public class FloatingEffectMaker : MonoBehaviour
    {
        [SerializeField] private bool _isLocal = true;
        [SerializeField] private float _floatingDistance = 1f;
        [SerializeField] private float _floatingTime = 1f;
        [SerializeField] private float _updatePerSecond = 30f;
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
            var updateTIme = 1f / _updatePerSecond;
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
                time += updateTIme;
                yield return new WaitForSeconds(updateTIme);
            }
            _isStartPos = !_isStartPos;
            StartCoroutine(CreateFloatEffect());
        }
    }
}