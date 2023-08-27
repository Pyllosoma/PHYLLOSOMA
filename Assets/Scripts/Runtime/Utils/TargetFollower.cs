using System;
using System.Collections;
using UnityEngine;

namespace Runtime.Utils
{
    public class TargetFollower : MonoBehaviour
    {
        [SerializeField] private Transform _target = null;
        [SerializeField] private float _followTime = 1f;
        [SerializeField] private float _followDistance = 10f;
        [SerializeField] private int _updatePerSecond = 15;
        [SerializeField] private int _followPositionUpdatePerSecond = 10;
        [SerializeField] private AnimationCurve _followSpeedCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        public void SetTarget(Transform target, float followTime = 1f, float followDistance = 10f){
            _target = target;
            _followTime = followTime;
            _followDistance = followDistance;
        }
        private void OnEnable(){
            StartCoroutine(UpdateFollowPosition());
        }
        private Vector3 GetFollowPosition(Vector3 target)
        {
            Vector3 directionFromTarget = transform.position - target;
            Vector3 followPosition = directionFromTarget.normalized * _followDistance + target;
            return followPosition;
        }
        private IEnumerator UpdateFollowPosition()
        {
            float updateTime = 1f / _followPositionUpdatePerSecond;
            Vector3 lastTargetPosition = _target.transform.position;
            Vector3 followPosition = GetFollowPosition(lastTargetPosition);
            IEnumerator followPositionCoroutine = FollowTarget(followPosition);
            StartCoroutine(followPositionCoroutine);
            while (gameObject.activeSelf) {
                yield return new WaitForSeconds(updateTime);
                if (!_target) continue;
                if (lastTargetPosition == _target.transform.position) continue;
                lastTargetPosition = _target.transform.position;
                followPosition = GetFollowPosition(lastTargetPosition);
                StopCoroutine(followPositionCoroutine);
                followPositionCoroutine = FollowTarget(followPosition);
                StartCoroutine(followPositionCoroutine);
            }
            StopCoroutine(followPositionCoroutine);
        }
        private IEnumerator FollowTarget(Vector3 followPosition)
        {
            float updateTime = 1f / _updatePerSecond;
            float time = 0f;
            while (time <= _followTime) {
                time += updateTime;
                float followSpeed = _followSpeedCurve.Evaluate(time / _followTime);
                transform.position = Vector3.Lerp(transform.position, followPosition, followSpeed);
                yield return new WaitForSeconds(updateTime);
            }
        }
        private void OnDisable()
        {
            StopAllCoroutines();
        }
    }
}