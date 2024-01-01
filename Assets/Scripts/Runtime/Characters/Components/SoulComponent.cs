using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Runtime.Characters.Components
{
    public class SoulComponent : MonoBehaviour
    {
        public float SoulMax => _soulMax;
        public float SoulValue => _soulValue;
        public float AdditionalRecover
        {
            get => _additionalRecover;
            set => _additionalRecover = value;
        }
        public float AdditionalRecoverPercent
        {
            get => _additionalRecoverPercent;
            set {
                if (value > 1f) value = 1f;
                if (value < 0f) value = 0f;
                _additionalRecoverPercent = value;
            
            }
        } 
        public bool EnableSoul {
            get => _enableSoul;
            set => _enableSoul = value;
        }
        [Title("Soul Option")]
        [SerializeField] private bool _enableSoul = true;
        [SerializeField] private float _soulMax = 100f;
        [SerializeField] private float _soulValue = 50f;
        [SerializeField] private float _startSoulValuePercent = 0.5f;
        [Title("Recover Option")]
        [SerializeField] private bool _usePercent = false;
        [HideIf("_usePercent")] [SerializeField] private float _recoverPerSecond = 1f;
        [HideIf("_usePercent")] [SerializeField] private float _additionalRecover = 0f;
        [ShowIf("_usePercent")] [Range(0f,1f)] [SerializeField] private float _recoverPercentPerSecond = 1f;
        [ShowIf("_usePercent")] [Range(0f,1f)] [SerializeField] private float _additionalRecoverPercent = 0f;
        [FoldoutGroup("Soul Events")]
        [SerializeField] private UnityEvent _onSoulEmpty = new UnityEvent();
        [FoldoutGroup("Soul Events")]
        [SerializeField] private UnityEvent<float> _onSoulValueChanged = new UnityEvent<float>();
        [FoldoutGroup("Soul Events")]
        [SerializeField] private UnityEvent _onSoulFull = new UnityEvent();
        private bool _isFull = false;
        private void OnDisable()
        {
            Reset();
        }
        private void FixedUpdate()
        {
            if (!_enableSoul) return;
            if (_isFull) return;
            _soulValue += (_usePercent?_recoverPercentPerSecond + _additionalRecoverPercent:_recoverPerSecond +  _additionalRecover) * Time.fixedDeltaTime;
            if (_soulValue >= _soulMax) {
                _soulValue = _soulMax;
                _isFull = true;
                _onSoulFull?.Invoke();
            }
            _onSoulValueChanged?.Invoke(_soulValue);
        }
        public void GiveSoulDamage(float damage)
        {
            if (!_enableSoul) return;
            _soulValue -= damage;
            _isFull = _soulValue >= _soulMax;
            if (_soulValue <= 0) {
                _soulValue = 0f;
                _onSoulEmpty.Invoke();
            }
            _onSoulValueChanged?.Invoke(_soulValue);
        }

        public void Reset()
        {
            _enableSoul = true;
            _soulValue = _soulMax * _startSoulValuePercent;
            _additionalRecover = 0f;
            _additionalRecoverPercent = 0f;
            _onSoulValueChanged?.Invoke(_soulValue);
        }
    }
}