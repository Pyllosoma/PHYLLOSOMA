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
                entity.Laser.EnableLaser(false);
                return;
            }
            entity.Laser.EnableLaser(true);
            entity.Laser.Attack(target);
        }
        public void Exit(Monster entity)
        {
            entity.Laser.EnableLaser(false);
        }
    }
}