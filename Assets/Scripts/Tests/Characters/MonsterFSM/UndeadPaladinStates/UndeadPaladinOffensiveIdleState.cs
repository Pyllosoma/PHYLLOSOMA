using System;
using Runtime.Patterns.FSM;
using UnityEngine;

namespace Tests.Characters.MonsterFSM.UndeadPaladinStates
{
    public class UndeadPaladinOffensiveIdleState : IState<UndeadPaladin>
    {
        private float _currentIdleStateTime = 5f;
        private float _startTime = 0f;
        public void Enter(UndeadPaladin entity)
        {
            _startTime = UnityEngine.Time.time;
            entity.Animator.SetTrigger("Offensive Idle");
            //Debug.Log("OffensiveIdleState");
        }
        public void Update(UndeadPaladin entity)
        {
            if (entity.TargetDetector.IsTargetExist) {
                entity.State = new UndeadPaladinChasePattern();
                return;
            }
            if (Time.time - _startTime > _currentIdleStateTime) {
                entity.State = new UndeadPaladinIdleState();
                return;
            }
        }
        public void FixedUpdate(UndeadPaladin entity)
        {
            
        }
        public void Exit(UndeadPaladin entity)
        {
            
        }
    }
}