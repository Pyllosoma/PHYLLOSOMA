﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI.Components.Sliders
{
    public class CharacterSlider : MonoBehaviour
    {
        [SerializeField] private int _maxHealth = 100;
        [SerializeField] private int _currentHealth = 100;
        [SerializeField] private float _followTime = 1f;
        [SerializeField] private int _followWaitTime = 1;
        [SerializeField] private float _updatesPerSecond = 60f;
        [SerializeField] private Slider _foreSlider;
        [SerializeField] private Slider _backSlider;
        private IEnumerator _followCurrentHealthCoroutine = null;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) {
                SetHealth(_currentHealth - 10);
            }
        }
        public void Init(int maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;
            _foreSlider.value = 1f;
            _backSlider.value = 1f;
        }
        public void SetHealth(int health,int maxHealth = -1)
        {
            if (maxHealth != -1) {
                _maxHealth = maxHealth;
            }
            _currentHealth = health;
            //if value is under 0, set it to 0
            if (_currentHealth < 0) {
                _currentHealth = 0;
            }
            _foreSlider.value = (float)_currentHealth / _maxHealth;
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