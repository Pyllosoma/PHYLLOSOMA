using System;
using Runtime.Patterns.FSM;
using Unity.VisualScripting;
using UnityEngine;

namespace Tests.Characters.MonsterFSM.SkeletonKnightStates
{
    [Serializable]
    public class SkeletonKnightAttackState : IState<SkeletonKnight>
    {
        public float AttackRange => _attackRange;
        [SerializeField] private float _attackRange = 2f;
        [SerializeField] private float _attackAnimationTime = 1f;
        [SerializeField] private float _attackAnimationClipLength = 2.4f;

        private float _animationSpeedRatio = 1f;
        private float _timer = 0f;
        public void Enter(SkeletonKnight entity)
        {
            Debug.Log("Enter Attack State");
            _animationSpeedRatio = _attackAnimationClipLength / _attackAnimationTime;
            entity.Animator.applyRootMotion = true;
            entity.Animator.SetFloat("AttackSpeed", _animationSpeedRatio);
            entity.Animator.SetBool("IsAttack",true);
            //Debug.Log("_animationSpeedRatio : " + _animationSpeedRatio);
        }
        public void Update(SkeletonKnight entity)
        {
            _timer += Time.deltaTime;
            if (_timer <= _attackAnimationTime) return;
            //Debug.Log("Timer: " + _timer);
            _timer = 0f;
            if (!entity.TargetDetector.IsTargetExist) {
                entity.State = entity.IdleState;
                return;
            }
            if (entity.TargetDetector.TargetDistance > _attackRange) {
                entity.State = entity.ChaseState;
                return;
            }
        }
        public void Exit(SkeletonKnight entity)
        {
            entity.Animator.applyRootMotion = false;
            entity.Animator.speed = 1f;
            _animationSpeedRatio = 1f;
            entity.Animator.SetFloat("AttackSpeed", _animationSpeedRatio);
            entity.Animator.SetBool("IsAttack",false);
        }
        public void FixedUpdate(SkeletonKnight entity) { }
    }
}