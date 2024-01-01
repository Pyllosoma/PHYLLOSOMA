using System;
using System.Collections.Generic;
using Runtime.Attributes;
using UnityEngine;

namespace Runtime.Utils
{
    /// <summary>
    /// 적을 탐색하기 위한 유틸성 컴포넌트
    /// </summary>
    public class TargetDetector : MonoBehaviour
    {
        public bool IsTargetExist => _isTargetExist;
        public float TargetDistance => _foundTargets.Count > 0 ? Vector3.Distance(transform.position, _foundTargets[0].transform.position) : float.MaxValue;
        public float TargetAngle{
            get {
                var result = 0f;
                if (_foundTargets.Count <=0) return result;
                var targetDirection = _foundTargets[0].transform.position - transform.position;
                var targetAngle = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;
                result = Mathf.DeltaAngle(transform.rotation.eulerAngles.y, targetAngle);
                return result;
            }
        }
        public List<GameObject> Targets => _foundTargets;
        [TagSelector][SerializeField] private List<string> _targetTag = new List<string>();
        [SerializeField] private List<GameObject> _foundTargets = new List<GameObject>();
        [SerializeField] private bool _isTargetExist = false;
        private void FixedUpdate(){
            #if UNITY_EDITOR
            //Draw Found Targets in Editor
            for (int i = 0; i < _foundTargets.Count; i++) {
                Debug.DrawLine(transform.position, _foundTargets[i].transform.position, i == 0 ? Color.red : Color.green);
            }
            #endif
            CheckNullAndDeactivate();
        }
        private void CheckNullAndDeactivate()
        {
            for (var i = 0; i < _foundTargets.Count; i++) {
                if (_foundTargets[i] == null) {
                    _foundTargets.RemoveAt(i);
                    i--;
                    continue;
                }
                if (_foundTargets[i].activeSelf == false) {
                    _foundTargets.RemoveAt(i);
                    i--;
                    continue;
                }
            }
            _isTargetExist = _foundTargets.Count > 0;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (_targetTag.Contains(other.tag)&&!_foundTargets.Contains(other.gameObject)) {
                _foundTargets.Add(other.gameObject);
                _isTargetExist = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (_targetTag.Contains(other.tag)&&_foundTargets.Contains(other.gameObject)) {
                _foundTargets.Remove(other.gameObject);
                _isTargetExist = _foundTargets.Count > 0;
            }
        }
    }
}