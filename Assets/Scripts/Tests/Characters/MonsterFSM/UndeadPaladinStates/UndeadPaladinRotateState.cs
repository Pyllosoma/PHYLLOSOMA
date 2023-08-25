using DG.Tweening;
using Runtime.Patterns.FSM;
using UnityEngine;

namespace Tests.Characters.MonsterFSM.UndeadPaladinStates
{
    public class UndeadPaladinRotateState : IState<UndeadPaladin>
    {
        private float _rotateSpeed = 1f;
        public void Enter(UndeadPaladin entity)
        {
            //Debug.Log(entity.TargetLooker.CurrentAngleGap);
            entity.Animator.transform.DOMove(entity.transform.position, 0.25f).SetEase(Ease.InSine);
            entity.Animator.transform.DORotate(entity.transform.rotation.eulerAngles, 0.25f).SetEase(Ease.InSine);
            entity.Animator.transform.localRotation = Quaternion.identity;
            Debug.Log("RotateState");
            entity.Animator.applyRootMotion = false;
            entity.Animator.SetTrigger("Rotate");
        }
        public void Update(UndeadPaladin entity)
        {
            //Debug.Log(entity.TargetLooker.CurrentAngleGap);
            if (!entity.TargetDetector.IsTargetExist) {
                entity.State = new UndeadPaladinIdleState();
                return;
            }
            if (entity.TargetLooker.IsInAngle) {
                entity.State = new UndeadPaladinAttackState();
                return;
            }
        }
        public void FixedUpdate(UndeadPaladin entity){
            entity.transform.Rotate(Vector3.up, entity.TargetLooker.CurrentAngleGap * _rotateSpeed * Time.fixedDeltaTime);
        }
        public void Exit(UndeadPaladin entity)
        {
            Debug.Log("Exit Rotate State");
            entity.Animator.applyRootMotion = true;
        }
    }
}