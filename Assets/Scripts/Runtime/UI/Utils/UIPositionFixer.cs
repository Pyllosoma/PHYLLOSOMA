using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.UI.Utils
{
    public class UIPositionFixer : MonoBehaviour
    {
        [SerializeField] private bool _fixDistance = true;
        [ShowIf("_fixDistance")][SerializeField] private Vector2 _distance = new Vector2(3f, 5f);
        [ShowIf("_fixDistance")][SerializeField] private bool _useStartOffset = true;
        [ShowIf("_fixDistance")][HideIf("_useStartOffset")][SerializeField] private Vector3 _offset = new Vector3(0f, 0f, 0f);
        [ShowIf("_fixDistance")][SerializeField] private Transform _targetTransform;
        [SerializeField] private bool _fixRotation = true;
        private Camera _camera;
        private void Awake() {
            _camera = Camera.main;
            if (_useStartOffset) {
                _offset = transform.localPosition;
            }
        }
        private void Update() {
            if (_fixDistance) InternalFixDistance();
            if (_fixRotation) InternalFixRotation();
        }
        private void InternalFixDistance()
        {
            var camPos = _camera.transform.position;
            var targetPos = _targetTransform.position + _offset;
            var direction = targetPos - camPos;
            direction.Normalize();
            var distance = Vector3.Distance(targetPos, camPos);
            transform.position = camPos + direction * Mathf.Clamp(distance, _distance.x, _distance.y);
        }
        private void InternalFixRotation() {
            transform.LookAt(_camera.transform);
        }
    }
}