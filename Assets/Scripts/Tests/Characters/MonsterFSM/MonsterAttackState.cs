using Runtime.Patterns.FSM;
using UnityEngine;

namespace Tests.Characters.MonsterFSM
{
    public class MonsterAttackState : IState<Monster>
    {
        public void Enter(Monster entity)
        {

        }
        public void Update(Monster entity)
        {
            if (!entity.TargetDetector.IsTargetExist) {
                entity.State = new MonsterIdleState();
                return;
            }
            var target = entity.TargetDetector.Targets[0];
            if (!entity.Laser.IsInRange(target)) {
                entity.State = new MonsterIdleState();
                return;
            }
            if (!entity.TargetLooker.IsInAngle) {
                entity.Laser.Finish();
                return;
            }
            // if (entity.TargetBlockChecker.IsDirectionBlocked) {
            //     entity.Laser.Finish();
            //     return;
            // }
            entity.Laser.Ready();
            entity.Laser.Attack(target,entity.TargetBlockChecker.TargetPosition);
        }
        public void Exit(Monster entity)
        {
            entity.Laser.Finish();
        }
    }
}