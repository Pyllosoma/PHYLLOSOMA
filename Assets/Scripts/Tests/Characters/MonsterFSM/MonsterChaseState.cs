using Runtime.Characters.FSM;
using UnityEngine;

namespace Tests.Characters.MonsterFSM
{
    public class MonsterChaseState : IState<Monster>
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
            if (entity.Laser.IsInRange(target)) {
                entity.State = new MonsterAttackState();
                return;
            }
            entity.Controller.SetDestination(target.transform.position);
        }
        public void Exit(Monster entity)
        {
            //캐릭터를 멈추도록 만든다
            entity.Controller.ResetPath();
            entity.Controller.velocity = Vector3.zero;
        }
    }
}