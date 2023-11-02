using System;
using System.Collections.Generic;
using Runtime.Patterns.FSM;
using UnityEngine;

namespace Tests.Characters.MonsterFSM.StatueStates
{
    [Serializable]
    public class StatueShockWaveAttackState : IState<Statue>
    {
        private List<GameObject> _targets = new List<GameObject>();
        [SerializeField] private float _startTime = 0f;
        [SerializeField] private float _chargeTime = 1f;
        [SerializeField] private float _attackTime = 1f;
        [SerializeField] private Vector3 _groundPosition = Vector3.zero;
        private Vector3 _lastPosition = Vector3.zero;
        public void Enter(Statue entity)
        {
            _lastPosition = entity.transform.position;
            if (Physics.Raycast(entity.transform.position, Vector3.down, out RaycastHit hit)) {
                _groundPosition = hit.point;
            }
            _targets = entity.TargetDetector.Targets;
            _startTime = Time.time;
            //entity.ShockWave.Ready();
        }
        public void Update(Statue entity)
        {
            //충전 이펙트
            if (_startTime + _chargeTime < Time.time) {
                //entity.ShockWave.Charge();
                return;
            }
            //공격 이펙트
            if (_startTime + _chargeTime + _attackTime < Time.time) {
                //entity.ShockWave.Attack(_targets);
                return;
            }
            entity.State = new StatueIdleState();
        }
        public void FixedUpdate(Statue entity)
        {
            
        }
        public void Exit(Statue entity)
        {
            //entity.ShockWave.Finish();
        }
    }
}