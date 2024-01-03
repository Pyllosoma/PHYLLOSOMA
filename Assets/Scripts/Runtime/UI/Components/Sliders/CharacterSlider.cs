using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Runtime.UI.Components.Sliders
{
    public class CharacterSlider : MonoBehaviour
    {
        public float MaxValue => _maxValue;
        public float Value => _value;
        [SerializeField] private float _maxValue = 100;
        [SerializeField] private float _value = 100;
        [SerializeField] private float _followTime = 1f;
        [SerializeField] private float _followWaitTime = 0.5f;
        [SerializeField] private int _updatesPerSecond = 60;
        [SerializeField] private Slider _foreSlider;
        [SerializeField] private Slider _backSlider;
        private IEnumerator _followCurrentHealthCoroutine = null;
        public void Init(float maxValue)
        {
            _maxValue = maxValue;
            _value = maxValue;
            _foreSlider.value = 1f;
            _backSlider.value = 1f;
        }
        public void SetValue(float value,float maxValue = -1)
        {
            if (maxValue != -1) {
                _maxValue = maxValue;
            }
            _value = value;
            //if value is under 0, set it to 0
            if (_value < 0) {
                _value = 0;
            }
            _foreSlider.value = _value / _maxValue;
            if (_followCurrentHealthCoroutine != null) StopCoroutine(_followCurrentHealthCoroutine);
            _followCurrentHealthCoroutine = FollowCurrentHealth();
            StartCoroutine(_followCurrentHealthCoroutine);
        }
        private IEnumerator FollowCurrentHealth()
        {
            float updateTick = 1f / _updatesPerSecond;
            float targetValue = _foreSlider.value;
            float currentValue = _backSlider.value;
            float tempValue = currentValue;
            float time = 0f;
            yield return new WaitForSeconds(_followWaitTime);
            while (time < _followTime) {
                time += updateTick;
                tempValue = Mathf.Lerp(tempValue, targetValue, time / _followTime);
                _backSlider.value = tempValue;
                yield return new WaitForSeconds(updateTick);
            }
            _backSlider.value = targetValue;
        }
        private void OnValidate()
        {
            _foreSlider = transform.Find("ForeSlider").GetComponent<Slider>();
            _backSlider = transform.Find("BackSlider").GetComponent<Slider>();
            if (_foreSlider == null) {
                Debug.LogError("ForeSlider is null");
            }
            if (_backSlider == null) {
                Debug.LogError("BackSlider is null");
            }
        }
    }
}