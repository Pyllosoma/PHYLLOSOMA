using DG.Tweening;
using UnityEngine;

namespace Runtime.Utils.Targetables
{
    public class ObjectRotator : MonoBehaviour, ITargetable
    {
        [SerializeField] private bool _resetAngle = false;
        [SerializeField] private bool _rotateToTarget = false;
        [SerializeField] private float _rotateTime = 1f;
        [SerializeField] private Ease _ease = Ease.InSine;
        [SerializeField] private Vector3 _rotateAngle = Vector3.zero;
        [SerializeField] private Vector3 _startAngle = Vector3.zero;
        [SerializeField] private Transform _target = null;
        public void SetTarget(Transform target) {
            _target = target;
        }
        public void RotateObject(Vector3 angle) {
            transform.DORotate(transform.rotation.eulerAngles + angle, _rotateTime).SetEase(_ease);
        }
        public void RotateObjectFromStart(Vector3 angle) {
            transform.DORotate(_startAngle + angle, _rotateTime).SetEase(_ease);
        }
        public void ResetAngle() {
            transform.DORotate(_startAngle, _rotateTime).SetEase(_ease);
        }
        public void LookAtTarget() {
            if (!_target) return;
            transform.DOLookAt(_target.position + _rotateAngle, _rotateTime).SetEase(_ease);
        }
        private void Start() {
            _startAngle = transform.rotation.eulerAngles;
        }
        private void Update()
        {
            if (_rotateToTarget) {
                _rotateToTarget = false;
                LookAtTarget();
                
            }
            if (_resetAngle) {
                _resetAngle = false;
                transform.DORotate(_startAngle, _rotateTime).SetEase(_ease);
            }
        }
    }
}