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
        public List<GameObject> Targets => _foundTargets;
        [TagSelector][SerializeField] private List<string> _targetTag = new List<string>();
        [SerializeField] private List<GameObject> _foundTargets = new List<GameObject>();
        [SerializeField] private bool _isTargetExist = false;
        private void FixedUpdate(){
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
            // if (_targetTag.Contains(other.tag)&&_foundTargets.Contains(other.gameObject)) {
            //     _foundTargets.Remove(other.gameObject);
            //     _isTargetExist = _foundTargets.Count > 0;
            // }
        }
    }
}