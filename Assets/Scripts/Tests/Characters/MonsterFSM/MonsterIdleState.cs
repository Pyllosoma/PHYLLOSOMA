using Runtime.Characters.FSM;
using UnityEngine;

namespace Tests.Characters.MonsterFSM
{
    public class MonsterIdleState : IState<Monster>
    {

        public void Enter(Monster entity)
        {
            
        }
        public void Update(Monster entity)
        {
            if (entity.TargetDetector.IsTargetExist) {
                entity.State = new MonsterChaseState();
                return;
            }
        }
        public void Exit(Monster entity)
        {
        }
    }
}