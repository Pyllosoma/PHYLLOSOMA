using Runtime.Patterns.FSM;
using UnityEngine;

namespace Tests.Characters.MonsterFSM.StatueStates
{
    public class StatueChaseState : IState<Statue>
    {
        public void Enter(Statue entity)
        {
            entity.TargetLooker.SetTarget(entity.TargetDetector.Targets[0].transform);
            entity.TargetBlockChecker.SetTarget(entity.TargetDetector.Targets[0].transform);
        }
        public void Update(Statue entity)
        {
            if (!entity.TargetDetector.IsTargetExist) {
                entity.State = new StatueIdleState();
                return;
            }
            var target = entity.TargetDetector.Targets[0];
            if (entity.Laser.IsInRange(Vector3.Distance(entity.gameObject.transform.position, target.transform.position))) {
                entity.State = new StatueAttackState();
                return;
            }
            entity.Controller.SetDestination(target.transform.position);
        }
        public void FixedUpdate(Statue entity)
        {
            
        }
        public void Exit(Statue entity)
        {
            //캐릭터를 멈추도록 만든다
            entity.Controller.ResetPath();
            entity.Controller.velocity = Vector3.zero;
        }
    }
}