﻿using UnityEngine;

namespace Runtime.Utils.Targetables
{
    public class TargetLooker : MonoBehaviour,ITargetable
    {
        public bool IsInAngle => _isInAngle;
        public float MaxAngleGap => _maxAngleGap;
        public float RotationSpeed => _rotationSpeed;
        public float CurrentAngleGap => _currentAngleGap;
        [SerializeField] private float _maxAngleGap = 45f;
        [SerializeField] private bool _isInAngle = false;
        [SerializeField] private bool _stopWhenInAngle = true;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private bool _isXRotate = true;
        [SerializeField] private bool _isYRotate = true;
        [SerializeField] private bool _isZRotate = true;
        private float _currentAngleGap = 0f;
        private Transform _target;
        public void SetTarget(Transform target) {
            _target = target;
        }
        private void FixedUpdate()
        {
            #if UNITY_EDITOR
                //draw line in enable angle
                Vector3 lineStart = transform.position;
                Vector3 line1Direction = new Vector3(Mathf.Sin(Mathf.Deg2Rad * (transform.rotation.eulerAngles.y + _maxAngleGap)), 0, Mathf.Cos( Mathf.Deg2Rad * (transform.rotation.eulerAngles.y + _maxAngleGap)));
                Vector3 line2Direction = new Vector3(Mathf.Sin(Mathf.Deg2Rad *(transform.rotation.eulerAngles.y - _maxAngleGap)), 0, Mathf.Cos(Mathf.Deg2Rad *  (transform.rotation.eulerAngles.y - _maxAngleGap)));
                Debug.DrawLine(lineStart,lineStart + line1Direction * 30,Color.green);
                Debug.DrawLine(lineStart, lineStart + line2Direction * 30, Color.green);
            #endif
            
            if (!_target) {
                _isInAngle = false;
                return;
            }
            //get front angle
            var targetDirection = _target.position - transform.position;
            var targetAngle = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;
            _currentAngleGap = Mathf.DeltaAngle(transform.rotation.eulerAngles.y, targetAngle);
            
            
            //Debug.Log(_target.position + " / " + transform.position + " / " + CurrentAngleGap);
            _isInAngle = Mathf.Abs(_currentAngleGap) <= _maxAngleGap;
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
    }
}