using System;
using DG.Tweening;
using Runtime.UI.Components.Sliders;
using UnityEngine;

namespace Tests.Characters
{
    public class MonsterUI : MonoBehaviour
    {
        [SerializeField] private Ease _ease = Ease.InSine;
        [SerializeField] private float _fadeTime = 0.5f;
        [SerializeField] private float _lifeTime = 5f;
        [SerializeField] private CharacterSlider _healthSlider;
        [SerializeField] private CharacterSlider _soulGaugeSlider;
        private CanvasGroup _canvasGroup;
        private float _timer = 0f;
        private bool _isOn = false;
        private void Awake() {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void FixedUpdate()
        {
            _timer += Time.fixedDeltaTime;
            FadeEvent(_timer < _lifeTime);
        }

        public void ResetValue(float health,float soulGauge)
        {
            _healthSlider.Init((int)health);
            _soulGaugeSlider.Init((int)soulGauge);
        }
        public void SetHealth(float health) {
            _timer = 0f;
            _healthSlider.SetHealth((int)health);
        }
        public void SetSoulGauge(float soulGauge) {
            _timer = 0f;
            _soulGaugeSlider.SetHealth((int)soulGauge);
        }
        private void FadeEvent(bool isOn) {
            if (_isOn == isOn) return;
            _isOn = isOn;
            _canvasGroup.DOFade(isOn ? 1f : 0f, _fadeTime).SetEase(_ease);
            
        }
    }
}