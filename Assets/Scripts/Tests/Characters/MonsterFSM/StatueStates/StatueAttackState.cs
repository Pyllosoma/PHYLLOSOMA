using Runtime.Patterns.FSM;
using UnityEngine;

namespace Tests.Characters.MonsterFSM.StatueStates
{
    public class StatueAttackState : IState<Statue>
    {
        public void Enter(Statue entity)
        {
            entity.Laser.Ready();
        }
        public void Update(Statue entity)
        {
            if (!entity.TargetDetector.IsTargetExist) {
                entity.State = new StatueIdleState();
                return;
            }
            var target = entity.TargetDetector.Targets[0];
            if (!entity.Laser.IsInRange(Vector3.Distance(entity.gameObject.transform.position, target.transform.position))) {
                entity.State = new StatueIdleState();
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
            
            entity.Laser.Attack(target,entity.TargetBlockChecker.TargetPosition);
        }
        public void FixedUpdate(Statue entity)
        {
            
        }
        public void Exit(Statue entity)
        {
            entity.Laser.Finish();
        }
    }
}