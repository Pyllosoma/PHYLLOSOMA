using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Runtime.Characters.FSM.States
{
    public class GroggyState : GameObjectFSM
    {
        [Title("State Settings")]
        public float GroggyTime = 2.5f;
        [SerializeField] private float _currentGroggyTime = 2.5f;
        [FoldoutGroup("State Events")]
        [SerializeField] private UnityEvent _onGroggy;
        [FoldoutGroup("State Events")]
        [SerializeField] private UnityEvent _onGroggyEnd;
        private float _timer = 0f;
        private bool _isGroggyEnd = false;
        public override void Enter(GameObject entity) {
            _currentGroggyTime = GroggyTime;
            _timer = 0f;
            _isGroggyEnd = false;
            _onGroggy?.Invoke();
        }
        public override void Update(GameObject entity) {
            _timer += Time.deltaTime;
            if (_timer >= _currentGroggyTime && !_isGroggyEnd) {
                _timer = 0f;
                _currentGroggyTime = GroggyTime;
                _onGroggyEnd?.Invoke();
            }
        }
    }
}