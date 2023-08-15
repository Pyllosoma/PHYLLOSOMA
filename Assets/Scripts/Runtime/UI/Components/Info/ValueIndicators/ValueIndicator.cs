﻿using System.Collections;
using TMPro;
using UnityEngine;

namespace Runtime.UI.Components.Info.ValueIndicators
{
    public class ValueIndicator : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _moneyText;
        [SerializeField] private int _curValue = 0;
        [SerializeField] private int _targetValue;
        [SerializeField] private float _updateTime = 1f;
        [SerializeField] private int _updatePerSecond = 30;
        private IEnumerator _valueUpdateCoroutine = null;
        private int test = 0;
        private void OnEnable()
        {
            //Get money info and write to text.
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) {
                test += 1000;
                UpdateValue(test);
            }
        }
        public void UpdateValue(int value)
        {
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
            while (timer < _updateTime) {
                timer += timePerUpdate;
                _curValue = Mathf.RoundToInt(Mathf.Lerp(_curValue, _targetValue, timer / _updateTime));
                _moneyText.text = _curValue.ToString();
                yield return new WaitForSeconds(timePerUpdate);
            }
            _curValue = _targetValue;
        }
    }
}