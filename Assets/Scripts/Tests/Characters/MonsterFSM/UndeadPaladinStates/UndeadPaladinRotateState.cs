using DG.Tweening;
using Runtime.Patterns.FSM;
using UnityEngine;

namespace Tests.Characters.MonsterFSM.UndeadPaladinStates
{
    public class UndeadPaladinRotateState : IState<UndeadPaladin>
    {
        private float _rotateTime = 1f;
        private float _rotateStartTime = 0f;
        public void Enter(UndeadPaladin entity)
        {
            Debug.Log("RotateState");
            entity.Animator.applyRootMotion = false;
            entity.Animator.SetTrigger("Rotate");
            
            entity.Rigidbody.angularVelocity = Vector3.zero;
            Vector3 targetDirection = entity.gameObject.transform.rotation.eulerAngles + new Vector3(0, entity.TargetLooker.CurrentAngleGap, 0);
            entity.Rigidbody.DORotate(targetDirection, _rotateTime);
            _rotateStartTime = Time.time;
        }
        public void Update(UndeadPaladin entity)
        {
            //Debug.Log(entity.TargetLooker.CurrentAngleGap);
            if (Time.time - _rotateStartTime < _rotateTime) return;
            
            entity.State = !entity.TargetDetector.IsTargetExist ? new UndeadPaladinIdleState() : new  UndeadPaladinAttackState();
        }
        public void FixedUpdate(UndeadPaladin entity)
        {
        }
        public void Exit(UndeadPaladin entity)
        {
            Debug.Log("Exit Rotate State");
            entity.Rigidbody.angularVelocity = Vector3.zero;
            entity.Animator.applyRootMotion = true;
        }
    }
}