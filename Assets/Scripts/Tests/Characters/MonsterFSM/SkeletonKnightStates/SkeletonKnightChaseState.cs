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
            entity.Animator.SetTrigger("Idle");
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
            entity.Animator.SetFloat("Speed",entity.Controller.velocity.magnitude);
            if (entity.TargetDetector.TargetDistance < entity.PatrolState.EnterRange) {
                entity.State = entity.PatrolState;
                return;
            }
        }
        public void FixedUpdate(SkeletonKnight entity)
        {

        }
        public void Exit(SkeletonKnight entity)
        {
            entity.Controller.velocity = Vector3.zero;
            entity.Animator.SetFloat("Speed",0f);
            entity.Controller.ResetPath();
        }
    }
}