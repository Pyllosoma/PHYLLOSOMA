using Runtime.Patterns.FSM;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Tests.Characters.MonsterFSM.UndeadPaladinStates
{
    public class UndeadPaladinPatrolState : IState<UndeadPaladin>
    {
        private Vector3 _currentPatrolPosition = Vector3.zero;
        private Vector2 _randomRange = new Vector2(-5f, 5f);
        public void Enter(UndeadPaladin entity)
        {
            entity.Animator.applyRootMotion = false;
            entity.Animator.transform.localPosition = Vector3.zero;
            entity.Animator.transform.localRotation = Quaternion.identity;
            entity.Controller.speed = entity.PatrolSpeed;
            _currentPatrolPosition.x = Random.Range(_randomRange.X, _randomRange.Y);
            _currentPatrolPosition.z = Random.Range(_randomRange.X, _randomRange.Y);
            entity.Controller.SetDestination(entity.transform.position + _currentPatrolPosition);
        }
        public void Update(UndeadPaladin entity)
        {
            entity.Animator.SetFloat("Speed", entity.Controller.velocity.magnitude);
            if (entity.TargetDetector.IsTargetExist) {
                entity.State = new UndeadPaladinChasePattern();
                return;
            }
            if (!entity.Controller.hasPath) {
                entity.State = new UndeadPaladinIdleState();
                return;
            }
        }
        public void FixedUpdate(UndeadPaladin entity)
        {
            
        }
        public void Exit(UndeadPaladin entity)
        {
            entity.Animator.applyRootMotion = true;
            entity.Controller.speed = entity.Speed;
            entity.Controller.ResetPath();
        }
    }
}