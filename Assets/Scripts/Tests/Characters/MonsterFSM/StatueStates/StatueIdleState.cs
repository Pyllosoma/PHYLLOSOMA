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
        public void Update(Statue entity)
        {
            if (entity.TargetDetector.IsTargetExist) {
                entity.State = new StatueChaseState();
                return;
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