using Runtime.Characters.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Characters.Components
{
public class HealthComponent : MonoBehaviour,IHealth
    {
        public bool IsDead => _isDead;

        public float MaxHealth
        {
            get => _maxHealth;
            set {
                _maxHealth = value;
                _health = value;
            }
        }
        public float Health => _health;
        [SerializeField] private bool _isDead = false;
        [SerializeField] private bool _isImmortal = false;
        [Title("Health Setting")]
        [SerializeField] private float _maxHealth = 100f;
        [SerializeField] private float _health = 100f;
        [Title("Defence Setting")]
        [SerializeField] private float _baseResistence = 0f;
        [FoldoutGroup("Health Events")]
        [SerializeField] private UnityEvent _onHealthEmpty;
        [FoldoutGroup("Health Events")]
        [SerializeField] private UnityEvent<float> _onHealthChange;
        public void GiveDamage(float health,string attackerTag = null) {
            if (_isImmortal) return;
            if (_isDead) return;
            _health -= health;
            _health = Mathf.Clamp(_health, 0f, _maxHealth);
            if (_health <= 0f) Death();
            _onHealthChange?.Invoke(_health);
        }
        private void Update()
        {
            if (_isImmortal) return;
            if (_isDead) return;
            _health = Mathf.Clamp(_health, 0f, _maxHealth);
            _onHealthChange?.Invoke(_health);
        }
        public void Death()
        {
            if (_isImmortal) return;
            _isDead = true;
            _onHealthEmpty?.Invoke();
        }
        public void Reset() {
            _health = _maxHealth;
            _isDead = false;
            _onHealthChange?.Invoke(_health);
        }
    }
}