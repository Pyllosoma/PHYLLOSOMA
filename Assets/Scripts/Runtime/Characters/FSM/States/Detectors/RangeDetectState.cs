using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Characters.FSM.States.Detectors
{
    public class RangeDetectState : TargetBaseState
    {
        [Title("State Settings")]
        [SerializeField] private float _range = 1f;
        [FoldoutGroup("Range Events")]
        [SerializeField] private UnityEvent _onEnterRange;
        [FoldoutGroup("Range Events")]
        [SerializeField] private UnityEvent _onExitRange;
        public override void Update(GameObject entity)
        {
            if (_targetDetector.IsTargetExist && _targetDetector.TargetDistance <= _range) {
                _onEnterRange?.Invoke();
            }
            if (!_targetDetector.IsTargetExist || _targetDetector.TargetDistance > _range) {
                _onExitRange?.Invoke();
            }
        }
    }
}