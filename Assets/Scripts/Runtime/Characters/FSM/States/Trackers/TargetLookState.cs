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
            if (!_targetableComponent.IsTargetExist) return;
            var targetDirection = _targetableComponent.Target.transform.position - _transform.position;
            targetDirection.Normalize();
            var newDirection = Vector3.RotateTowards(_transform.forward, targetDirection, _rotationSpeed * Time.fixedDeltaTime, 0.0f);
            var quaternion = Quaternion.LookRotation(newDirection);
            var rotation = quaternion.eulerAngles;
            // Debug.Log(rotation);
            if (!_isXRotate) rotation.x = 0f;
            if (!_isYRotate) rotation.y = 0f;
            if (!_isZRotate) rotation.z = 0f;
            _transform.rotation = Quaternion.Euler(rotation);
        }
    }
}