using Runtime.Characters.FSM;
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
            // //공격하는 부분
            // if (entity.Laser.IsInAttackAngle(target)) {
            //     //Rotate to target
            //     var targetDir = target.transform.position - entity.transform.position;
            //     var newDir = Vector3.RotateTowards(entity.transform.forward, targetDir, entity.RotateSpeed * Time.deltaTime, 0.0f);
            //     entity.transform.rotation = Quaternion.LookRotation(newDir);
            //     return;
            // }
            entity.Laser.Attack(target);
        }
        public void Exit(Monster entity)
        {
            
        }
    }
}