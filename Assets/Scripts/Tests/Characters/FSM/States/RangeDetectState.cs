using Runtime.Utils;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Tests.Characters.FSM.States
{
    public class RangeDetectState : GameObjectFSM
    {
        [Title("State Settings")]
        [SerializeField] private float _range = 1f;
        [Title("Required Components")]
        [SerializeField] private TargetDetector _targetDetector;
        [FoldoutGroup("Range Events")]
        [SerializeField] private UnityEvent _onEnterRange;
        [FoldoutGroup("Range Events")]
        [SerializeField] private UnityEvent _onExitRange;
        public override void Update(GameObject entity)
        {
            base.Update(entity);
            if (_targetDetector.IsTargetExist && _targetDetector.TargetDistance <= _range) {
                _onEnterRange?.Invoke();
            }
            if (!_targetDetector.IsTargetExist || _targetDetector.TargetDistance > _range) {
                _onExitRange?.Invoke();
            }
        }
    }
}