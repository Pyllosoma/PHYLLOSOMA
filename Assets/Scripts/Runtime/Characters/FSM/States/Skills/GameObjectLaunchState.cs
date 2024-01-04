using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Characters.FSM.States.Skills
{
    public class GameObjectLaunchState : TargetBaseState
    {
        [Title("State Settings")]
        [SerializeField] private bool _isOneShot = true;
        [HideIf("_isOneShot")] [SerializeField] private float _launchInterval = 0.5f;
        [HideIf("_isOneShot")] [SerializeField] private int _shotCount = 1;
        [SerializeField] private int _launchCount = 1;
        [SerializeField] private bool _useCustomSpawnPoint = false;
        [ShowIf("_useCustomSpawnPoint")] [SerializeField] private Transform _spawnPoint;
        [SerializeField] private GameObject _gameObjectPrefab;
        [FoldoutGroup("Launch Events")]
        [SerializeField] private UnityEvent<GameObject> _onLaunch;
        [FoldoutGroup("Launch Events")]
        [SerializeField] private UnityEvent _onLaunchEnd;
        private int _currentLaunchCount = 0;
        private float _timer = 0f;
        public override void Enter(GameObject entity) {
            base.Enter(entity);
            _currentLaunchCount = 0;
            _timer = 0f;
        }
        public override void Update(GameObject entity) {
            if (_currentLaunchCount >= _launchCount) return;
            _timer += Time.deltaTime;
            if (_timer >= _launchInterval) {
                _timer = 0f;
                Launch(entity);
            }
        }

        private void Launch(GameObject entity) {
            if (_useCustomSpawnPoint) {
                var go = Object.Instantiate(_gameObjectPrefab, _spawnPoint.position, _spawnPoint.rotation);
                _onLaunch?.Invoke(go);
            }
            else {
                var go = Object.Instantiate(_gameObjectPrefab, entity.transform.position, entity.transform.rotation);
                _onLaunch?.Invoke(go);
            }
            _currentLaunchCount++;
            if (_currentLaunchCount >= _launchCount) {
                _onLaunchEnd?.Invoke();
                if (_isOneShot) {
                    _currentLaunchCount = 0;
                    _timer = 0f;
                }
            }
        }
    }
}