using Runtime.Utils.Components;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Runtime.Characters.FSM.States.Skills
{
    public class GameObjectLaunchState : TargetBaseState
    {
        [Title("State Settings")]
        [SerializeField] private bool _isOneShot = true; 
        [HideIf("_isOneShot")] [SerializeField] private int _shotCount = 1;
        [HideIf("_isOneShot")] [SerializeField] private float _shotInterval = 0.5f;
        [SerializeField] private int _launchCount = 1;
        [SerializeField] private bool _useCustomSpawnPoint = false;
        [ShowIf("_useCustomSpawnPoint")] [SerializeField] private Transform _spawnPoint;
        [SerializeField] private GameObject _gameObjectPrefab;
        [FoldoutGroup("Launch Events")]
        [SerializeField] private UnityEvent<GameObject> _onLaunch;
        [FoldoutGroup("Launch Events")]
        [SerializeField] private UnityEvent _onLaunchEnd;
        private int _currentShotCount = 0;
        private float _timer = 0f;
        public override void Enter(GameObject entity) {
            base.Enter(entity);
            if (_isOneShot) _shotCount = 1;
            _currentShotCount = 0;
            _timer = 0f;
        }
        public override void Update(GameObject entity) {
            if (_currentShotCount >= _shotCount) return;
            _timer += Time.deltaTime;
            if (_timer >= _shotInterval) {
                _timer = 0f;
                Launch(entity);
            }
        }

        private void Launch(GameObject entity) {
            for (int i = 0; i < _launchCount; i++)
            {
                var spawnTransform = _useCustomSpawnPoint ? _spawnPoint : entity.transform;
                var go = Object.Instantiate(_gameObjectPrefab, spawnTransform.position, spawnTransform.rotation);
                go.GetComponent<TargetableComponent>().Target = _targetableComponent.Target;
                _onLaunch?.Invoke(_targetableComponent.Target);
            }
            _currentShotCount++;
            if (_currentShotCount >= _shotCount) {
                _onLaunchEnd?.Invoke();
            }
        }
    }
}