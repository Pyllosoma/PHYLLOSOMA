using Runtime.Patterns.FSM;
using UnityEngine;

namespace Tests.Characters.MonsterFSM.UndeadPaladinStates
{
    public class UndeadPaladinAttackState : IState<UndeadPaladin>
    {

        public void Enter(UndeadPaladin entity)
        {
            Debug.Log("UndeadPaladinAttackState Enter");
            entity.MeleeWeapon.Ready();
            entity.Animator.SetBool("IsAttack", true);
        }
        public void Update(UndeadPaladin entity)
        {
            if (!entity.TargetDetector.IsTargetExist) {
                entity.State = new UndeadPaladinIdleState();
                return;
            }
            var target = entity.TargetDetector.Targets[0];
            if (!entity.MeleeWeapon.IsInRange(Vector3.Distance(entity.gameObject.transform.position, target.transform.position))) {
                entity.State = new UndeadPaladinChasePattern();
                return;
            }
            // if (entity.TargetBlockChecker.IsDirectionBlocked) {
            //     entity.MeleeWeapon.Finish();
            //     return;
            // }
            entity.MeleeWeapon.Attack(target,target.transform.position);
        }
        public void FixedUpdate(UndeadPaladin entity)
        {
            
        }
        public void Exit(UndeadPaladin entity)
        {
            entity.Animator.SetBool("IsAttack", false);
            entity.MeleeWeapon.Finish();
        }
    }
}