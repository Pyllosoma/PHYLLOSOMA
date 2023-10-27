using System.Collections;
using TMPro;
using UnityEngine;

namespace Runtime.UI.Components.Info.Indicators
{
    public class ValueIndicator : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _moneyText;
        [SerializeField] private int _curValue = 0;
        [SerializeField] private int _targetValue;
        [SerializeField] private float _updateTime = 1f;
        [SerializeField] private int _updatePerSecond = 60;
        [SerializeField] private AnimationCurve _updateCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        private IEnumerator _valueUpdateCoroutine = null;
        private void OnEnable()
        {
            //Get money info and write to text.
        }
        public void SetStartValue(int value)
        {
            _curValue = value;
            _targetValue = value;
            _moneyText.text = _curValue.ToString();
        }
        public void UpdateValue(int value)
        {
            if (value == _targetValue) {
                return;
            }
            _targetValue = value;
            if (_valueUpdateCoroutine != null) {
                StopCoroutine(_valueUpdateCoroutine);
            }
            _valueUpdateCoroutine = UpdateMoney();
            StartCoroutine(_valueUpdateCoroutine);
        }
        private IEnumerator UpdateMoney()
        {
            float timePerUpdate = 1f / _updatePerSecond;
            float timer = 0f;
            int gap = _targetValue - _curValue;
            int startValue = _curValue;
            while (timer < _updateTime) {
                timer += timePerUpdate;
                _curValue = Mathf.RoundToInt(_updateCurve.Evaluate(timer / _updateTime) * gap) + startValue;
                _moneyText.text = _curValue.ToString();
                yield return new WaitForSecondsRealtime(timePerUpdate);
            }
            _curValue = _targetValue;
        }
    }
}