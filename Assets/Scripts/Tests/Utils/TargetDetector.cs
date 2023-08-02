using System;
using System.Collections.Generic;
using Runtime.Attributes;
using UnityEngine;

namespace Tests.Utils
{
    /// <summary>
    /// 적을 탐색하기 위한 유틸성 컴포넌트
    /// </summary>
    public class TargetDetector : MonoBehaviour
    {
        public bool IsTargetExist => _foundTargets.Count > 0;
        public List<GameObject> Targets => _foundTargets;
        [TagSelector][SerializeField] private List<string> _targetTag = new List<string>();
        [SerializeField] private List<GameObject> _foundTargets = new List<GameObject>();
        private void OnTriggerEnter(Collider other)
        {
            if (_targetTag.Contains(other.tag)&&!_foundTargets.Contains(other.gameObject)) {
                Debug.Log($"Target {other.gameObject.name} is found.");
                _foundTargets.Add(other.gameObject);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            Debug.Log($"Target {other.gameObject.name} is lost.");
            _foundTargets.Remove(other.gameObject);
        }
    }
}