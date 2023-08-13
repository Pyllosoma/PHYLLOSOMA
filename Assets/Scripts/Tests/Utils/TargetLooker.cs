using UnityEngine;

namespace Tests.Utils
{
    public class TargetLooker : MonoBehaviour
    {
        public bool IsInAngle => _isInAngle;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private bool _isXRotate = true;
        [SerializeField] private bool _isYRotate = true;
        [SerializeField] private bool _isZRotate = true;
        [SerializeField] private bool _stopWhenInAngle = true;
        [SerializeField] private float _maxAngleGap = 45f;
        [SerializeField] private Transform _target;
        [SerializeField] private bool _isInAngle = false;
        private void FixedUpdate()
        {
            if (!_target) {
                _isInAngle = false;
                return;
            }
            var targetDirection = _target.position - transform.position;
            _isInAngle = Mathf.Abs(Vector3.Angle(targetDirection,transform.forward)) <= _maxAngleGap;
            if (_stopWhenInAngle && _isInAngle) {
                return;
            }
            var newDirection = Vector3.RotateTowards(transform.forward, targetDirection, _rotationSpeed * Time.fixedDeltaTime, 0.0f);
            var quaternion = Quaternion.LookRotation(newDirection);
            transform.rotation = Quaternion.Euler(
                _isXRotate ? quaternion.eulerAngles.x : transform.rotation.eulerAngles.x,
                _isYRotate ? quaternion.eulerAngles.y : transform.rotation.eulerAngles.y,
                _isZRotate ? quaternion.eulerAngles.z : transform.rotation.eulerAngles.z);
        }
        public void SetTarget(Transform target) {
            _target = target;
        }
    }
}