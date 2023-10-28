using System;
using Runtime.Patterns.FSM;
using UnityEngine;

namespace Tests.Characters.MonsterFSM.SkeletonKnightStates
{
    [Serializable]
    public class SkeletonKnightChaseState : IState<SkeletonKnight>
    {
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _acceleration = 10f;
        [SerializeField] private float _rotateSpeed = 720f;
        public void Enter(SkeletonKnight entity)
        {
            entity.Controller.speed = _speed;
            entity.Controller.acceleration = _acceleration;
            entity.Controller.angularSpeed = _rotateSpeed;
        }
        public void Update(SkeletonKnight entity)
        {
            if (!entity.TargetDetector.IsTargetExist) {
                entity.State = entity.IdleState;
                return;
            }
            entity.Controller.SetDestination(entity.TargetDetector.Targets[0].transform.position);
            entity.Animator.SetFloat("Speed", entity.Controller.velocity.magnitude);
        }
        public void FixedUpdate(SkeletonKnight entity)
        {
            if (!entity.TargetDetector.IsTargetExist) {
                entity.State = entity.IdleState;
                return;
            }
            entity.Controller.SetDestination(entity.TargetDetector.Targets[0].transform.position);
            if (entity.Controller.remainingDistance <= entity.AttackState.AttackRange) {
                entity.State = entity.AttackState;
                return;
            }
        }
        public void Exit(SkeletonKnight entity)
        {
            entity.Animator.SetFloat("Speed",0f);
            entity.Controller.ResetPath();
        }
    }
}