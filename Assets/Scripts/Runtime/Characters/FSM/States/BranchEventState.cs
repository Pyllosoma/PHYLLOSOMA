using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Runtime.Characters.FSM.States
{
    [Serializable]
    public class BranchEventState : GameObjectFSM
    {
        [Serializable]
        private class EventBranch
        {
            public float Chance => _chance;
            public UnityEvent OnBranchSelected => _onBranchSelected;
            [SerializeField] private float _chance = 1f;
            [SerializeField] private UnityEvent _onBranchSelected;
        }
        [SerializeField] private bool _useWaitTime = false;
        [ShowIf("_useWaitTime")] [SerializeField] private float _waitTime = 1f;
        [SerializeField] private List<EventBranch> _patternBranches = new List<EventBranch>();
        private float _totalChance = 0f;
        private bool _isCalculated = false;
        private float _timer = 0f;
        private bool _isActivated = false;
        public override void Enter(GameObject entity)
        {
            base.Enter(entity);
            if (!_isCalculated) {
                _isCalculated = true;
                _totalChance = 0f;
                foreach (var patternBranch in _patternBranches) {
                    _totalChance += patternBranch.Chance;
                }
            }
            _timer = 0f;
            _isActivated = false;
            if (!_useWaitTime) {
                ActivateBranch();
            }
        }
        public override void Update(GameObject entity)
        {
            base.Update(entity);
            if (!_useWaitTime) return;
            _timer += Time.deltaTime;
            if (_timer <= _waitTime) return;
            if (_isActivated) return;
            ActivateBranch();
        }

        private void ActivateBranch()
        {
            _isActivated = true;
            var random = Random.Range(0f, _totalChance);
            var currentChance = 0f;
            
            foreach (var patternBranch in _patternBranches) {
                currentChance += patternBranch.Chance;
                if (random <= currentChance) {
                    patternBranch.OnBranchSelected?.Invoke();
                    break;
                }
            }
        }
    }
}