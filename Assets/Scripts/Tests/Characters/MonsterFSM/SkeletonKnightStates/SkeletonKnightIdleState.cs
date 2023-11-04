using System;
using Runtime.Patterns.FSM;
using UnityEngine;

namespace Tests.Characters.MonsterFSM.SkeletonKnightStates
{
    [Serializable]
    public class SkeletonKnightIdleState : IState<SkeletonKnight>
    {
        public void Enter(SkeletonKnight entity) {
            Debug.Log("Enter Idle State");
        }
        public void Update(SkeletonKnight entity)
        {
            if (entity.TargetDetector.IsTargetExist) {
                entity.State = entity.ChaseState;
                return;
            }
        }
        public void FixedUpdate(SkeletonKnight entity)
        {
            
        }
        public void Exit(SkeletonKnight entity)
        {
            
        }
    }
}