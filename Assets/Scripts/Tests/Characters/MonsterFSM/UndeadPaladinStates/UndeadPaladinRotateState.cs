using Runtime.Patterns.FSM;
using UnityEngine;

namespace Tests.Characters.MonsterFSM.UndeadPaladinStates
{
    public class UndeadPaladinRotateState : IState<UndeadPaladin>
    {

        public void Enter(UndeadPaladin entity)
        {
            Debug.Log("RotateState");
        }
        public void Update(UndeadPaladin entity)
        {
            entity.Animator.SetFloat("RotateSpeed", entity.Rigidbody.angularVelocity.magnitude);
            if (!entity.TargetDetector.IsTargetExist) {
                entity.State = new UndeadPaladinIdleState();
                return;
            }
            if (entity.TargetLooker.IsInAngle) {
                entity.State = new UndeadPaladinAttackState();
                return;
            }
        }
        public void FixedUpdate(UndeadPaladin entity)
        {
            if (!entity.TargetLooker.IsInAngle) {
                entity.Rigidbody.MoveRotation(Quaternion.Euler(0,entity.TargetLooker.CurrentAngleGap,0));
            }
        }
        public void Exit(UndeadPaladin entity)
        {
            entity.Rigidbody.angularVelocity = Vector3.zero;
            entity.Animator.SetFloat("RotateSpeed",0f);
        }
    }
}