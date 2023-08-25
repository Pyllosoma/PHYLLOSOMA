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
        [SerializeField] private float _updatePerSecond = 15f;
        [SerializeField] private AnimationCurve _followSpeedCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        private void OnEnable()
        {
            StartCoroutine(FollowTarget());
        }
        internal Vector3 GetFollowPosition(Vector3 target)
        {
            Vector3 directionFromTarget = transform.position - target;
            Vector3 followPosition = directionFromTarget.normalized * _followDistance + target;
            return followPosition;
        }
        private IEnumerator FollowTarget()
        {
            float updateTime = 1f / _updatePerSecond;
            float currentTime = 0f;
            Vector3 lastTargetPosition = _target.transform.position;
            Vector3 followPosition = GetFollowPosition(lastTargetPosition);
            while (gameObject.activeSelf) {
                if (_target == null) {
                    yield return new WaitForSeconds(updateTime);
                    continue;
                }
                Vector3 currentTargetPosition = _target.transform.position;
                if (lastTargetPosition == currentTargetPosition) {
                    if (currentTime < _followTime) {
                        currentTime += updateTime;
                        float followSpeed = _followSpeedCurve.Evaluate(currentTime / _followTime);
                        transform.position = Vector3.Lerp(transform.position, followPosition, followSpeed);
                    }
                    yield return new WaitForSeconds(updateTime);
                    continue;
                }
                currentTime = updateTime;
                lastTargetPosition = currentTargetPosition;
                followPosition = GetFollowPosition(lastTargetPosition);
                transform.position = Vector3.Lerp(transform.position, followPosition ,_followSpeedCurve.Evaluate(currentTime / _followTime));
                yield return new WaitForSeconds(updateTime);
            }
        }
        private void OnDisable()
        {
            StopAllCoroutines();
        }
    }
}