using Runtime.Patterns.FSM;
using UnityEngine;

namespace Tests.Characters.MonsterFSM.StatueStates
{
    public class StatueAttackState : IState<Statue>
    {
        public void Enter(Statue entity)
        {
            
        }
        public void Update(Statue entity)
        {
            if (!entity.TargetDetector.IsTargetExist) {
                entity.State = new StatueIdleState();
                return;
            }
            var target = entity.TargetDetector.Targets[0];
            float distance = Vector3.Distance(entity.gameObject.transform.position, target.transform.position);
            if (!entity.Laser.IsInRange(distance)) {
                entity.State = new StatueIdleState();
                return;
            }
            //일정 사거리 안에 오거나 혹은 공격 각도 밖에 있다면 충격파 공격
            if (!entity.TargetLooker.IsInAngle&&distance < entity.ShockWaveAttackRange) {
                entity.State = new StatueShockWaveAttackState();
                return;
            }
            entity.State = new StatueLaserAttackState();
        }
        public void FixedUpdate(Statue entity)
        {
            
        }
        public void Exit(Statue entity)
        {
            
        }
    }
}