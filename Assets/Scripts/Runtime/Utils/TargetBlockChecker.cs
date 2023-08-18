using System;
using UnityEngine;

namespace Runtime.Utils
{
    public class TargetBlockChecker : MonoBehaviour
    {
        public bool IsDirectionBlocked => _isDirectionBlocked;
        public Vector3 TargetPosition => _targetPosition;
        [SerializeField] private bool _isDirectionBlocked = false;
        [SerializeField] private float _directionCheckRange = 20f;
        [SerializeField] private Vector3 _targetPosition;
        private Transform _target;
        private void FixedUpdate()
        {
            if (!_target) {
                _isDirectionBlocked = false;
                _targetPosition = Vector3.zero;
                return;
            }
            var targetDirection = _target.position - transform.position;
            if (Physics.Raycast(transform.position,targetDirection,out var hit,_directionCheckRange)) {
                _isDirectionBlocked = hit.collider.gameObject != _target.gameObject;
                _targetPosition = hit.point;
            }
        }
        public void SetTarget(Transform target)
        {
            _target = target;
        }
    }
}