using Sirenix.OdinInspector;
using Tests.Characters.FSM;
using UnityEngine;
using UnityEngine.Events;

namespace Tests.Characters.MonsterFSM.StatueStates
{
    public class StatueGroggy : GameObjectFSM
    {
        public float GroggyTime
        {
            get => _currentGroggyTime;
            set => _currentGroggyTime = value;
        }
        [Title("State Settings")]
        [SerializeField] private float _defaultGroggyTime = 2.5f;
        [SerializeField] private float _currentGroggyTime = 2.5f;
        [FoldoutGroup("State Events")]
        [SerializeField] private UnityEvent _onGroggy;
        [FoldoutGroup("State Events")]
        [SerializeField] private UnityEvent _onGroggyEnd;
        private float _timer = 0f;
        private bool _isGroggyEnd = false;
        public override void Enter(GameObject entity) {
            _currentGroggyTime = _defaultGroggyTime;
            _timer = 0f;
            _isGroggyEnd = false;
            _onGroggy?.Invoke();
        }
        public override void Update(GameObject entity) {
            _timer += Time.deltaTime;
            if (_timer >= _currentGroggyTime && !_isGroggyEnd) {
                _timer = 0f;
                _currentGroggyTime = _defaultGroggyTime;
                _onGroggyEnd?.Invoke();
            }
        }
    }
}