using System;
using UnityEngine;

namespace Runtime.Utils
{
    public class TargetBlockChecker : MonoBehaviour
    {
        public bool IsDirectionBlocked => _isDirectionBlocked;
        public Vector3 DetectedPosition => _isDirectionBlocked ? _blockPosition : _targetPosition;
        public Vector3 TargetPosition => _targetPosition;
        public Vector3 BlockPosition => _blockPosition;
        [SerializeField] private bool _isDirectionBlocked = false;
        [SerializeField] private float _directionCheckRange = 20f;
        [SerializeField] private Vector3 _targetPosition;
        [SerializeField] private Vector3 _blockPosition;
        private Transform _target;
        private void FixedUpdate()
        {
            if (!_target) {
                _isDirectionBlocked = false;
                _targetPosition = Vector3.zero;
                _blockPosition = Vector3.zero;
                return;
            }
            var targetDirection = _target.position - transform.position;
            Debug.DrawRay(transform.position, targetDirection, Color.red);
            if (!Physics.Raycast(transform.position, targetDirection, out var hit, _directionCheckRange)) return;
            _isDirectionBlocked = hit.collider.gameObject != _target.gameObject;
            if (_isDirectionBlocked) {
                //Debug.Log("Block!");
                _blockPosition = hit.point;
            } else {
                //Debug.Log("Hit!");
                _targetPosition = hit.point;
            }

        }
        public void SetTarget(Transform target)
        {
            _target = target;
        }
    }
}