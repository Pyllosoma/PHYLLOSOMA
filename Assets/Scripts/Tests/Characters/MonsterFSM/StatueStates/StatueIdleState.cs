using System;
using Runtime.Patterns.FSM;

namespace Tests.Characters.MonsterFSM.StatueStates
{
    [Serializable]
    public class StatueIdleState : IState<Statue>
    {
        public void Enter(Statue entity)
        {
            entity.TargetLooker.SetTarget(null);
            entity.TargetBlockChecker.SetTarget(null);
        }
        public void Update(Statue entity) {
            //entity.State = entity.ShockWaveAttackState;
            if (entity.TargetDetector.IsTargetExist) {
                entity.State = entity.ChaseState;
            }
        }
        public void FixedUpdate(Statue entity)
        {
            
        }
        public void Exit(Statue entity)
        {
            
        }
    }
}