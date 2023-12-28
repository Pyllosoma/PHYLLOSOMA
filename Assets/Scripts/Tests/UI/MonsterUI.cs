using Runtime.UI.Components.Animations;
using Runtime.UI.Components.Sliders;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Tests.UI
{
    public class MonsterUI : MonoBehaviour
    {
        public bool UseAutoToggle
        {
            get => _useAutoToggle;
            set {
                _useAutoToggle = value;
                _timer = 0f;
            }
        }
        [FormerlySerializedAs("useUseAutoToggle")]
        [FormerlySerializedAs("_autoToggle")]
        [Title("Auto Toggle Option")]
        [SerializeField] private bool _useAutoToggle = true;
        [SerializeField] private float _lifeTime = 5f;
        [SerializeField] UIAnimation _animation;
        [Title("UI Components")]
        [SerializeField] private CharacterSlider _healthSlider;
        [SerializeField] private CharacterSlider _soulGaugeSlider;
        [FoldoutGroup("Toggle Events")]
        [SerializeField] private UnityEvent _onToggle = new UnityEvent();
        [FoldoutGroup("Toggle Events")]
        [SerializeField] private UnityEvent _onUnToggle = new UnityEvent();
        private float _timer = 0f;
        private bool _isToggle = false;
        private void FixedUpdate()
        {
            if (!_useAutoToggle) return;
            _timer += Time.fixedDeltaTime;
            Toggle(_timer < _lifeTime);
        }
        public void Toggle(bool isToggle) {
            if (_isToggle == isToggle) return;
            _isToggle = isToggle;
            if (_isToggle) {
                _animation.Play();
                _onToggle.Invoke();
            }
            else {
                _animation.Rewind();
                _onUnToggle.Invoke();
            }
                
        }
        public void ResetValue(float health,float soulGauge)
        {
            _healthSlider.Init((int)health);
            _soulGaugeSlider.Init((int)soulGauge);
            _timer = _lifeTime + 1;
        }
        public void SetHealth(float health) {
            _timer = 0f;
            _healthSlider.SetHealth(health);
        }
        public void SetSoulGauge(float soulGauge) {
            _timer = 0f;
            _soulGaugeSlider.SetHealth(soulGauge);
        }
    }
}