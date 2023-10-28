using System;
using Runtime.Patterns.FSM;
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
            entity.Animator.gameObject.transform.localPosition = Vector3.zero;
            entity.Animator.gameObject.transform.localRotation = Quaternion.identity;
            _animationSpeedRatio = _attackAnimationClipLength / _attackAnimationTime;
            entity.Animator.SetFloat("AttackSpeed", _animationSpeedRatio);
            entity.Animator.SetTrigger("Attack");
            Debug.Log("_animationSpeedRatio : " + _animationSpeedRatio);
        }
        public void Update(SkeletonKnight entity)
        {
            _timer += Time.deltaTime;
            if (_timer <= _attackAnimationTime) return;
            Debug.Log("Timer: " + _timer);
            _timer = 0f;
            if (!entity.TargetDetector.IsTargetExist) {
                entity.State = entity.IdleState;
                return;
            }
            var targetDistance = Vector3.Distance(entity.gameObject.transform.position, entity.TargetDetector.Targets[0].transform.position);
            if (targetDistance > _attackRange) {
                entity.State = entity.ChaseState;
                return;
            }
        }
        public void Exit(SkeletonKnight entity)
        {
            entity.Animator.speed = 1f;
            _animationSpeedRatio = 1f;
            entity.Animator.SetFloat("AttackSpeed", _animationSpeedRatio);
            entity.Animator.gameObject.transform.localPosition = Vector3.zero;
            entity.Animator.gameObject.transform.localRotation = Quaternion.identity;

        }
        public void FixedUpdate(SkeletonKnight entity) { }
    }
}