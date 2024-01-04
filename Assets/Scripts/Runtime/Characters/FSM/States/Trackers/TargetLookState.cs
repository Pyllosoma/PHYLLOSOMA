using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Characters.FSM.States.Trackers
{
    public class TargetLookState : TargetBaseState
    {
        [Title("State Settings")]
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private bool _isXRotate = true;
        [SerializeField] private bool _isYRotate = true;
        [SerializeField] private bool _isZRotate = true;
        private Transform _transform;
        public override void Enter(GameObject entity)
        {
            base.Enter(entity);
            _transform = entity.transform;
        }

        public override void FixedUpdate(GameObject entity)
        {
            var targetDirection = _targetDetector.Targets[0].transform.position - _transform.position;
            var newDirection = Vector3.RotateTowards(_transform.forward, targetDirection, _rotationSpeed * Time.fixedDeltaTime, 0.0f);
            var quaternion = Quaternion.LookRotation(newDirection);
            _transform.rotation = Quaternion.Euler(
                _isXRotate ? quaternion.eulerAngles.x : _transform.rotation.eulerAngles.x,
                _isYRotate ? quaternion.eulerAngles.y : _transform.rotation.eulerAngles.y,
                _isZRotate ? quaternion.eulerAngles.z : _transform.rotation.eulerAngles.z);
        }
    }
}