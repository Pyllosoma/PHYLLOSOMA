using Runtime.Patterns.FSM;
using UnityEngine;

namespace Tests.Characters.MonsterFSM.UndeadPaladinStates
{
    public class UndeadPaladinAttackState : IState<UndeadPaladin>
    {
        public void Enter(UndeadPaladin entity)
        {
            entity.Animator.SetBool("IsAttack", true);
            entity.MeleeWeapon.SetOwner(entity.gameObject).SetWaitTime(0.5f).SetDelay(0.75f).SetDelay(2.4f).Ready();
        }
        public void Update(UndeadPaladin entity)
        {
            if (!entity.TargetDetector.IsTargetExist) {
                entity.State = new UndeadPaladinIdleState();
                return;
            }
            if (!entity.MeleeWeapon.IsInRange(Vector3.Distance(entity.gameObject.transform.position, entity.TargetBlockChecker.TargetPosition))) {
                entity.State = new UndeadPaladinChasePattern();
                return;
            }
            // //Debug.Log(entity.TargetLooker.IsInAngle);
            // if (!entity.TargetLooker.IsInAngle) {
            //     entity.State = new UndeadPaladinRotateState();
            // }
        }
        public void FixedUpdate(UndeadPaladin entity)
        {
            
            
        }
        public void Exit(UndeadPaladin entity)
        {
            //Debug.Log("Exit Attack State");
            entity.Animator.SetBool("IsAttack", false);
            entity.MeleeWeapon.Finish();
        }
    }
}