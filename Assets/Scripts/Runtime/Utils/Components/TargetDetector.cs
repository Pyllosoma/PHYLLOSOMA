using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Runtime.Attributes;
using UnityEngine;

namespace Runtime.Utils.Components
{
    /// <summary>
    /// 적을 탐색하기 위한 유틸성 컴포넌트
    /// </summary>
    public class TargetDetector : TargetableComponent
    {
        public override GameObject Target
        {
            get => IsTargetExist ? _foundTargets[0] : null;
            set {
                #if UNITY_EDITOR
                    Debug.Log("Target is readonly");
                #endif
                return;
            }
        }
        public override bool IsTargetExist => _isTargetExist;
        public List<GameObject> Targets => _foundTargets;
        [TagSelector][SerializeField] private List<string> _targetTag = new List<string>();
        [SerializeField] private List<GameObject> _foundTargets = new List<GameObject>();
        private bool _isTargetExist = false;
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