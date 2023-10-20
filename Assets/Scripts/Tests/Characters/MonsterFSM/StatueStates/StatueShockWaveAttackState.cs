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
        public void Enter(Statue entity)
        {
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